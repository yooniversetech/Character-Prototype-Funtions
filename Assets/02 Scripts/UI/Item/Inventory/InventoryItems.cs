using UnityEngine;

public class InventoryItem : IDraggable
{
    // 아이템 데이터에서 사용될 필드
    public ItemData Data { get; private set; }
    public int ItemID => Data.ItemID; 
    public int MaxStack => Data.MaxStack;
    public bool IsFull => CurrentStack >= MaxStack;

    // 인벤토리 내부에서 사용될 실질적인 데이터
    public int CurrentStack {  get; private set; }
    public ICell CurrentCell { get; private set; }

    ICell IDraggable.CurrentCell { get => CurrentCell; set => CurrentCell = value; }
    int IDraggable.CurrentStack { get => CurrentStack; set => CurrentStack = value; }


    public InventoryItem(ItemData itemData, int amount = 1)
    {
        this.Data = itemData;
        this.CurrentStack = amount;
    }

    public void SetCell(ICell newCell)
    {
        CurrentCell = newCell;
    }

    public void AddStack(int amount) => CurrentStack += amount;
    public void RemoveStack(int amount) => CurrentStack -= amount;
}