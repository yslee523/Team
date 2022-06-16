using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public ItemBuffer itemBuffer;
    public Transform slotRoot;
    private List<Slot> slots;
    public System.Action<ItemProperty> onSlotClick; // 여기다가 외부 함수 연결
    void Start()
    {
        slots = new List<Slot>();
        int slotCnt = slotRoot.childCount;

        for (int i = 0; i < slotCnt; i++)
        {
            var slot = slotRoot.GetChild(i).GetComponent<Slot>();

            if(i < itemBuffer.items.Count)
            {
                slot.SetItem(itemBuffer.items[i]);
            }
            else
            {
                // 아이템 없는공간 버튼 안눌리게??@.@;;
                slot.GetComponent<UnityEngine.UI.Button>().interactable = false;
            }

            slots.Add(slot);
        }
    }

    void Update()
    {
    }

    public void OnClickSlot(Slot slot)
    {
        if(onSlotClick != null)
        {
            onSlotClick(slot.item);
        }
    }
}
