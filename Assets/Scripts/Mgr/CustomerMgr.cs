using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMgr : SingletonMono<CustomerMgr>
{
    private Queue<Customer>[] _queues = new Queue<Customer>[5];
}