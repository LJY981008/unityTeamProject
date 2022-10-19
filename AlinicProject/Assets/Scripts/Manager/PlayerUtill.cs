using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUtill : MonoBehaviour
{
    public static PlayerUtill instance; 
    private float rotateY = 0.0f;
    private float rotateX = 0.0f;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
    }
    public void MoveRotate(float rotateSizeX, float rotateSizeY)
    {
        rotateY += rotateSizeY;
        rotateX += rotateSizeX;
        rotateX = Mathf.Clamp(rotateX, -40, 40);
        Quaternion playerQuat = Quaternion.Euler(new Vector3(rotateX, rotateY, 0.0f));
        transform.rotation = Quaternion.Slerp(transform.rotation, playerQuat, Time.deltaTime * 500f);
        Minimap.instance.MovePlayerMapForcus(-rotateY);

    }
    // 맵에서 바닥을 찾는 함수 
    public static Vector3 GetHeightMapPos(Vector3 _origin)
    {
        Vector3 origin = _origin;
        origin.y += 200f;
        RaycastHit hitInfo;
        int layerMask = 1 << LayerMask.NameToLayer("Player");   // 플레이어의 레이어
        layerMask = ~layerMask;                                 // 를 제외
        if (Physics.Raycast(origin, Vector3.down, out hitInfo, Mathf.Infinity, layerMask))
        {
            return hitInfo.point;
        }
        return Vector3.zero;
    }
    //랜덤위치 바닥을 찾는 함수
    public static Vector3 GetRandomMapPos(Vector3 _origin)
    {
        Vector3 origin = _origin;
        float randX = Random.Range(origin.x - 10, origin.x + 10);
        float randZ = Random.Range(origin.z - 10, origin.z + 10);
        origin.x = randX;
        origin.z = randZ;
        origin.y += 200f;
        RaycastHit hitInfo;
        if (Physics.Raycast(origin, Vector3.down, out hitInfo, Mathf.Infinity))
        {
            if (hitInfo.collider.CompareTag("Terrain"))
            {
                return hitInfo.point;
            }
            else
            {
                return GetRandomMapPos(_origin);
            }
        }
        return Vector3.zero;
    }
    public static RaycastHit GetShotEndPos()
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = (1 << LayerMask.NameToLayer("Player")) | (1 << LayerMask.NameToLayer("Bullet"));
        layerMask = ~layerMask;
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask))
        {
            return hitInfo;
        }
        else
        {
            return hitInfo;
        }
        
    }
}
