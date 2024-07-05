using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SingletonMono<Player>
{
    public Dish DishInHand { get; private set; } = null;

    private int Y => Mathf.FloorToInt(transform.position.y);

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (Y >= Const.H - 1)
            {
                //卡住的逻辑
                return;
            }

            //后面可以优化成位移动画
            transform.position += new Vector3(0, 1, 0);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (Y <= 0)
            {
                //卡住的逻辑
                return;
            }

            transform.position += new Vector3(0, -1, 0);
        }

        //取餐
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (DishMgr.Instance.GetDish(out Dish dish, Y))
            {
                DishInHand = dish;
                dish.transform.SetParent(transform);
                dish.transform.localPosition = Vector3.zero;
            }
        }

        //送餐
        if (Input.GetKeyDown(KeyCode.K))
        {
        }

        //销毁
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!DishInHand)
            {
                //手上没东西却硬要销毁的逻辑
                return;
            }

            DishInHand.DeSpawn();
            DishInHand = null;
        }
    }
}