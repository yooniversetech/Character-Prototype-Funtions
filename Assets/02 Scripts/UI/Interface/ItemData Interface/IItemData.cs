using UnityEngine;

public interface IItemData
{
    Sprite ItemIcon { get; }

    int ItemID { get; }
    string ItemName { get; }
    string Description { get; }
    int MaxStack { get; }
}
