using UnityEngine;

public interface IItemSlot
{
    IItemData ItemData { get; }
    int CurrentStack { get; set; }
    bool IsEmpty { get; }
}
