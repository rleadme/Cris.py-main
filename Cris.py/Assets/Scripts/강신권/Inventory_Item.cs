using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CrisPyItemId
{
    WeaponGun = 0,
    Weapon = 1,
    DoorKey = 2,
    Food = 3,
}

public class Item
{
    private int id;
    private int count;
    private string name;
    private string description;


    public Item(int itemId)
        : this(itemId, string.Empty, string.Empty)
    {

    }

    public Item(int itemId, string name, string description)
        : this(itemId, 1, name, description)
    {
    }

    public Item(int itemId, int itemCount, string name, string description)
    {
        Id = itemId;
        Count = itemCount;
        Name = name;
        Description = description;
    }

    public int Id
    {
        get
        {
            return id;
        }
        private set
        {
            id = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }
        private set
        {
            name = value;
        }
    }

    public string Description
    {
        get
        {
            return description;
        }
        private set
        {
            description = value;
        }
    }

    public int Count
    {
        get
        {
            return count;
        }
        private set
        {
            count = value;
        }
    }

    public void AddCount(int amount)
    {
        count += amount;
    }

    public void SubCount(int amount)
    {
        count -= amount;
    }

    // Compare by id and count
    public override bool Equals(object? obj)
    {
        Item itemObj = obj as Item;

        if (itemObj == null)
        {
            return false;
        }
        else
        {
            return id == itemObj.Id && count == itemObj.count;
        }
    }

    public override string ToString()
    {
        return "Item ID : " + Id + " ITEM NAME : " + Name + " ITEM DESCRIPTION : " + Description;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}



// Item에 종속적인 Slot 객체
public class Slot
{
    private List<Item> itemList;
    public Slot(int slotsNum)
    {
        itemList = new List<Item>(slotsNum);
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }

    public void RemoveAt(Item item, int itemRemoveAmount)
    {
        if (item == null)
        {
            return;
        }

        // indexOf 내부적으로 Object Equals 메서드 호출.
        int index = itemList.IndexOf(item);

        if (index == -1)
        {
            return;
        }
        else
        {
            if (itemList[index].Count >= itemRemoveAmount)
            {
                itemList.RemoveAt(index);
            }
            else
            {
                itemList[index].SubCount(itemRemoveAmount);
            }
        }
    }

    public void RemoveAt(int index, int itemRemoveAmount)
    {
        if (itemList[index].Count >= itemRemoveAmount)
        {
            itemList.RemoveAt(index);
        }
        else
        {
            itemList[index].SubCount(itemRemoveAmount);
        }
    }
    public Item SelectAt(int index)
    {
        if (index >= itemList.Count || index < 0)
            return null;
        else
            return itemList[index];
    }

    public Item this[int index]
    {
        get
        {
            return SelectAt(index);
        }
    }

    // Slot's Current Item Count
    public int Count
    {
        get
        {
            return itemList.Count;
        }
    }

    // Slot's Max Size, Capacity
    public int Size
    {
        get
        {
            return itemList.Capacity;
        }
    }
}
