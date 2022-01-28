using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginDialog : MonoBehaviour
{
    public Sprite sprite;
    public Image image;

    private float prepareTime;

    private bool loadImg = true;
    private bool loadScene = false;

    void Start()
    {
        prepareTime = Time.time;
    }

    void Update()
    {
        if (Input.anyKeyDown && loadScene)
        {
            Fade.instance.LoadNextScene();
            loadScene = false;
        }
        if (Input.anyKeyDown && Time.time > prepareTime + 2f && loadImg)
        {
            Fade.instance.LoadNextImg(sprite, image);
            Invoke("LoadSceneToTrue", 2f);
            loadImg = false;
        }   
    }

    void LoadSceneToTrue()
    {
        loadScene = true;
    }
}
