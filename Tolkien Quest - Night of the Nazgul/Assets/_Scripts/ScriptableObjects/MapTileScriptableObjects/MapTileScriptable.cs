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

	[Header("Exploration Text Variables")]
	public bool canExploreHere;
	public bool exploreRequiresRoll;
	public int exploreMinLimit;
	public int exploreSecondaryIndex;

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
	public bool rangeFailMeansMoveOn;

	
	public bool combatHere;
	public bool merchantHere;
	public EncounterMerchantScriptable myMerchantScriptable;

	public bool canMoveOn;

	[Header("Time Variables")]
	public int timeTaken;
	public int timeTakenSecondary;
}
