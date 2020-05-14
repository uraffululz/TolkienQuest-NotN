using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponScriptable", menuName = "ScriptableObjects/InventoryScriptables/InventoryWeaponScriptable", order = 0)]
public class InventoryWeaponScriptable : ScriptableObject {

	[TextArea(2, 5, order = 0)]
	public string[] notes;

	public string itemName;

	public int itemIndex;

//MAYBE Add each individual weapon to this list, which may make determining the player's combat attacks/choices easier in the future
	public enum weaponTypes {Default, BareHanded, BattleAxe, Bow, Club, Dagger, Mace, Spear, Staff, Sword, TwoHandedSword, WarHammer, MagicSword, MagicBow, OrcSlayingSword};
	public weaponTypes MyWeaponType;

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

	public int obtainsArrows;
	
}
