using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Animator anim;
    public bool prepareFinding = false;

    public AudioSource caidian;
    public AudioSource death;

    [Header("[Setting]")]
    // 左右移动速度
    public float moveSpeed;
    public Player player;

    [Header("贴图默认朝向")]
    public Facing facing;

    // 水平运动比例
    private float moveX;
    private MobileHorizontalInputController inputController;

    private float endFindTime = 0;

    // 获取组件
    void Start()
    {
        anim = GetComponent<Animator>();
        
        GameObject directionJoyStick = GameObject.FindGameObjectWithTag("DirectionJoyStick");
        inputController = directionJoyStick.GetComponent<MobileHorizontalInputController>();
    }

    void FixedUpdate()
    {
        // 虚拟轴水平移动
        if (inputController.dragging)
        {
            moveX = inputController.horizontal;
        }
        else
        {
            moveX = Input.GetAxisRaw("Horizontal");
        }

        if (player.isMoving && player.transform.position.x < 50f)
        {
            Vector3 tPos = transform.position;
            tPos.x += moveX * moveSpeed * Time.fixedDeltaTime;
            transform.position = tPos;
        }
    }

    void Update()
    {
        if (prepareFinding && endFindTime < Time.time)
        {
            anim.SetBool("Finding", true);
            endFindTime = Time.time + 1f;
            StartCoroutine(JudgeDeath());
            prepareFinding = false;
        }
    }

    IEnumerator JudgeDeath()
    {
        yield return new WaitForSeconds(1f);
        if (!player.isInvincible)
        {
            death.Play();
            GameController.instance.GameDefeat();
        }
        else
        {
            caidian.Stop();
        }
    }
}
