using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    [SerializeField] private List<ItemScript> Items = new List<ItemScript>();

    private PlayerController Controller;
    
    private void Awake()
    {
        Controller = GetComponent<PlayerController>();
    }

    public List<ItemScript> GetItemList() => Items;

    public int GetItemCount() => Items.Count;

    public ItemScript FindItem(string itemName)
    {
        return Items.Find((invItem) => invItem.name == itemName);
    }

    public void AddItem(ItemScript item, int amount = 0)
    {
        int itemIndex = Items.FindIndex(listItem => listItem.name == item.name);
        if (itemIndex != -1)
        {
            ItemScript listItem = Items[itemIndex];
            if (listItem.stackable && listItem.amountValue < listItem.maxSize)
            {
                listItem.ChangeAmount(item.amountValue);
            }
        }
        else
        {
            if (item == null) return;

            ItemScript itemClone = Instantiate(item);
            itemClone.Initialize(Controller);
            itemClone.SetAmount(amount <= 1 ? item.amountValue : amount);
            Items.Add(itemClone);
        }
    }

    public void DeleteItem(ItemScript item)
    {
        Debug.Log($"{item.name} deleted!");
        int itemIndex = Items.FindIndex(listItem => listItem.name == item.name);
        if (itemIndex == -1) return;

        Items.Remove(item);
    }

    public List<ItemScript> GetItemsOfCategory(ItemCategory itemCategory)
    {
        if (Items == null || Items.Count <= 0) return null;

        return itemCategory == ItemCategory.None ? Items : 
            Items.FindAll(item => item.itemCategory == itemCategory);
    }
}
