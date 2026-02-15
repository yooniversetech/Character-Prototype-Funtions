using UnityEngine;

public interface IDraggable
{
    int CurrentStack {  get; set; }
    int ItemID {  get; }
    int MaxStack { get; }
    bool IsFull { get; }
    ICell CurrentCell { get; set; }

    void SetCell(ICell newCell);
    void AddStack(int amount);
    void RemoveStack(int amount);
}
