using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * �ۼ��� : ���ؿ�
 * ������ ���� : 2022-08-17
 * ���� : ���ӸŴ���. ���� ����, ���� ����
 */
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int gunIndex = 1;                        // �⺻�� ��

    public float CurrentDamage;
    public PlayerBody playerBox;
    public GameObject monster;

    PlayerChar playerChar;
    GameObject playerDowner;
    private string[] listWeapon = { "Rifle", "Pistol", "Shotgun" };    // ���� ���
    private PlayerUpper playableWeapon;                      // ���� ��� ���� ����
    private float disPlayerToMonster;

    [Header("Input KeyCodes")]
    [SerializeField]
    private KeyCode keyCodeRun = KeyCode.LeftShift; // �޸��� Ű
    private KeyCode keyCodeJump = KeyCode.Space;    // ���� Ű
    private Movement movement; // Ű���� �Է����� �÷��̾� �̵�, ����
    private Status status; // �̵��ӵ� ���� �÷��̾� ����
    private float x, z;
    void Awake()
    {
        instance = this;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        movement = playerBox.GetComponent<Movement>();
        status = playerBox.GetComponent<Status>();
    }
    private void Start()
    {
        CreateCharacter("Man_Full");
        SelectWeapon();    // �⺻���� ����
        playerDowner.transform.localPosition = Vector3.zero;
    }
    private void Update()
    {
        disPlayerToMonster = Vector3.Distance(playerBox.transform.position, monster.transform.position);
        UIManager.instance.SetEnableBossHp(disPlayerToMonster);
        Minimap.instance.MoveMonsterMap();
        Move();
        Jump();
        PlayerUtill.instance.MoveRotate(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
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
            obj.transform.position = PlayerUtill.GetRandomMapPos(playerBox.transform.position);
        }
    }
    public void SelectWeapon()
    {
        if (playableWeapon != null)
        {
            ObjectPoolManager.ReturnGun(playableWeapon);           // ���� ���� ������Ʈ �� �����͵� ����
        }
        CreatePlayableWeapon(listWeapon[gunIndex]);        // ������ ���� ����
    }
    // ���ϴ� ���� ���� ����
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
    public void CreateCharacter(string _name)
    {
        // ĳ���͸� �����ϴ� �ڵ带 �ۼ�
        GameObject tmpPlayerChar = ResourcesManager.instance.GetChar("Man_Full");
        
        if (tmpPlayerChar != null)
        {
            //playerChar = Instantiate(tmpPlayerChar, Vector3.zero, Quaternion.identity);
            // ĳ���� ���ӿ�����Ʈ�� ����Ʈ���� �ҷ����� �ڵ�
            playerDowner = GameObject.Instantiate<GameObject>(tmpPlayerChar, Vector3.zero, Quaternion.identity, playerBox.transform);
            /*playerDowner.transform.localPosition = Vector3.zero;
            playerDowner.transform.localRotation = Quaternion.identity;
            playerDowner.transform.SetParent(playerBox.transform);*/
        }
        else
        {
            Debug.Log("���� ����");
        }
    }
    void Move()
    {
        // �÷��̾� �̵�
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        // �̵��� �� �� (�ȱ� or �ٱ�)
        if (x != 0 || z != 0)
        {
            bool isRun = false;
            // ���̳� �ڷ� �̵��� ��� �޸� �� ����.
            if (z > 0) isRun = Input.GetKey(keyCodeRun);
            movement.MSpeed = isRun == true ? status.RunSpeed : status.WalkSpeed;
        }
        movement.Move(new Vector3(x, 0, z));
    }
    void Jump()
    {
        if (Input.GetKeyDown(keyCodeJump))
        {
            movement.Jump();
        }
    }
}
