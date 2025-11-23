using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    private static SceneController instance;
    public static SceneController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SceneController>();
                if (instance == null)
                {
                    Debug.Log("No SceneController");
                }
            }
            return instance;
        }
    }

    public enum NowScene
    {
        Start = 0,
        SkillTree = 1,
        Game = 2
    }

    [Header("当前场景")]
    public NowScene nowscene;
    [Header("切换场景过渡遮罩")]
    public GameObject sceneshader;
    [Header("淡入时间")]
    public float fadeintime;
    [Header("中间态过渡时间")]
    public float fadetime;
    [Header("淡出时间")]
    public float fadeouttime;

    void Start()
    {
        sceneshader.SetActive(false);
        sceneshader.GetComponent<CanvasGroup>().alpha = 0;
        nowscene=(NowScene)SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        
    }

    public void ToScene(int id)
    {
        sceneshader.SetActive(true);
        sceneshader = GameObject.Find("SceneShader");
        sceneshader.GetComponent<CanvasGroup>().alpha = 0;
        StartCoroutine(SceneShaderFade(id));
    }

    IEnumerator SceneShaderFade(int id)
    {
        float time = 0;
        while (sceneshader.GetComponent<CanvasGroup>().alpha < 1)
        {
            time+= Time.deltaTime;
            sceneshader.GetComponent<CanvasGroup>().alpha=Mathf.Lerp(0,1, time / fadeintime);
            yield return null;
        }
        sceneshader.GetComponent<CanvasGroup>().alpha = 1;

        SceneManager.LoadScene(id);
        nowscene = (NowScene)SceneManager.GetActiveScene().buildIndex;
        yield break;
    }
}
