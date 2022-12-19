using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * 게임 매니저
 * 
 */
public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    public float currentDamage = 0;             // 무기의 공격력
    public PlayerBody playerBody;           // 플레이어
    public PlayerUpper playableWeapon;      // 현재 사용 중인 무기
    public GameObject monster;              // 몬스터 오브젝트
    public bool isStart = false;
    [Header("Input KeyCodes")]
    [SerializeField]
    private KeyCode keyCodeRun = KeyCode.LeftShift; // 달리기 키
    private KeyCode keyCodeJump = KeyCode.Space;    // 점프 키
    private Movement movement;              // 키보드 입력으로 플레이어 이동, 점프
    private Status status;                  // 이동속도 등의 플레이어 정보
    public int gunIndex = 1;               // 사용할 무기의 값
    private float moveX, moveZ;             // 이동 값
    private float disPlayerToMonster;       // 플레이어와 보스 사이의 거리
    private bool die = false;               // 사망 트리거
    private string[] listWeapon = { "Rifle", "Pistol", "Shotgun" };    // 무기 목록
    private GameObject playableCharacter;   // 플레이어의 캐릭터
    private int buffItemSpawnCoolTime = 9999;
    public bool isSpawnItem = false;
    public int shotCount;
    public float additionalDamage;         // 추가 %데미지
    public int plusDamage;               // 추가 +데미지
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
        SelectWeapon();    // 기본무기 권총
        playableCharacter.transform.localPosition = Vector3.zero;   // 캐릭터의 위치
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
            
            //좌클릭
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
            //우클릭
            else
            {
                shotCount = 0;
                playableWeapon.IdleGun();
            }

            //키 다운으로 무기 선택
            if (Input.GetKeyDown(KeyCode.Alpha1) && gunIndex != 0)
            {
                gunIndex = 0;
                SelectWeapon();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && gunIndex != 1)
            {
                //권총
                gunIndex = 1;
                SelectWeapon();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && gunIndex != 2)
            {
                //샷건
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
                    Debug.Log("스폰");
                }
            }
            catch(UnityException e)
            {
                Debug.Log(e.Message);
            }
        }
        //여기에 버프 스폰 알림 자막 작성
    }

    // 무기 선택 함수
    public void SelectWeapon()
    {
        isSelect = false;
        if (playableWeapon != null)
        {
            ObjectPoolManager.ReturnGun(playableWeapon);           // 이전 무기 리턴
        }
        CreatePlayableWeapon(listWeapon[gunIndex]);                 // 선택한 무기 생성
    }
    // 선택한 무기 씬에 활성화 함수
    public void CreatePlayableWeapon(string weaponName)
    {
        GameObject selectWeapon = ResourcesManager.instance.GetPlayableWeapon(weaponName);
        if (selectWeapon != null)
        {
            playableWeapon = ObjectPoolManager.GetGun(weaponName);
        }
        else
        {
            Debug.Log("Create실패");
        }
        
    }
    // 플레이어 캐릭터 생성 함수
    public void CreateCharacter(string _name)
    {
        GameObject tmpPlayerChar = ResourcesManager.instance.GetChar("Man_Full");
        if (tmpPlayerChar != null)
            playableCharacter = GameObject.Instantiate<GameObject>(tmpPlayerChar, Vector3.zero, Quaternion.identity, playerBody.transform);
        else
            Debug.Log("생성 실패");
    }
    // 플레이어 이동 함수
    void Move()
    {
        // 플레이어 이동
        moveX = Input.GetAxisRaw("Horizontal");
        moveZ = Input.GetAxisRaw("Vertical");
        // 이동중 일 때 (걷기 or 뛰기)
        if (moveX != 0 || moveZ != 0)
        {
            playableWeapon.IsMove(true);
            bool isRun = false;
            // 옆이나 뒤로 이동할 대는 달릴 수 없다.
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
    // 점프 함수
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
    // 죽었을 때 함수
    public void Die()
    {
        die = true;
        playableWeapon.IsDie();
        playerBody.DieRotate();
    }
}
