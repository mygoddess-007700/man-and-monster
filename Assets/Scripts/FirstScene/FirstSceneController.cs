using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSceneController : MonoBehaviour
{
    public float prepareTime;
    void Start()
    {
        prepareTime = Time.time;
    }

    void Update()
    {
        if (Input.anyKeyDown && Time.time > prepareTime + 2f)
        {
            Fade.instance.LoadNextScene();
        }   
    }
}
