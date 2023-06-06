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

        slot.AddItem(new Item((int)CrisPyItemId.WeaponGun, "��", "�ΰ��� ���� ���� ������ ������"));
        slot.AddItem(new Item((int)CrisPyItemId.Weapon, "��Į", "�̰� �Ļ��� �ڸ���� ����ǵ�..."));
        slot.AddItem(new Item((int)CrisPyItemId.Food, "�Ļ�", "���� �԰� ��ƾ���?"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad7) == true)
        {
            Item item = slot.SelectAt(0);
            if (item == null)
            {
                Debug.Log("�������� ����ִ�.");
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
                Debug.Log("�������� ����ִ�.");
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
                Debug.Log("�������� ����ִ�.");
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
            // slot.AddItem(new Item(0, "���� ������ ��", "�ſ� �����ϴ�"));
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2) == true)
        {
            // slot.AddItem(new Item(1, "���� �׷� ��Į", "���� ������"));
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3) == true)
        {
            // slot.AddItem(new Item(2, "������ �Ļ�", "��ó��� �����..."));
        }
    }
}
