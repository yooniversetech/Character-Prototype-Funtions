using UnityEngine;

public static class ItemActionProcessor
{
    //public static void Swap(ICell source, ICell target)
    //{
    //    var tempItem = source.ContainedItem;
    //    source.SetItem(target.ContainedItem);
    //    target.SetItem(tempItem);
    //}

    public static bool TryMerge(ICell source, ICell target)
    {
        IDraggable sourceItem = source.ContainedItem;
        IDraggable targetItem = target.ContainedItem;

        //if (sourceItem.ID == targetItem.ID)

        return true;
    }
}
