using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Player : SingletonMono<Player>
{
    //手上的菜品
    public Dish DishInHand { get; private set; } = null;

    //玩家位置当前处在的Y轴值
    private int Y => Mathf.FloorToInt(transform.position.y);

    //玩家目前得到的积分
    private int score;
    public int Score { get{return score;} set{} }

    //血条
    private float health;
    public float Health
    {
        get { return health; }
        set
        {
            health = value; 
            if (health <= 0) { health = 0; GameOver(); }
            GamePanel_ gameui=UIManager.Instance.GetPanel<GamePanel_>();
            if(gameui)gameui.health.value=health/100;
        }
    }

    private void Update()
    {
        if (health <= 0) return;
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                UIManager.Instance.HidePanel<StopPanel>();
            }
            else
            {
                Time.timeScale = 0;
                UIManager.Instance.ShowPanel<StopPanel>();
            }
        }
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
    private void GameOver()
    {
        UIManager.Instance.ShowPanel<OverPanel>();
    }
}

