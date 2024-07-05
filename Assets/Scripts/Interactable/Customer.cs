using UnityEngine;


public class Customer : MonoBehaviour
{
    public DishType WantType { get; private set; }

    public void Init()
    {
        WantType = Utils.GetRandomEnumValue<DishType>();
    }
}