using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private const int slotsNum = 3;

    private Slot slot;

    // Start is called before the first frame update
    void Start()
    {
        slot = new Slot(slotsNum);

        slot.AddItem(new Item((int)CrisPyItemId.WeaponGun, "총", "인간이 만든 아주 무서운 무기지"));
        slot.AddItem(new Item((int)CrisPyItemId.Weapon, "식칼", "이건 식빵을 자르라고 만든건데..."));
        slot.AddItem(new Item((int)CrisPyItemId.Food, "식빵", "밥은 먹고 살아야지?"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad7) == true)
        {
            Item item = slot.SelectAt(0);
            if (item == null)
            {
                Debug.Log("아이템이 비어있다.");
            }
            else
            {
                Debug.Log(item.ToString());
            }
        }
        else if (Input.GetKeyDown(KeyCode.Keypad8) == true)
        {
            Item item = slot.SelectAt(1);
            if (item == null)
            {
                Debug.Log("아이템이 비어있다.");
            }
            else
            {
                Debug.Log(item.ToString());
            }
        }
        else if (Input.GetKeyDown(KeyCode.Keypad9) == true)
        {
            Item item = slot.SelectAt(2);
            if (item == null)
            {
                Debug.Log("아이템이 비어있다.");
            }
            else
            {
                Debug.Log(item.ToString());
            }
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1) == true)
        {
            // slot.AddItem(new Item(0, "아주 강력한 총", "매우 강력하다"));
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2) == true)
        {
            // slot.AddItem(new Item(1, "그저 그런 식칼", "둔한 나이프"));
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3) == true)
        {
            // slot.AddItem(new Item(2, "옥수수 식빵", "잠시나마 평온을..."));
        }
    }
}
