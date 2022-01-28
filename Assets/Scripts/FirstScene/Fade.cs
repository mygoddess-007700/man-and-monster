using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    public static Fade instance;
    public Animator anim;

    void Awake()
    {
        instance = this;    
    }

    void Start()
    {
        GameObject.DontDestroyOnLoad(this.gameObject); 
    }

    public void LoadNextImg(Sprite img, Image tImage)
    {
        StartCoroutine(LoadImg(img, tImage));
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex+1));
    }

    public void LoadBeginScene()
    {
        StartCoroutine(LoadScene(0));
    }

    public void LoadEndScene()
    {
        StartCoroutine(LoadDefeat());
    }

    IEnumerator LoadDefeat()
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(LoadScene(5));
    }

    IEnumerator LoadScene(int index)
    {
        anim.CrossFade("FadeOut", 0);

        yield return new WaitForSeconds(0.95f);

        AsyncOperation async = SceneManager.LoadSceneAsync(index);
        async.completed += OnLoadScene;
    }
    
    IEnumerator LoadImg(Sprite img, Image tImage)
    {
        anim.CrossFade("FadeOut", 0);

        yield return new WaitForSeconds(0.95f);

        tImage.sprite = img;
        anim.CrossFade("FadeIn", 0);
    }

    private void OnLoadScene(AsyncOperation obj)
    {
        anim.CrossFade("FadeIn", 0);
    }
}
