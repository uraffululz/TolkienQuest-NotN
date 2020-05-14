using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEncounterScriptable", menuName = "ScriptableObjects/EncounterScriptable", order = 0)]
public class EncounterScriptable : ScriptableObject {

	public int encounterIndex;

	[TextArea(4, 10, order = 0)] public string[] miscNotes;

	[TextArea(4, 10, order = 0)] public string[] encounterText;

	//public bool isCombat;
	public bool isMerchant;
	public bool canMoveOn;
	public bool moveOnRandomly;
//TODO Probably get rid of the rollHowToMoveOnMax. I can't see where it's actually used, as none of the EncounterScriptables seem to have it assigned
	public int rollHowToMoveOnMax;
	public bool movesOnTwoSpaces;

	public string moveToSpecificTile;

	//public bool canRunAway;

	[Header("Further Encounter Options", order = 0)]
	public string furtherEncounter1Text;
	public int furtherEncounter1Index;

	public string furtherEncounter2Text;
	public int furtherEncounter2Index;

	public string furtherEncounter3Text;
	public int furtherEncounter3Index;

	public string furtherEncounter4Text;
	public int furtherEncounter4Index;

	public enum actionSkill { No, General, Trickery, Perception, Running, Magical};

	[Header("Exploration Text Variables")]
	public bool exploreHasMultipleRanges;
	public actionSkill actionAddsSkill;
	//public int actionMinLimit;
	//public int actionSecondaryIndex;
	public int howManyRanges;
	public int range1Min;
	public int range1Length;
	public RangeInt exploreRange1;
	public int range1FurtherIndex;
	public int range2Min;
	public int range2Length;
	public RangeInt exploreRange2;
	public int range2FurtherIndex;
	public int range3Min;
	public int range3Length;
	public RangeInt exploreRange3;
	public int range3FurtherIndex;
	public int range4Min;
	public int range4Length;
	public RangeInt exploreRange4;
	public int range4FurtherIndex;
	public bool rangeFailMeansMoveOn;
	public bool canGetLost;
	public int randomDirectionChoiceMax;
	public bool hasRunAwayRange;
	public bool hasSneakAttackRange;
	public bool rangeFailMeansGameOver;

	[Header("Inventory Adjustment Variables", order = 1)]
	public bool recoversFullInventory;
	public bool emptiesInventory;
	//public bool logsEmptiedInventory;
	public bool loseWeaponsAndArmor;
	public InventoryWeaponScriptable keepSilverDagger;
	//public bool loseAllMoney; This is already part of all the encounters that "Empty Inventory" anyway

	[Space]

	public bool obtainsItems;
	public EncounterObtainedItemList obtainedItemList;
	//public EncounterObtainedItemList alternateItemList;
	public bool exploreRangeDeterminesItems;

	//public ScriptableObject checksForSpecificItem;
	//public bool checksItemQuantity;
	public InventoryItemScriptable mealScript;
	public float loseMeals;
	public int costsCopper;
	//public InventoryItemScriptable useSpecificItem;

	[Space]
	[Header("Combat")]
	public CombatScriptable combatScript;


	[Header("Misc. Variables", order = 1)]
	public int timeTaken;
	public bool canRestAtNight;

	[Header("Experience Point Variables")]
	public int XPGained;

	[Header("Misc. Variables")]
	public int damageAlteredAmount;
	public bool dealsRandomDirectDamage;
	public bool hasDamageRanges;
	public bool deals50PercentDamage;
	public bool curesDisease;
	public bool hasFreeMeal;
	[Space]
	public bool autoGameOver;
	public bool autoWin;
	[Space]
	public bool meetTom;
	public bool checkIfMetTom;
	public bool meetGildor;
	public bool checkIfMetGildor;
	[Space]
	public bool canJumpInRiver;
	public bool jumpInRiverRequiresRoll;
	[Space]
	public bool floatsDownstream;
	public bool hasDownstreamRanges;
	public bool landsOnOtherRiverSide;
	public bool lowerValueFloats1Space;

	public bool isTimeLocked;
	public int dayLimit1;
	public int timeLimit1;
	public int limit1Index;
	public int dayLimit2;
	public int timeLimit2;
	public int limit2Index;
	public int overTimeIndex;

	public int addsBonusToNextExplore;

	[Space]
	public int warnedSettlement;
	//public bool notesTheTime;
}
