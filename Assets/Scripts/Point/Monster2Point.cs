using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2Point : MonoBehaviour
{
    public Monster2 monster2;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            monster2.anim.SetTrigger("Find");
            StartCoroutine(MoveMonster2());
        }    
    }

    IEnumerator MoveMonster2()
    {
        yield return new WaitForSeconds(0.5f);
        monster2.isMoving = true;
    }
}
