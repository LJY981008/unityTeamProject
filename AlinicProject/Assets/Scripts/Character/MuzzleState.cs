using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleState : MonoBehaviour
{
    // �ѱ��Ǻ���ġ
    public Vector3 muzzlePos;
    public Vector3 GetMuzzlePos()
    {
        muzzlePos = transform.localPosition;
        return muzzlePos;
    }
}
