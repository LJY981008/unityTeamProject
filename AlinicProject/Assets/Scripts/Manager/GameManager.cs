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


    public float currentDamage;             // 무기의 공격력
    public PlayerBody playerBody;           // 플레이어
    public PlayerUpper playableWeapon;      // 현재 사용 중인 무기
    public GameObject monster;              // 몬스터 오브젝트

    [Header("Input KeyCodes")]
    [SerializeField]
    private KeyCode keyCodeRun = KeyCode.LeftShift; // 달리기 키
    private KeyCode keyCodeJump = KeyCode.Space;    // 점프 키
    private Movement movement;              // 키보드 입력으로 플레이어 이동, 점프
    private Status status;                  // 이동속도 등의 플레이어 정보
    private int gunIndex = 1;               // 사용할 무기의 값
    private float moveX, moveZ;             // 이동 값
    private float disPlayerToMonster;       // 플레이어와 보스 사이의 거리
    private bool die = false;               // 사망 트리거
    private string[] listWeapon = { "Rifle", "Pistol", "Shotgun" };    // 무기 목록
    private GameObject playableCharacter;   // 플레이어의 캐릭터
    
    

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
        SelectWeapon();    // 기본무기 권총
        playableCharacter.transform.localPosition = Vector3.zero;   // 캐릭터의 위치
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
        //좌클릭
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)))
        {
            playableWeapon.FireGun(gunIndex);
        }
        //우클릭
        else
        {
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
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameObject obj = Instantiate<GameObject>(ResourcesManager.instance.buffItem);
            obj.transform.position = PlayerUtill.GetRandomMapPos(playerBody.transform.position);
        }
    }
    // 무기 선택 함수
    public void SelectWeapon()
    {
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
    // 점프 함수
    void Jump()
    {
        if (Input.GetKeyDown(keyCodeJump))
        {
            movement.Jump();
        }
    }
    // 죽었을 때 함수
    public void Die()
    {
        die = true;
        playableWeapon.IsDie();
        playerBody.DieRotate();
    }
}
