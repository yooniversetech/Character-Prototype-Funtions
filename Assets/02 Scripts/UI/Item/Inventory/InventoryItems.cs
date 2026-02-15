using UnityEngine;

public class InventoryItem : IDraggable
{
    private ItemData data;

    public int ItemID => data.ID; 

    public int MaxStack => data.MaxStack;

    public bool IsFull => CurrentStack >= MaxStack;

    public int CurrentStack {  get; private set; }
    public ICell CurrentCell { get; private set; }

    ICell IDraggable.CurrentCell { get => CurrentCell; set => CurrentCell = value; }
    int IDraggable.CurrentStack { get => CurrentStack; set => CurrentStack = value; }


    public InventoryItem(ItemData itemData, int amount = 1)
    {
        this.data = itemData;
        this.CurrentStack = amount;
    }

    public void SetCell(ICell newCell)
    {
        CurrentCell = newCell;
    }

    public void AddStack(int amount) => CurrentStack += amount;
    public void RemoveStack(int amount) => CurrentStack -= amount;
}