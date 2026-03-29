using UnityEngine;

public interface IItemData
{
    ItemData Data { get; }

    Sprite ItemIcon { get; }
    int ItemID { get; }
    string ItemName { get; }
    string Description { get; }
    int MaxStack { get; }
}
