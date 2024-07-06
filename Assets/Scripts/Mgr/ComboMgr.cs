using System;
using System.Collections.Generic;
using UnityEngine;

public class ComboMgr : SingletonMono<ComboMgr>
{
    [SerializeField] private Sprite[] nums;
    [SerializeField] private GameObject comboNumsPrefabs;
    [SerializeField] private Transform comboParent;

    [ContextMenuItem("UpdateCombo", "UpdateCombo")] [SerializeField]
    private string combo;

    public void UpdateCombo()
    {
        comboParent.DestroyAllChildren();
        // string combostring = Player.Instance.Combo.ToString();
        string combostring = combo;
        int size = combo.Length;
        for (int i = 0; i < size; i++)
        {
            GameObject combobj = Instantiate(comboNumsPrefabs, comboParent, false);
            combobj.transform.localPosition = new Vector3(-2.3f - (size - 1 - i), 0);
        }
    }
}