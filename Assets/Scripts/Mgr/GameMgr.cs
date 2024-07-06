using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameMgr : SingletonMono<GameMgr>
{
    /// <summary>
    /// ´ýÈ¡²ÍÊýÁ¿
    /// </summary>
    public int toDoNum;

    public int money = 100;

    private int overNum;
    private float curTime;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
       
    }


    public void UpateMoney(int money,bool add)
    {
        if(add)
        {

        }
        else
        {
            money -= 10;
        }
    }

    public void UpdateNum(int toDoNum,int overNum)
    {

    }
}
