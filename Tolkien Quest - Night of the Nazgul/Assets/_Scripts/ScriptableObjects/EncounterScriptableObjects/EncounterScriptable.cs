using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEncounterScriptable", menuName = "ScriptableObjects/EncounterScriptable", order = 0)]
public class EncounterScriptable : ScriptableObject {

	public int encounterIndex;

	[TextArea(4, 10, order = 0)] public string[] encounterText;

	public bool isCombat;
	public bool isMerchant;
	public bool canMoveOn;
	public bool moveOnRandomly;
	public int rollHowToMoveOnMax;

	public string moveToSpecificTile;

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

	public bool rangeFailMeansGameOver;

	[Header("Inventory Adjustment Variables", order = 1)]
	public bool obtainsItems;

	[Header("Misc. Variables", order = 1)]
	public int timeTaken;
	public bool canRestAtNight;

	[Header("Experience Point Variables")]
	public int XPGained;

	[Header("Misc. Variables")]
	public bool autoGameOver;
	public bool autoWin;
	
}
