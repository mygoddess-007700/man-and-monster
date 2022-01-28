using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // For GroundSensor
    public Rigidbody2D rb;
    public int currentJumpCount = 0;
    public bool grounded = true;
    public bool isMoving = false;
    public bool isInvincible = false;

    public Animator anim;

    [Header("实例化的ButtonClickController脚本")]
    public ButtonClickController jumpBtn;

    [Header("[Setting]")]
    // 左右移动速度
    public float moveSpeed;
    // 跳跃升力
    public float jumpForce;
    // 最大跳跃次数
    public int maxJumpCount;

    [Header("贴图默认朝向")]
    public Facing facing;

    [Header("音频")]
    public AudioSource door;
    // public AudioSource jumpAudio;
    // public AudioSource runAudio;


    // 水平运动比例
    private float moveX;
    private MobileHorizontalInputController inputController;

    // 获取组件
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        GameObject directionJoyStick = GameObject.FindGameObjectWithTag("DirectionJoyStick");
        inputController = directionJoyStick.GetComponent<MobileHorizontalInputController>();
    }

    void FixedUpdate()
    {
        if (moveX != 0 && (transform.position.x > -8f) && (transform.position.x < 48f))
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (jumpBtn.pressed || Input.GetButtonDown("Jump"))
        {
            if (currentJumpCount < maxJumpCount)
            {
                jumpBtn.pressed = false;
                // 跳跃行为
                Jump();
            }
        }

        // 虚拟轴水平移动
        if (inputController.dragging)
        {
            moveX = inputController.horizontal;
        }
        else
        {
            moveX = Input.GetAxisRaw("Horizontal");
        }

        // 左右水平移动（因为想要实现只有攻击和翻滚不能移动，其它情况下可以移动，且空中移动播放空中动画）
        if (moveX == 0)
        {
            anim.SetBool("Running", false);
            // StopAudio(runAudio);
        }
        else
        {
            if (grounded)
            {
                anim.SetBool("Running", true);
            }

            Flip(moveX > 0);

            Vector2 tPos = transform.position;
            tPos.x += moveX * moveSpeed * Time.fixedDeltaTime;
            if (transform.position.x > 69.5f && tPos.x < 69.5f)
            {
                door.Play();
                return;
            }
            if (transform.position.x > 50f && tPos.x < 50f)
            {
                return;
            }
            transform.position = tPos;
        }
    }

    void Jump()
    {
        // PlayAudio(jumpAudio);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        currentJumpCount++;
        anim.SetTrigger("Jump");
    }

    /// <summary>
    /// 会根据贴图方向通过传入的水平移动方向参数进行翻转
    /// </summary>
    /// <param name="right">正在向x轴正方向移动</param>
    void Flip(bool right)
    {
        if (facing == Facing.Left)
        {
            right = !right;
        }
        
        float next = right ? 0 : 180;
        if (transform.rotation.eulerAngles.y != next)
        {
            transform.rotation = Quaternion.Euler(0, next, 0);
        }
    }

    float GetNextTime(float offset)
    {
        return Time.time + offset;
    }

    void PlayAudio(AudioSource t)
    {
        if (t != null && !t.isPlaying)
        {
            t.Play();
        }
    }

    void StopAudio(AudioSource t)
    {
        if (t != null && t.isPlaying)
        {
            t.Stop();
        }
    }
}
