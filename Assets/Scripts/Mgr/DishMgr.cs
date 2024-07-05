using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishMgr : SingletonMono<DishMgr>
{
    private Queue<Dish>[] _queues = new Queue<Dish>[5];
}