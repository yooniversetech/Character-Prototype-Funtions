using UnityEngine;

public interface IDraggable
{
    ItemData Info { get; }
    ICell CurrentCell { get; set; }
    int CurrentStack {  get; set; }

    void Initialize(ItemData info, int stack);
}
