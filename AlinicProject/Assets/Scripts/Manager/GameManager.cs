using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * ���� �Ŵ���
 * 
 */
public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    public float currentDamage;             // ������ ���ݷ�
    public PlayerBody playerBody;           // �÷��̾�
    public PlayerUpper playableWeapon;      // ���� ��� ���� ����
    public GameObject monster;              // ���� ������Ʈ

    [Header("Input KeyCodes")]
    [SerializeField]
    private KeyCode keyCodeRun = KeyCode.LeftShift; // �޸��� Ű
    private KeyCode keyCodeJump = KeyCode.Space;    // ���� Ű
    private Movement movement;              // Ű���� �Է����� �÷��̾� �̵�, ����
    private Status status;                  // �̵��ӵ� ���� �÷��̾� ����
    private int gunIndex = 1;               // ����� ������ ��
    private float moveX, moveZ;             // �̵� ��
    private float disPlayerToMonster;       // �÷��̾�� ���� ������ �Ÿ�
    private bool die = false;               // ��� Ʈ����
    private string[] listWeapon = { "Rifle", "Pistol", "Shotgun" };    // ���� ���
    private GameObject playableCharacter;   // �÷��̾��� ĳ����
    
    

    void Awake()
    {
        instance = this;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        movement = playerBody.GetComponent<Movement>();
        status = playerBody.GetComponent<Status>();
    }
    private void Start()
    {
        CreateCharacter("Man_Full");
        SelectWeapon();    // �⺻���� ����
        playableCharacter.transform.localPosition = Vector3.zero;   // ĳ������ ��ġ
    }
    private void Update()
    {
        disPlayerToMonster = Vector3.Distance(playerBody.transform.position, monster.transform.position);
        UIManager.instance.SetEnableBossHp(disPlayerToMonster);
        Minimap.instance.MoveMonsterMap();
        if (!die)
        {
            Move();
            Jump();
            PlayerUtill.instance.MoveRotate(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
        }
        //��Ŭ��
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)))
        {
            playableWeapon.FireGun(gunIndex);
        }
        //��Ŭ��
        else
        {
            playableWeapon.IdleGun();
        }
        
        //Ű �ٿ����� ���� ����
        if (Input.GetKeyDown(KeyCode.Alpha1) && gunIndex != 0)
        {
            gunIndex = 0;
            SelectWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && gunIndex != 1)
        {
            //����
            gunIndex = 1;
            SelectWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && gunIndex != 2)
        {
            //����
            gunIndex = 2;
            SelectWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            playableWeapon.ReloadGun(gunIndex);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameObject obj = Instantiate<GameObject>(ResourcesManager.instance.buffItem);
            obj.transform.position = PlayerUtill.GetRandomMapPos(playerBody.transform.position);
        }
    }
    // ���� ���� �Լ�
    public void SelectWeapon()
    {
        if (playableWeapon != null)
        {
            ObjectPoolManager.ReturnGun(playableWeapon);           // ���� ���� ����
        }
        CreatePlayableWeapon(listWeapon[gunIndex]);                 // ������ ���� ����
    }
    // ������ ���� ���� Ȱ��ȭ �Լ�
    public void CreatePlayableWeapon(string weaponName)
    {
        GameObject selectWeapon = ResourcesManager.instance.GetPlayableWeapon(weaponName);
        if (selectWeapon != null)
        {
            playableWeapon = ObjectPoolManager.GetGun(weaponName);
        }
        else
        {
            Debug.Log("Create����");
        }
    }
    // �÷��̾� ĳ���� ���� �Լ�
    public void CreateCharacter(string _name)
    {
        GameObject tmpPlayerChar = ResourcesManager.instance.GetChar("Man_Full");
        if (tmpPlayerChar != null)
            playableCharacter = GameObject.Instantiate<GameObject>(tmpPlayerChar, Vector3.zero, Quaternion.identity, playerBody.transform);
        else
            Debug.Log("���� ����");
    }
    // �÷��̾� �̵� �Լ�
    void Move()
    {
        // �÷��̾� �̵�
        moveX = Input.GetAxisRaw("Horizontal");
        moveZ = Input.GetAxisRaw("Vertical");
        // �̵��� �� �� (�ȱ� or �ٱ�)
        if (moveX != 0 || moveZ != 0)
        {
            playableWeapon.IsMove(true);
            bool isRun = false;
            // ���̳� �ڷ� �̵��� ��� �޸� �� ����.
            if (moveZ > 0) isRun = Input.GetKey(keyCodeRun);
            movement.MSpeed = isRun == true ? status.RunSpeed : status.WalkSpeed;
            playableWeapon.IsRun(isRun);
        }
        else
        {
            playableWeapon.IsMove(false);
            playableWeapon.IsRun(false);
        }
        movement.Move(new Vector3(moveX, 0, moveZ));
    }
    // ���� �Լ�
    void Jump()
    {
        if (Input.GetKeyDown(keyCodeJump))
        {
            movement.Jump();
        }
    }
    // �׾��� �� �Լ�
    public void Die()
    {
        die = true;
        playableWeapon.IsDie();
        playerBody.DieRotate();
    }
}
