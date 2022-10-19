using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileEffectScript : MonoBehaviour
{
    [SerializeField]
    private SphereCollider EffectColider;

    int _damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerBody player = InitMonster.Instance.target.GetComponent<PlayerBody>();
            Debug.Log(player.name + " hit");
            player.OnDamage(_damage);
            EffectColider.gameObject.SetActive(false);
        }
    }
}
