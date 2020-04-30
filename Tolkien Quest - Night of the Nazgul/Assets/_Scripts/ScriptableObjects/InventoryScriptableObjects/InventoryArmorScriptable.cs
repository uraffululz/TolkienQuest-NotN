using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewArmorScriptable", menuName = "ScriptableObjects/InventoryScriptables/InventoryArmorScriptable", order = 0)]
public class InventoryArmorScriptable : ScriptableObject {

	[TextArea(2, 5, order = 0)]
	public string[] notes;

	public string itemName;

	public int itemIndex;

	[Space]

	public int armorQuantity;

	[Space]

	public bool noTrickeryPenalty;
	public enum skillIncrease {None, General, Trickery };
	public skillIncrease mySkillIncrease;
}
