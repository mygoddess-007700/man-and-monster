using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Retry : MonoBehaviour
{
    public Button retryBtn;

    void Awake()
    {
        retryBtn.onClick.AddListener(RetryGame);
    }

    void RetryGame()
    {
        Fade.instance.LoadBeginScene();
    }
}
