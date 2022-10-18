using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TalkPanelScript : MonoBehaviour
{
    [SerializeField]
    private Text textUI;
    [SerializeField]
    private Button nextTextBT, LoadSceneBT;

    bool isFinish;
    int talkCount;

    private void Awake()
    {
        isFinish = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TalkText("asdasdasdasdasd"));
    }

    // Update is called once per frame
    void Update()
    {
        if (isFinish)
        {
            if (Input.GetMouseButton(0))
            {
                StartCor();
            }
        }
    }

    public void StartCor()
    {
        StartCoroutine(TalkText("next texttttttttttt"));
    }

    IEnumerator TalkText(string text)
    {
        isFinish = false;
        nextTextBT.gameObject.SetActive(false);
        int i;
        string talkText = "";
        for (i = 0; i < text.Length; i++)
        {
            talkText += text[i];
            textUI.text = talkText;
            yield return new WaitForSeconds(0.05f);
        }
        if(i >= text.Length)
        {
            isFinish = true;
            talkCount++;
            if (talkCount < 3)
            {
                nextTextBT.gameObject.SetActive(true);
            }
            else
            {
                LoadSceneBT.gameObject.SetActive(true);
            }
        }

    }

    public void LoadScene()
    {
        SceneManager.LoadScene("LoadingScene");
    }


}
