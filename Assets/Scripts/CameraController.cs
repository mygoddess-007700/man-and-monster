using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    // public Transform player;
    public Transform target;
    // public Transform monster;
    // public int index = 1;
    // public float map1LX = -9.5f;
    // public float map1RX = 10f;
    // public float map2LX = 10f;
    // public float map2RX = 30f;
    // public float map3LX = 30f;
    // public float map3RX = 50f;
    public int index = 1;
    public float map1RX = 48f;
    public float map2RX = 68f;
    public float minPosX;
    public float maxPosX;

    public float smoothTime = 0.05f;

    public AudioSource Scene1;
    public AudioSource Scene2;
    public AudioSource caidian;

    void Start()
    {
        instance = this;
    }

    void FixedUpdate()
    {
        if (target.position.x > map1RX && index == 1)
        {
            minPosX = map1RX + 10f;
            maxPosX = minPosX;
            index = 2;
        }

        if (target.position.x < minPosX && index == 2)
        {
            Vector3 tPos = new Vector3(minPosX, transform.position.y, -100);
            transform.position = Vector3.Lerp(transform.position, tPos, Time.deltaTime * smoothTime * 100);
            Scene1.Stop();
            Scene2.Play();
        }

        if (target.position.x > map2RX && index == 2)
        {
            minPosX = map2RX + 10f;
            maxPosX = minPosX;
            index = 3;
        }

        if (target.position.x < minPosX && index == 3)
        {
            Vector3 tPos = new Vector3(minPosX, transform.position.y, -100);
            transform.position = Vector3.Lerp(transform.position, tPos, Time.deltaTime * smoothTime * 100);
            caidian.Stop();
        }

        Vector3 targetPos = new Vector3(target.position.x, transform.position.y, -100);
        if (targetPos.x > minPosX && targetPos.x < maxPosX)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * smoothTime * 100);
        }
        // if (player.position.x > map1LX && player.position.x < map1RX)
        // {
        //     Vector3 targetPos = new Vector3((map1LX + map1RX)/2, target.transform.position.y, -100);
        //     transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * smoothTime * 100);
        //     if (player.position.x > (map1LX + map1RX)/2 && index != 1)
        //     {
        //         Vector3 monsterPos = new Vector3(monster.transform.position.x - 6f, monster.transform.position.y, monster.transform.position.z);
        //         monster.transform.position = monsterPos;
        //     }
        //     index = 1;
        // }
        // if (player.position.x > map2LX && player.position.x < map2RX)
        // {
        //     Vector3 targetPos = new Vector3((map2LX + map2RX)/2, target.transform.position.y, -100);
        //     transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * smoothTime * 100);
        //     if (player.position.x < (map2LX + map2RX)/2 && index != 2)
        //     {
        //         Vector3 monsterPos = new Vector3(monster.transform.position.x + 6f, monster.transform.position.y, monster.transform.position.z);
        //         monster.transform.position = monsterPos;
        //     }
        //     else if (index != 2)
        //     {
        //         Vector3 monsterPos = new Vector3(monster.transform.position.x - 20f, monster.transform.position.y, monster.transform.position.z);
        //         monster.transform.position = monsterPos;
        //     }
        //     index = 2;
        // }
        // if (player.position.x > map3LX && player.position.x < map3RX)
        // {
        //     Vector3 targetPos = new Vector3((map3LX + map3RX)/2, target.transform.position.y, -100);
        //     transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * smoothTime * 100);
        //     if (player.position.x < (map3LX + map3RX)/2 && index != 3)
        //     {
        //         Vector3 monsterPos = new Vector3(monster.transform.position.x + 20f, monster.transform.position.y, monster.transform.position.z);
        //         monster.transform.position = monsterPos;
        //     }
        //     index = 3;
        // }
    }
}
