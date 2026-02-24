using UnityEngine;

public interface IItemData
{
    int ItemID { get; }

    string ItemName { get; }
    
    Sprite ItemIcon { get; }

    string Description { get; }
}
