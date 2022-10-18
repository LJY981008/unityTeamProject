using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoad : MonoBehaviour
{
    [SerializeField]
    private Slider progressbar;
    [SerializeField]
    private Text loadtext;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        StartCoroutine(LoadScene());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator LoadScene()
    {
        Debug.Log("loading");
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync("CityBuilderUrbanAPODemo");
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            yield return null;
            if (progressbar.value < 1f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
            }
            else
            {
                loadtext.text = "Press Spacebar to Play";
            }

            if (Input.GetKeyDown(KeyCode.Space) && progressbar.value >= 1f && op.progress >= 0.9f)
            {
                op.allowSceneActivation = true;
            }
        }
    }
}
