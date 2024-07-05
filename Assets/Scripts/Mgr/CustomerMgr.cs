using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class CustomerMgr : SingletonMono<CustomerMgr>
{
    private int h = 4;
    private int w = 4;
    private Queue<Customer>[] _queues = new Queue<Customer>[h];
    [SerializeField] private GameObject customerPrefab;

    public Customer SpawnCustomer(Vector2 pos)
    {
        GameObject gobj = Instantiate(customerPrefab);
        gobj.transform.position = pos;
        Customer customer = gobj.GetComponent<Customer>();
        customer.Init();
        return customer;
    }

    private void Start()
    {
        for (int i = 0; i < h; i++)
        {
            _queues[i] = new Queue<Customer>();
            for (int j = 0; j < w; j++)
            {
                _queues[i].Enqueue(SpawnCustomer(new(w - i, j)));
            }
        }
    }
}