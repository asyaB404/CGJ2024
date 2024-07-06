using System;
using System.Collections.Generic;
using UnityEngine;

public class ComboMgr : SingletonMono<ComboMgr>
{
    [SerializeField] private Sprite[] nums;
    [SerializeField] private GameObject comboNumsPrefabs;
    private Stack<GameObject> _comboStack;

    [ContextMenuItem("UpdateCombo","UpdateCombo")][SerializeField] private string combo;
    public void UpdateCombo()
    {
        _comboStack.Clear();
        foreach (var item in _comboStack)
        {
            Destroy(item);
        }
        string combostring = Player.Instance.Combo.ToString();
        combostring = combo;
        int size = combo.Length;
        for (int i = 0; i < size; i++)
        {
            GameObject combobj = Instantiate(comboNumsPrefabs, transform, false);
            combobj.transform.localPosition = new Vector3(-2.3f - (size - 1 - i), 0);
        }
    }
}