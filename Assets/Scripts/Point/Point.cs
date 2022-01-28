using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public Monster monster;
    public float refreshDuration = 5f;
    public AudioSource caidian;

    private float refreshTime = 0;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (refreshTime > Time.time)
        {
            return;
        }
        if (other.gameObject.tag == "Player")
        {
            caidian.Play();
            monster.prepareFinding = true;
            refreshTime = Time.time + refreshDuration;
        }    
    }
}
