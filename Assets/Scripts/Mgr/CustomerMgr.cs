using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMgr : SingletonMono<CustomerMgr>
{
    private Queue<Customer>[] _queues = new Queue<Customer>[Const.W1];
    [SerializeField] private GameObject customerPrefab;

    public bool SendDishToCustomer(Dish dish, int y)
    {
        if (y < 0 || y > Const.H - 1 || !dish)
        {
            Debug.LogError("错误");
            return false;
        }

        var queue = _queues[y];
        if (queue.Count <= 0) return false;

        var customer = queue.Dequeue();
        customer.SendCallBack(dish.Type == customer.WantType);
        return true;
    }

    public Customer SpawnCustomer(int y)
    {
        if (y < 0 || y > Const.H - 1)
        {
            Debug.LogError("y不能超过地图范围");
            return null;
        }

        var queue = _queues[y];
        GameObject gobj = Instantiate(customerPrefab, transform, false);
        Customer customer = gobj.GetComponent<Customer>();
        customer.InitForRandom();
        queue.Enqueue(customer);
        customer.transform.localPosition = new Vector2(queue.Count, y);
        return customer;
    }
}