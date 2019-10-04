using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableMapTileReader : MonoBehaviour {

	public MapTileScriptable myTileScriptable;

	[SerializeField] GameObject MerchantUI;

	[SerializeField] string myLocationID;

	[TextArea(3, 10, order = 0)] public string[] locationText;

	[Header("Encounter Text Variables")]
	public string encounterOptionText1;
	public int encounterOptionIndex1;
	public string encounterOptionText2;
	public int encounterOptionIndex2;
	public string encounterOptionText3;
	public int encounterOptionIndex3;

	[Header("Exploration Variables")]
	[SerializeField] bool canExploreHere;
	[SerializeField] bool exploreRequiresRoll;
	[SerializeField] int exploreMinLimit;

	[SerializeField] bool hasCombat;

	[SerializeField] bool hasMerchant;
	public EncounterMerchantScriptable myMerchant;


	[SerializeField] bool canMoveOn;

	[Header("Time Variables")]
	[SerializeField] int timeTaken;


	void Awake() {
		if (myTileScriptable != null) {
			myLocationID = myTileScriptable.locationID;
			locationText = myTileScriptable.locationText;
			encounterOptionText1 = myTileScriptable.encounterText1;
			encounterOptionIndex1 = myTileScriptable.encounterIndex1;
			encounterOptionText2 = myTileScriptable.encounterText2;
			encounterOptionIndex2 = myTileScriptable.encounterIndex2;
			encounterOptionText3 = myTileScriptable.encounterText3;
			encounterOptionIndex3 = myTileScriptable.encounterIndex3;

			canExploreHere = myTileScriptable.canExploreHere;
			if (canExploreHere) {
				exploreRequiresRoll = myTileScriptable.exploreRequiresRoll;
			}

			hasCombat = myTileScriptable.combatHere;
			hasMerchant = myTileScriptable.merchantHere;

			if (hasMerchant) {
				myMerchant = myTileScriptable.myMerchantScriptable;
			}
			
			canMoveOn = myTileScriptable.canMoveOn;

			timeTaken = myTileScriptable.timeTaken;
		}
	}
}
