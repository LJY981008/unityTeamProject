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


    public float currentDamage = 0;             // ������ ���ݷ�
    public PlayerBody playerBody;           // �÷��̾�
    public PlayerUpper playableWeapon;      // ���� ��� ���� ����
    public GameObject monster;              // ���� ������Ʈ
    public bool isStart = false;
    [Header("Input KeyCodes")]
    [SerializeField]
    private KeyCode keyCodeRun = KeyCode.LeftShift; // �޸��� Ű
    private KeyCode keyCodeJump = KeyCode.Space;    // ���� Ű
    private Movement movement;              // Ű���� �Է����� �÷��̾� �̵�, ����
    private Status status;                  // �̵��ӵ� ���� �÷��̾� ����
    public int gunIndex = 1;               // ����� ������ ��
    private float moveX, moveZ;             // �̵� ��
    private float disPlayerToMonster;       // �÷��̾�� ���� ������ �Ÿ�
    private bool die = false;               // ��� Ʈ����
    private string[] listWeapon = { "Rifle", "Pistol", "Shotgun" };    // ���� ���
    private GameObject playableCharacter;   // �÷��̾��� ĳ����
    private int buffItemSpawnCoolTime = 9999;
    public bool isSpawnItem = false;
    public int shotCount;
    public float additionalDamage;         // �߰� %������
    public int plusDamage;               // �߰� +������
    public float plusSpeed;
    public float buffSpeed;
    public float prevDamage;
    public bool isSkill;
    public bool isSelect;
    public Dictionary<string, float> currentSkillCoolTime = new Dictionary<string, float>();
    private BuffItem tmp;
    void Awake()
    {
        instance = this;
        plusSpeed = 1.0f;
        buffSpeed = 1.0f;
        additionalDamage = 1.0f;
        plusDamage = 0;
        prevDamage = currentDamage;
        shotCount = 0;
        isSkill = false;
        isSelect = false;
        movement = playerBody.GetComponent<Movement>();
        status = playerBody.GetComponent<Status>();
    }
    private void Start()
    {
        CreateCharacter("Man_Full");
        SelectWeapon();    // �⺻���� ����
        playableCharacter.transform.localPosition = Vector3.zero;   // ĳ������ ��ġ
        StartCoroutine(SpawnBuff());
    }
    private void Update()
    {

        if (shotCount < 15)
        {
            currentDamage = prevDamage;
        }
        if (currentSkillCoolTime[listWeapon[0]] > 0)
        {
            currentSkillCoolTime[listWeapon[0]] -= Time.deltaTime;
        }
        else
        {
            currentSkillCoolTime[listWeapon[0]] = 0;
        }
        if (currentSkillCoolTime[listWeapon[1]] > 0)
        {
            currentSkillCoolTime[listWeapon[1]] -= Time.deltaTime;
        }
        else
        {
            currentSkillCoolTime[listWeapon[1]] = 0;
        }
        if (currentSkillCoolTime[listWeapon[2]] > 0)
        {
            currentSkillCoolTime[listWeapon[2]] -= Time.deltaTime;
        }
        else
        {
            currentSkillCoolTime[listWeapon[2]] = 0;
        }
        if (isStart && !isSkill)
        {
            if (!die)
            {
                Move();
                Jump();
                PlayerUtill.instance.MoveRotate(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
            }
            disPlayerToMonster = Vector3.Distance(playerBody.transform.position, monster.transform.position);
            UIManager.instance.SetEnableBossHp(disPlayerToMonster);
            Minimap.instance.MoveMonsterMap();
            
            //��Ŭ��
            if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)))
            {
                playableWeapon.FireGun(gunIndex);
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                if (UIManager.instance.imageSkillPanel.fillAmount >= 1)
                {
                    isSkill = true;
                    currentSkillCoolTime[listWeapon[gunIndex]] = playableWeapon.gunData.skillCoolTime;
                    PlayerSkill.instance.gunIndex = gunIndex;
                    PlayerSkill.instance.Skill();
                    UIManager.instance.SkillEvent(listWeapon[gunIndex]);
                }
            }
            //��Ŭ��
            else
            {
                shotCount = 0;
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

            if (Input.GetKeyDown(KeyCode.V))
            {
                if (UIManager.instance.imageUltiIcon.fillAmount >= 1f)
                {
                    UltiSkill.instance.StartUltiArea();
                    UIManager.instance.UltiEvent();
                }
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                if (tmp != null)
                    ObjectPoolManager.ReturnItem(tmp);
                tmp = ObjectPoolManager.GetItem();
                Debug.Log("aa");
            }

        }
    }
    private IEnumerator SpawnBuff()
    {
        
        while (true) {
            yield return new WaitForSeconds(buffItemSpawnCoolTime);
            try
            {
                if (!isSpawnItem && isStart)
                {
                    UIManager.instance.isSpawnItem = true;
                    UIManager.instance.DoCoroutine("ViewSpawnItemText");
                    isSpawnItem = true;
                    ObjectPoolManager.GetItem();
                    Debug.Log("����");
                }
            }
            catch(UnityException e)
            {
                Debug.Log(e.Message);
            }
        }
        //���⿡ ���� ���� �˸� �ڸ� �ۼ�
    }

    // ���� ���� �Լ�
    public void SelectWeapon()
    {
        isSelect = false;
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
            movement.MSpeed = isRun == true ? status.RunSpeed * plusSpeed * buffSpeed : status.WalkSpeed * plusSpeed * buffSpeed;
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
    public IEnumerator CoolTime(int gunIndex)
    {
        while (currentSkillCoolTime[listWeapon[gunIndex]] > 0) {
            currentSkillCoolTime[listWeapon[gunIndex]]--;
            yield return new WaitForSecondsRealtime(1f);
        }
    }
    public void setStop()
    {
        isStart = false;
        moveX = 0f;
        moveZ = 0f;
        playableWeapon.IdleGun();
    }
    public void setStart()
    {
        isStart = true;
    }
    // �׾��� �� �Լ�
    public void Die()
    {
        die = true;
        playableWeapon.IsDie();
        playerBody.DieRotate();
    }
}
