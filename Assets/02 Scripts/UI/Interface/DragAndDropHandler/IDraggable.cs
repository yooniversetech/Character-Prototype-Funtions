using UnityEngine;

public interface IDraggable
{
    ItemData Data { get; }
    ICell CurrentCell { get; set; }
    
    int CurrentStack {  get; set; }
    int ItemID {  get; }
    int MaxStack { get; }
    bool IsFull { get; }

    void SetCell(ICell newCell);
    void AddStack(int amount);
    void RemoveStack(int amount);
}
