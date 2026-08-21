using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
    public IReadOnlyList<ItemSlot> Slots => slots;
    private List<ItemSlot> slots;

    public event Action OnInventoryChanged;

    public Inventory(int slotCount)
    {
        slots = new List<ItemSlot>(slotCount);
        for (int i = 0; i < slotCount; i++)
        {
            var slot = new ItemSlot();
            slot.OnSlotChanged += () => OnInventoryChanged?.Invoke();  // 슬롯 변경 시 인벤토리도 함께 변경 이벤트
            slots.Add(slot); // 슬롯을 리스트에 추가
        }
    }

    /// <summary>
    /// 아이템 추가 시도. 다 못 넣으면 남은 개수 반환 (0이면 전부 성공)
    /// </summary>
    /// <param name="data"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public int AddItem(ItemData data, int amount)
    {
        int remaining = amount;
        remaining = FillExistingStacks(data, remaining);
        remaining = FillEmptySlots(data, remaining);
        return remaining;
    }

    /// <summary>
    /// 이미 같은 아이템이 있는 슬롯부터 채우기 (스택 합치기)
    /// </summary>
    /// <param name="data"></param>
    /// <param name="remaining"></param>
    /// <returns></returns>
    private int FillExistingStacks(ItemData data, int remaining)
    {
        foreach (var slot in slots)
        {
            if (remaining <= 0) break;
            if (!slot.IsEmpty && slot.ItemData == data)
                remaining = slot.AddItem(data, remaining);
        }

        return remaining;
    }

    /// <summary>
    /// 채우고 남았으면 빈 슬롯에 채우기
    /// </summary>
    /// <param name="data"></param>
    /// <param name="remaining"></param>
    /// <returns></returns>
    private int FillEmptySlots(ItemData data, int remaining)
    {
        foreach (var slot in slots)
        {
            if (remaining <= 0) break;
            if (slot.IsEmpty)
                remaining = slot.AddItem(data, remaining);
        }

        return remaining;
    }

    public bool RemoveItem(ItemData data, int amount)
    {
        int remaining = amount;

        foreach (var slot in slots)
        {
            if (remaining <= 0) break;
            if (!slot.IsEmpty && slot.ItemData == data)
                remaining -= slot.RemoveItem(remaining);
        }

        return remaining <= 0;
    }

    public void SwapSlots(int indexA, int indexB)
    {
        if (indexA < 0 || indexA >= slots.Count) return;
        if (indexB < 0 || indexB >= slots.Count) return;
        
        slots[indexA].SwapWith(slots[indexB]);
    }



    //인벤토리 테스트용 메서드 (인게임의 아이템 오브젝트를 생성해서 사용할 때 삭제예정)
    public void DebugSetSlot(int index, ItemData data, int amount)
    {
        if (index < 0 || index >= slots.Count) return;
        slots[index].AddItem(data, amount);
    }
}
