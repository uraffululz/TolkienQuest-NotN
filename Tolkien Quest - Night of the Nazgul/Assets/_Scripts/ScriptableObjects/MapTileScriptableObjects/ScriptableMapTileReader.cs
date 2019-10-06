using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableMapTileReader : MonoBehaviour {

	public MapTileScriptable myTileScriptable;

	//[SerializeField] GameObject MerchantUI;

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
	//public bool canExploreHere;
	public bool exploreRequiresRoll;
	public int exploreMinLimit;
	public int exploreIndex;

	public bool exploreHasMultipleRanges;
	public int howManyRanges;
	public RangeInt exploreRange1;
	//public int range1Min;
	//public int range1Max;
	public int range1FurtherIndex;
	public RangeInt exploreRange2;
	//public int range2Min;
	//public int range2Max;
	public int range2FurtherIndex;
	public RangeInt exploreRange3;
	//public int range3Min;
	//public int range3Max;
	public int range3FurtherIndex;
	public bool rangeFailMeansMoveOn;

	[SerializeField] bool hasCombat;

	//[SerializeField] bool hasMerchant;
	public EncounterMerchantScriptable myMerchant;


	[SerializeField] bool canMoveOn;

	[Header("Misc. Variables")]
	public int timeTaken;
	public int secondaryTimeTaken;
	public int XPGained;


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
			
			if (myTileScriptable.canExploreHere) {
				exploreRequiresRoll = myTileScriptable.exploreRequiresRoll;
				exploreMinLimit = myTileScriptable.exploreMinLimit;
				exploreIndex = myTileScriptable.exploreSecondaryIndex;
			}

			if (myTileScriptable.exploreHasMultipleRanges) {
				exploreHasMultipleRanges = myTileScriptable.exploreHasMultipleRanges;
				howManyRanges = myTileScriptable.howManyRanges;

				if (howManyRanges > 0) {
					exploreRange1 = new RangeInt(myTileScriptable.range1Min, myTileScriptable.range1Length);
					range1FurtherIndex = myTileScriptable.range1FurtherIndex;
				}

				if (howManyRanges > 1) {
					exploreRange2 = new RangeInt(myTileScriptable.range2Min, myTileScriptable.range2Length);
					range2FurtherIndex = myTileScriptable.range2FurtherIndex;
				}

				if (howManyRanges > 2) {
					exploreRange3 = new RangeInt(myTileScriptable.range3Min, myTileScriptable.range3Length);
					range3FurtherIndex = myTileScriptable.range3FurtherIndex;
				}

				rangeFailMeansMoveOn = myTileScriptable.rangeFailMeansMoveOn;
			}

			hasCombat = myTileScriptable.combatHere;
			
			if (myTileScriptable.merchantHere) {
				myMerchant = myTileScriptable.myMerchantScriptable;
			}
			
			canMoveOn = myTileScriptable.canMoveOn;

			timeTaken = myTileScriptable.timeTaken;
			secondaryTimeTaken = myTileScriptable.timeTakenSecondary;
		}
	}
}
