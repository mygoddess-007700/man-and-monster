using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMove : MonoBehaviour
{
    [Header("[Setting]")]
    // 左右移动速度
    public float moveSpeed;
    public Player player;

    // 水平运动比例
    private float moveX;
    private MobileHorizontalInputController inputController;

    // 获取组件
    void Start()
    {        
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

        if (player.isMoving && player.transform.position.x > 10f && player.transform.position.x < 40f)
        {
            Vector3 tPos = transform.position;
            tPos.x += moveX * moveSpeed * Time.fixedDeltaTime;
            transform.position = tPos;
        }
    }
}
