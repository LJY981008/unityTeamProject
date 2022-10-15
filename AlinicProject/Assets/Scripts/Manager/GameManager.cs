using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * 작성자 : 이준영
 * 마지막 수정 : 2022-08-17
 * 내용 : 게임매니저. 무기 생성, 무기 변경
 */
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int gunIndex = 1;                        // 기본총 값

    public float CurrentDamage;
    public PlayerBody playerBox;
    public GameObject monster;

    PlayerChar playerChar;
    GameObject playerDowner;
    private string[] listWeapon = { "Rifle", "Pistol", "Shotgun" };    // 무기 목록
    private PlayerUpper playableWeapon;                      // 현재 사용 중인 무기
    private float disPlayerToMonster;

    [Header("Input KeyCodes")]
    [SerializeField]
    private KeyCode keyCodeRun = KeyCode.LeftShift; // 달리기 키
    private KeyCode keyCodeJump = KeyCode.Space;    // 점프 키
    private Movement movement; // 키보드 입력으로 플레이어 이동, 점프
    private Status status; // 이동속도 등의 플레이어 정보
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
        SelectWeapon();    // 기본무기 권총
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
            obj.transform.position = PlayerUtill.GetRandomMapPos(playerBox.transform.position);
        }
    }
    public void SelectWeapon()
    {
        if (playableWeapon != null)
        {
            ObjectPoolManager.ReturnGun(playableWeapon);           // 이전 무기 오브젝트 및 컴포넌드 제거
        }
        CreatePlayableWeapon(listWeapon[gunIndex]);        // 선택한 무기 생성
    }
    // 원하는 무기 씬에 생성
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
    public void CreateCharacter(string _name)
    {
        // 캐릭터를 생성하는 코드를 작성
        GameObject tmpPlayerChar = ResourcesManager.instance.GetChar("Man_Full");
        
        if (tmpPlayerChar != null)
        {
            //playerChar = Instantiate(tmpPlayerChar, Vector3.zero, Quaternion.identity);
            // 캐릭터 게임오브젝트를 리스트에서 불러오는 코드
            playerDowner = GameObject.Instantiate<GameObject>(tmpPlayerChar, Vector3.zero, Quaternion.identity, playerBox.transform);
            /*playerDowner.transform.localPosition = Vector3.zero;
            playerDowner.transform.localRotation = Quaternion.identity;
            playerDowner.transform.SetParent(playerBox.transform);*/
        }
        else
        {
            Debug.Log("생성 실패");
        }
    }
    void Move()
    {
        // 플레이어 이동
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        // 이동중 일 때 (걷기 or 뛰기)
        if (x != 0 || z != 0)
        {
            bool isRun = false;
            // 옆이나 뒤로 이동할 대는 달릴 수 없다.
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
