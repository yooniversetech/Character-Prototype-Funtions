using UnityEngine;

public class InventoryItem : IDraggable
{
    public ItemData ItemData { get; private set; }

    public int CurrentStack {  get; private set; }
    public ICell CurrentCell { get; private set; }



    ICell IDraggable.CurrentCell { get => CurrentCell; set => CurrentCell = value; }
    int IDraggable.CurrentStack { get => CurrentStack; set => CurrentStack = value; }

    public ItemData Data => throw new System.NotImplementedException();

    public InventoryItem(ItemData itemData, int amount = 1)
    {
        this.ItemData = itemData;
        this.CurrentStack = amount;
    }

    public void SetCell(ICell newCell)
    {
        CurrentCell = newCell;
    }

    public void AddStack(int amount) => CurrentStack += amount;
    public void RemoveStack(int amount) => CurrentStack -= amount;
}
