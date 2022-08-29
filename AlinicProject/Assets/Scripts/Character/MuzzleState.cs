using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleState : MonoBehaviour
{
    // ÃÑ±¸ÇÇº¿À§Ä¡
    public Vector3 muzzlePos;
    public Vector3 GetMuzzlePos()
    {
        muzzlePos = transform.localPosition;
        return muzzlePos;
    }
}
