using UnityEngine;

public interface IDraggable
{
    ItemData Data { get; }
    ICell CurrentCell { get; set; }
    int CurrentStack {  get; set; }
    int ItemID {  get; }
    int MaxStack { get; }
    bool isFull { get; }

    void Initialize(ItemData info, int stack);
}
