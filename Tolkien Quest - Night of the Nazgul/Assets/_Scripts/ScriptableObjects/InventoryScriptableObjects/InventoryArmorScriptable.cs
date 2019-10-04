using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewArmorScriptable", menuName = "ScriptableObjects/InventoryScriptables/InventoryArmorScriptable", order = 0)]
public class InventoryArmorScriptable : ScriptableObject {

	public string itemName;

	public int itemIndex;
}
