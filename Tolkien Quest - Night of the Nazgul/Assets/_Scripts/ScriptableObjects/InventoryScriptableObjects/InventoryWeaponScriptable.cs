using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponScriptable", menuName = "ScriptableObjects/InventoryScriptables/InventoryWeaponScriptable", order = 0)]
public class InventoryWeaponScriptable : ScriptableObject {
  
	public string itemName;

	public int itemIndex;
}
