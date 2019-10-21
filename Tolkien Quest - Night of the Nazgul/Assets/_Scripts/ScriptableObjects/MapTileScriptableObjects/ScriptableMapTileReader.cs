using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableMapTileReader : MonoBehaviour {

	public MapTileScriptable myTileScriptable;

	//[SerializeField] GameObject MerchantUI;

	public string myLocationID;
	public bool allowedToClickMapTile;

	[TextArea(3, 10, order = 0)] public string[] locationText;

	[Header("Encounter Text Variables")]
	public string encounterOptionText1;
	public int encounterOptionIndex1;
	public string encounterOptionText2;
	public int encounterOptionIndex2;
	public string encounterOptionText3;
	public int encounterOptionIndex3;
	public string encounterOptionText4;
	public int encounterOptionIndex4;

	[Header("Exploration Variables")]
	public MapTileScriptable.actionSkill addSkillToAction;

	public bool exploreHasMultipleRanges;
	public int howManyRanges;
	public RangeInt exploreRange1;
	public int range1FurtherIndex;
	public RangeInt exploreRange2;
	public int range2FurtherIndex;
	public RangeInt exploreRange3;
	public int range3FurtherIndex;
	public RangeInt exploreRange4;
	public int range4FurtherIndex;
	public bool rangeFailMeansMoveOn;
	public bool canGetLost;
	public int randomDirectionChoiceMax;

	[SerializeField] bool hasCombat;

	//[SerializeField] bool hasMerchant;
	public EncounterMerchantScriptable myMerchant;


	[SerializeField] bool canMoveOn;

	[Header("Misc. Variables")]
	public int timeTaken;
	public int secondaryTimeTaken;
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
	public int XPGained;

	[Header("Unique Tile Rule Variables")]
	public bool changesPlayerLocation;
	public string changedLocationID;

	public bool previousLocationsConsidered;
	public string[] locationsConsidered;
	public int locationBasedEncounter;
	public int alternateLocationEncounter;


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
			encounterOptionText4 = myTileScriptable.encounterText4;
			encounterOptionIndex4 = myTileScriptable.encounterIndex4;
			addSkillToAction = myTileScriptable.actionAddsSkill;

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
				if (howManyRanges > 3) {
					exploreRange4 = new RangeInt(myTileScriptable.range4Min, myTileScriptable.range4Length);
					range4FurtherIndex = myTileScriptable.range4FurtherIndex;
				}

				rangeFailMeansMoveOn = myTileScriptable.rangeFailMeansMoveOn;
				canGetLost = myTileScriptable.canGetLost;
				randomDirectionChoiceMax = myTileScriptable.randomDirectionRangeMax;

			}

			hasCombat = myTileScriptable.combatHere;
			
			if (myTileScriptable.merchantHere) {
				myMerchant = myTileScriptable.myMerchantScriptable;
			}
			
			canMoveOn = myTileScriptable.canMoveOn;

			timeTaken = myTileScriptable.timeTaken;
			secondaryTimeTaken = myTileScriptable.timeTakenSecondary;
			canRestAtNight = myTileScriptable.canRestAtNight;

			timeLocked = myTileScriptable.timeLocked;
			daysLimit1 = myTileScriptable.daysLimit1;
			timeLimit1 = myTileScriptable.timeLimit1;
			limit1Index = myTileScriptable.limit1Index;
			daysLimit2 = myTileScriptable.daysLimit2;
			timeLimit2 = myTileScriptable.timeLimit2;
			limit2Index = myTileScriptable.limit2Index;
			overTimeIndex = myTileScriptable.overTimeIndex;

			XPGained = myTileScriptable.experience;

			changesPlayerLocation = myTileScriptable.changesPlayerLocation;
			changedLocationID = myTileScriptable.changedLocationID;

			previousLocationsConsidered = myTileScriptable.previousLocationsConsidered;
			locationsConsidered = myTileScriptable.locationsConsidered;
			locationBasedEncounter = myTileScriptable.locationBasedEncounter;
			alternateLocationEncounter = myTileScriptable.alternateLocationEncounter;
		}
	}
}
