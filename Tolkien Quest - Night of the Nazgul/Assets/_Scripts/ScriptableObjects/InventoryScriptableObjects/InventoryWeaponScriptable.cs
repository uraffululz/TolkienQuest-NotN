using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponScriptable", menuName = "ScriptableObjects/InventoryScriptables/InventoryWeaponScriptable", order = 0)]
public class InventoryWeaponScriptable : ScriptableObject {
  
	public string itemName;

	public int itemIndex;

	[Space]

	public int weaponQuantity;

	[Space]

	public int additionalDamage;
	public bool requiresEnemyInChainOrPlateArmor;

	[Space]

	public int MeleeOBAdjustment;
	public bool canBeUsedAsMissileAttack;

	[Space]

	public int MissileOBAdjustment;	
	public bool onlyMissileAttacks;

	[Space]

	public bool autoKillsOrcs;
	
}
