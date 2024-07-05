using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField]private CustomerInfo info;
    public void Init()
    {
        this.info = info;
    }
}