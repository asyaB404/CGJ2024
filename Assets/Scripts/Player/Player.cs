using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Player : SingletonMono<Player>
{
<<<<<<< HEAD
=======
    public float GameTime { get; private set; }

>>>>>>> origin/main
    //手上的菜品
    public Dish DishInHand { get; private set; } = null;

    //玩家位置当前处在的Y轴值
    private int Y => Mathf.FloorToInt(transform.position.y);

    //玩家目前得到的积分
    public int Score { get; set; }

    //血条
    public float Health { get; set; }

    public int Combo { get; private set; }

    public void AddCombo()
    {
        Combo += 1;
    }

    private void Update()
    {
        GameTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (Y >= Const.H - 1)
            {
                //卡住的逻辑  可以用个动画或音效来表现？
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
                dish.transform.DOKill(true);
                DishInHand = dish;
                dish.transform.SetParent(transform);
                dish.transform.localPosition = new Vector3(0, -0.01f, 0);
            }
        }

        //送餐
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (DishInHand)
            {
                if (DishInHand && CustomerMgr.Instance.SendDishToCustomer(DishInHand, Y))
                {
                    //送餐成功
                }
                else
                {
                    //送餐失败
                }

                DishInHand.DeSpawn();
                DishInHand = null;
            }
            else
            {
                //手上没东西还硬送的逻辑
            }
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
<<<<<<< HEAD

=======
>>>>>>> origin/main
}