using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2 : MonoBehaviour
{
    //玩家脚本
    public Player player;
    public bool isMoving = false;

    public Rigidbody2D rb;
    public Animator anim;

    public AudioSource death;
    public AudioSource caidian;

    [Header("Setting")]
    public float speed = 6f;
    public float attackConsciousnessRange = 7f;
    public float findDuration = 5f;

    [Header("贴图默认朝向")]
    public Facing facing;

    [Header("移动判定")]
    public bool attackConsciousness = false;
    public bool finding = false;

    private float playerDistance;
    private bool playing = false;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //时刻获取碰撞状态
        rb.WakeUp();

        Vector2 dir;
        dir = player.transform.position - transform.position;
        playerDistance = dir.magnitude;

        attackConsciousness = (playerDistance <= attackConsciousnessRange);

        if (attackConsciousness && !finding)
        {
            StartCoroutine(FindPlayer());
            finding = true;
        }

        if (isMoving && transform.position.x < 65.5f)
        {
            caidian.Play();
            Flip((player.transform.position.x - transform.position.x) > 0);

            Vector2 target = new Vector2(player.transform.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }
    }

    public IEnumerator FindPlayer()
    {
        yield return new WaitForSeconds(findDuration);
        if (attackConsciousness)
        {
            anim.SetTrigger("Find");
            yield return new WaitForSeconds(0.5f);
            isMoving = true;
        }
        finding = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (isMoving && !playing)
            {
                playing = true;
                death.Play();
                GameController.instance.GameDefeat();
            }
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (isMoving && !playing)
            {
                death.Play();
                GameController.instance.GameDefeat();
            }
        }
    }

    /// <summary>
    /// 会根据贴图方向通过传入的水平移动方向参数进行翻转
    /// </summary>
    /// <param name="right">正在向x轴正方向移动</param>
    public void Flip(bool right)
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
}
