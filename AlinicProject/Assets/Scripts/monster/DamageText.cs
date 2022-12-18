using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private float moveSpeed;
    private float alphaSpeed;
    private float destroyTime;
    TextMeshProUGUI text;
    Color alpha;
    public int damage;

    private float x;
    private float y;
    private float z;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 40.0f;
        alphaSpeed = 2.0f;
        destroyTime = 2.0f;

        text = GetComponent<TextMeshProUGUI>();
        alpha = text.color;
        text.text = damage.ToString();

        x = 200 + InitMonster.Instance.random.Next(100);
        y = 80;
        z = -30;

        transform.localPosition = new Vector3(x, y, z);
        transform.rotation = new Quaternion();
        transform.localScale = new Vector3(1, 1, 1);

        Invoke("DestroyObject", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {

        // Debug.Log("X : " + transform.position.x);
        // Debug.Log("Y : " + transform.position.y);
        // Debug.Log("Z : " + transform.position.z);

        // Debug.Log("L X : " + transform.localPosition.x);
        // Debug.Log("L Y : " + transform.localPosition.y);
        // Debug.Log("L Z : " + transform.localPosition.z);

        Vector3 moveVector = new Vector3(0, moveSpeed * Time.deltaTime, moveSpeed * Time.deltaTime);

        transform.localPosition += moveVector;

        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // 텍스트 알파값
        text.color = alpha;

    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
