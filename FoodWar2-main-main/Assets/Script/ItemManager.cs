using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
    public static ItemManager instance
    {
        get
        {
            if (_instance == null)
            {
                // ��l�ƪ��ɭԦs�Ҧ����D��ƥ�
                _instance = new ItemManager();
                _instance.items = new List<Item>(Resources.LoadAll<Item>("Data"));
                //foreach (Item i in _instance.items)
                   // Debug.LogError(i.name);
            }
            return _instance;
        }
    }
    static ItemManager _instance = null;

    List<Item> items = new List<Item>();


    /// <summary>�ΦW�٨��o�۹������D��</summary>
    /// <param name="itemName"></param>
    /// <returns></returns>
    public Item GetItmeByName(string itemName)
    {
        for(int i = 0; i < items.Count; i++)
        {
            if (items[i].name == itemName)
                return items[i];
        }
        return null;
    }
}
