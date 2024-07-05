using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Dish DishInHand { get; private set; } = null;

    private int Y
    {
        get => Mathf.FloorToInt(transform.position.y);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (Y <= 0)
            {
                //卡住的逻辑
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (Y >= Const.H - 1)
            {
                //卡住的逻辑
                return;
            }
        }

        //取餐
        if (Input.GetKeyDown(KeyCode.J))
        {
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