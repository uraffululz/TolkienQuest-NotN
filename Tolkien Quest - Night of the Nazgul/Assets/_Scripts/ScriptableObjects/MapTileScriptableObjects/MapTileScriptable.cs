using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMapTileScriptable", menuName = "ScriptableObjects/MapTileScriptable", order = 0)]
public class MapTileScriptable : ScriptableObject {

	public string locationID;

	[TextArea(3, 10, order = 0)] public string[] locationText;

	[Header("Encounter Text Variables")]
	public string encounterText1;
	public int encounterIndex1;
	public string encounterText2;
	public int encounterIndex2;
	public string encounterText3;
	public int encounterIndex3;
	public string encounterText4;
	public int encounterIndex4;

	//[Header("Exploration Text Variables")]
	//public bool canExploreHere;
	//public bool actionRequiresRoll;

	public enum actionSkill {No, General, Trickery, Perception};

	[Header("Exploration Text Variables")]

	public actionSkill actionAddsSkill;
	//public int actionMinLimit;
	//public int actionSecondaryIndex;

	public bool exploreHasMultipleRanges;
	public int howManyRanges;
	public int range1Min;
	public int range1Length;
	public int range1FurtherIndex;
	public int range2Min;
	public int range2Length;
	public int range2FurtherIndex;
	public int range3Min;
	public int range3Length;
	public int range3FurtherIndex;
	public int range4Min;
	public int range4Length;
	public int range4FurtherIndex;
	public bool rangeFailMeansMoveOn;
	public bool canGetLost;
	//public bool rangeFailMeansGameOver;
	//public int randomDirectionRangeMin;
	public int randomDirectionRangeMax;

	public bool combatHere;
	public bool merchantHere;
	public EncounterMerchantScriptable myMerchantScriptable;

	public bool canMoveOn;

	[Header("Time Variables")]
	public int timeTaken;
	public int timeTakenSecondary;
	public bool canRestAtNight;
	public bool timeLocked;
	public int daysLimit1;
	public int timeLimit1;
	public int limit1Index;
	public int daysLimit2;
	public int timeLimit2;
	public int limit2Index;
	public int overTimeIndex;

	[Header("Experience Point Variables")]
	public int experience;

	[Header("Unique Tile Rule Variables")]
	public bool changesPlayerLocation;
	public string changedLocationID;

	public bool previousLocationsConsidered;
	public string[] locationsConsidered;
	public int locationBasedEncounter;
	public int alternateLocationEncounter;

	public string riverSameSideTile1ID;
	public string riverSameSideTile2ID;
	public string riverOtherSideTile1ID;
	public string riverOtherSideTile2ID;
}
