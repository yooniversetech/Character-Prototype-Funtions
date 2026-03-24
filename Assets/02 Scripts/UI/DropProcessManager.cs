using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class DropProcessManager : MonoBehaviour
{
    public List<IDropRule> _dropRules = new List<IDropRule>();
    [SerializeField] private Transform mainCanvasTransform;

    public static DropProcessManager Instance { get; private set; }
    public InventoryItemDraggable DraggableItem {  get; private set; }
    public Transform MainCanvasTransform => mainCanvasTransform;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        //else Destroy(gameObject);

        _dropRules.Add(new MergeStrategy());
        _dropRules.Add(new GetDownStrategy());
    }

    public void ProcessDrop(ItemSlot targetCell, InventoryItemDraggable draggedItem)
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
