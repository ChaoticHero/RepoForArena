using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1
public class InventoryList<T> where T : class
{
    private T _item;
    public T item
    {
        get { return _item; }
    }
    // 2
    public InventoryList()
    {
        Debug.Log("Generic list initalized...");
    }
    public void SetItem(T newItem)
    {
        // 3
        _item = newItem;
        Debug.Log("New item added...");
    }
}
