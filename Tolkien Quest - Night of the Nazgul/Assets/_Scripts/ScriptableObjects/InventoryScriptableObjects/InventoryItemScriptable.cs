using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemScriptable", menuName = "ScriptableObjects/InventoryScriptables/InventoryItemScriptable", order = 0)]
public class InventoryItemScriptable : ScriptableObject {
    
	public string itemName;

	public int itemIndex;

	public int itemQuantity;
}
