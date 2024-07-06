using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Player : SingletonMono<Player>
{
    public float GameTime { get; private set; }


    //手上的菜品
    public Dish DishInHand { get; private set; } = null;

    //玩家位置当前处在的Y轴值
    private int Y => Mathf.FloorToInt(transform.position.y);

    //玩家目前得到的积分
<<<<<<< HEAD
    private int score;
    public int Score { get{return score;} set{
        score=value;
        GamePanel_ gameui=UIManager.Instance.GetPanel<GamePanel_>();
        if(gameui)gameui.score.text="得分："+score;
    } }
=======
    public int Score { get; set; }
>>>>>>> origin/main

    //血条
    public float Health { get; set; }
    

    private void Update()
    {
<<<<<<< HEAD
// <<<<<<< HEAD
        if (health <= 0) return;
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (UIManager.Instance.GetPanel<StopPanel>()!=null)
            {
                UIManager.Instance.HidePanel<StopPanel>();
            }
            else
            {
                UIManager.Instance.ShowPanel<StopPanel>();
            }
        }
// =======
        GameTime += Time.deltaTime;
// >>>>>>> a20a4a87b50a6de842eaeae7dcf4be6c8266fcf0
=======
        GameTime += Time.deltaTime;
>>>>>>> origin/main
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
                    DishInHand.DeSpawn();
                    DishInHand = null;
                }
                else
                {
                    //送餐失败
                }
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
// <<<<<<< HEAD
    private void GameOver()
    {
        UIManager.Instance.ShowPanel<OverPanel>();
    }
}

// =======
// }
// >>>>>>> a20a4a87b50a6de842eaeae7dcf4be6c8266fcf0
=======
}
>>>>>>> origin/main
