using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public GameObject [] buffs; // 생성할 버프들
    ResourcesManager rm;
    public Transform playerTransform; // 플레이어의 트랜스폼
    public float maxDistance;// 플레이어 위치로부터 아이템이 배치될 최대 반경
    public float timeBetSpawnMax, timeBetSpawnMin; // 최대, 최소 시간 간격
    private float timeBetSpawn, lastSpawnTime; // 생성 간격, 마지막 생성 시점
    void Awake()
    {
        maxDistance = 10f;
        timeBetSpawnMax = 10f;
        timeBetSpawnMin = 3f;
        // 생성 간격과 생성 시점 초기화
        timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
        lastSpawnTime = 0f;
    }    
    // 주기적으로 아이템 생성 처리 실행
    void Update()
    {
        // 현래 시점에 마지막 생성 시점에서 생성 주기 이상 지남
        // 플레이어 캐릭터가 존재함
        if (Time.time >= lastSpawnTime + timeBetSpawn && playerTransform != null)
        {
            // 마지막 생성 시간 갱신
            lastSpawnTime = Time.time;
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
            // 아이탬 생성 실행
            Spawn();
        }
    }
    private void Spawn()
    {
        {
        // 플레이어 근처에서 랜덤 위치 가져오기
        Vector3 spawnPosition = PlayerUtill.GetRandomMapPos(playerTransform.position);
        // 아이템 중 하나를 무작위로 골라 랜덤 위치에 생성
        GameObject selectedbuff = buffs[Random.Range(0, buffs.Length)];
        GameObject buff = Instantiate(selectedbuff, spawnPosition, Quaternion.identity);
        }
    }
}
