using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class CustomerMgr : SingletonMono<CustomerMgr>
{
    private Queue<Customer>[] _queues = new Queue<Customer>[Const.H];

    [SerializeField] private GameObject customerPrefab;

    public int Count =>
        _queues.Sum(variable => variable.Count);


    //刷新顾客间隔
    public float SpawnDuration
    {
        get
        {
            if (Player.Instance.GameTime < 180)
            {
                return 1.5f - (1.3f * (Player.Instance.GameTime / 180));
            }

            return 0.2f;
        }
    }

    //计时器
    private float _timer;


    private void Start()
    {
        for (int i = 0; i < Const.W1; i++)
        {
            _queues[i] ??= new Queue<Customer>(Const.W1);
        }
    }

    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            SpawnCustomer(MyRandom.Instance.NextInt(Const.H));
            _timer = SpawnDuration;
        }
    }


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
        //送完餐之后销毁
        customer.DeSpawn();
        UpdateCustoms(queue);
        return true;
    }

    private void UpdateCustoms(IEnumerable<Customer> queue)
    {
        foreach (var customer in queue)
        {
            Transform customerTransform = customer.transform;
            customerTransform.DOKill(true);
            customerTransform.DOLocalMove(customerTransform.position + new Vector3(-1, 0, 0), 0.5f);
        }
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