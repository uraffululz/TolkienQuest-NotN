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

	
	public bool combatHere;
	public bool merchantHere;
	public EncounterMerchantScriptable myMerchantScriptable;

	public bool canMoveOn;

	[Header("Time Variables")]
	public int timeTaken;
	public int timeTakenSecondary;
}
