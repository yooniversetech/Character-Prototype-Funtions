using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class DropProcessManager : MonoBehaviour
{
    public List<IDropRule> _dropRules = new List<IDropRule>();
    public static DropProcessManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        _dropRules.Add(new AddRule());

    }

    public void ProcessDrop(ItemCell targetCell, InventoryItemDraggable draggedItem)
    {
        foreach (var rule in _dropRules)
        {
            if (rule.IsMatch(targetCell, draggedItem))
            {
                rule.Execute(targetCell, draggedItem);
                return;
            }
        }
    }
}
