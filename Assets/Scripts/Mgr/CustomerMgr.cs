using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMgr : SingletonMono<CustomerMgr>
{
    private Queue<Customer>[] _queues = new Queue<Customer>[Const.W1];

    public bool Send(int y)
    {
        if (y < 0 || y > Const.H - 1)
        {
            Debug.LogError("y不能超过地图高度");
            return false;
        }

        return true;
    }
}