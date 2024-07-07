using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : SingletonMono<Player>
{
    public float GameTime { get; private set; }

    //累死我了 懒得做动画机了
    [SerializeField] private Sprite[] sprites;

    //手上的菜品
    public Dish DishInHand { get; private set; } = null;

    //玩家位置当前处在的Y轴值
    private int Y => Mathf.FloorToInt(transform.position.y);
    [SerializeField] private SpriteRenderer sr;

    //玩家目前得到的积分

    private int _score;

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            GamePanel.Instance.score.text = "得分：" + _score;
        }
    }


    [SerializeField] private float health = 100;

    public float Health
    {
        get => health;
        set
        {
            health = value;
            GamePanel.Instance.health.value = health / 100;
            if (health <= 0)
            {
                GameOver();
            }
        }
    }

    private void Update()
    {
        if (health <= 0) return;
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (UIManager.Instance.GetPanel<StopPanel>())
            {
                UIManager.Instance.HidePanel<StopPanel>();
            }
            else
            {
                UIManager.Instance.ShowPanel<StopPanel>();
            }
        }

        GameTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (Y >= Const.H - 1)
            {
                //卡住的逻辑  可以用个动画或音效来表现？
                return;
            }

            transform.DOKill(true);
            transform.DOLocalMove(transform.position + new Vector3(0, 1, 0), 0.05f).SetEase(Ease.Linear);
            if (!DishInHand)
            {
                sr.sprite = sprites[0];
            }

            if (DishInHand)
            {
                sr.sprite = sprites[2];
                DishInHand.transform.localPosition = new Vector3(0.0f, -0.23f, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (Y <= 0)
            {
                //卡住的逻辑
                return;
            }

            transform.DOKill(true);
            transform.DOLocalMove(transform.position + new Vector3(0, -1, 0), 0.05f).SetEase(Ease.Linear);
            sr.sprite = sprites[2];
            if (DishInHand)
            {
                DishInHand.transform.localPosition = new Vector3(0.0f, -0.23f, 0);
            }
        }

        //取餐
        if (Input.GetKeyDown(KeyCode.J))
        {
            sr.sprite = sprites[3];
            if (DishMgr.Instance.GetDish(out Dish dish, Y))
            {
                dish.transform.DOKill(true);
                DishInHand = dish;
                dish.transform.SetParent(transform);
                dish.transform.localPosition = new Vector3(0, -0.23f, 0);
                dish.transform.localScale = new Vector3(1, 0.42f, 1);
                MusicMgr.Instance.PlaySound("get");
            }

            if (DishInHand)
            {
                DishInHand.transform.localPosition = new Vector3(-0.3f, -0.23f, 0);
            }
        }

        //送餐
        if (Input.GetKeyDown(KeyCode.K))
        {
            sr.sprite = sprites[1];
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

            if (DishInHand)
            {
                DishInHand.transform.localPosition = new Vector3(0.3f, -0.23f, 0);
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
            MusicMgr.Instance.PlaySound("destroy");
            DishInHand.DeSpawn();
            DishInHand = null;
        }
    }


    private void GameOver()
    {
        UIManager.Instance.ShowPanel<OverPanel>();
    }
}