using System;
using UnityEngine;

public class ComboMgr : SingletonMono<ComboMgr>
{
    [SerializeField] private Sprite[] nums;
    [SerializeField] private GameObject comboNumsPrefabs;

    public void UpdateCombo()
    {
        string combo = Player.Instance.Combo.ToString();
        int size = combo.Length;
        for (int i = 0; i < size; i++)
        {
            GameObject combobj = Instantiate(comboNumsPrefabs, transform, false);
            combobj.transform.localPosition = new Vector3(-2.3f - (size - 1 - i), 0);
        }
    }
}