using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    void Start()
    {
        instance = this;    
    }

    public void GameDefeat()
    {
        Fade.instance.LoadEndScene();
    }
}
