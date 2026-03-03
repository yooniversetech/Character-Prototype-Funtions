using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class DropProcessManager : MonoBehaviour
{
    public List<IDropRule> rules = new List<IDropRule>();

    public void ProcessDrop(ItemCell targetCell, InventoryItemDraggable draggedItem)
    {
        foreach (var rule in rules)
        {
            if (rule.IsMatch(targetCell, draggedItem))
            {
                rule.Execute(targetCell, draggedItem);
                return;
            }
        }
    }

    public void ProcessAdd()
    {

    }
    public void ProcessSetDown()
    {

    }
}
