using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemScriptable", menuName = "ScriptableObjects/InventoryScriptables/InventoryItemScriptable", order = 0)]
public class InventoryItemScriptable : ScriptableObject {
    
	[TextArea (2, 5, order = 0)]
	public string[] notes;

	public string itemName;

	public int itemIndex;

	[Space]

	public int itemQuantity;
	public bool isInfinite;
	public bool isStackable;
	public bool usableOncePerDay;
	public bool usableTwicePerDay;

	[Space]

	public bool isMoney;

	[Space]

	public bool isAMeal;
	public bool isACure;

	[Space]

	public bool autoDefeatsWights;

	public enum skillIncrease {None, General, Trickery};
	public skillIncrease mySkillIncrease;
}
