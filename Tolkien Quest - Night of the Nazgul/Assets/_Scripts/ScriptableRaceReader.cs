using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableRaceReader : MonoBehaviour {

	[SerializeField] RaceScriptable myRaceScriptable;

	public string raceName;

	[TextArea(3, 10, order = 0)] public string raceDescriptionFromBook;

	public int myMeleeOBAdjust;
	public int myRunningAdjust;
	public int myGeneralAdjust;
	public int myTrickeryAdjust;

	public int myUndergroundGeneralAdjust;
	public int myUndergroundPerceptionAdjust;
	public int myUndergroundMagicalAdjust;

	public int myOutdoorsPerceptionAdjust;
	public int myOutdoorsMagicalAdjust;

	public bool willIgnoreNightRule1;
	public bool willIgnoreNightRule2;
	public bool willIgnoreNightRule3;


	void Awake () {
		raceName = myRaceScriptable.raceName;
		raceDescriptionFromBook = myRaceScriptable.FullRaceDescriptionFromBook;

		myMeleeOBAdjust = myRaceScriptable.AdjustMeleeOB;
		myRunningAdjust = myRaceScriptable.AdjustRunning;
		myGeneralAdjust = myRaceScriptable.AdjustGeneral;
		myTrickeryAdjust = myRaceScriptable.AdjustTrickery;

		myUndergroundGeneralAdjust = myRaceScriptable.AdjustWhenUndergroundGeneral;
		myUndergroundPerceptionAdjust = myRaceScriptable.AdjustWhenUndergroundPerception;
		myUndergroundMagicalAdjust = myRaceScriptable.AdjustWhenUndergroundMagical;

		myOutdoorsPerceptionAdjust = myRaceScriptable.AdjustWhenOutdoorsPerception;
		myOutdoorsMagicalAdjust = myRaceScriptable.AdjustWhenOutdoorsMagical;

		willIgnoreNightRule1 = myRaceScriptable.ignoreNightRule1;
		willIgnoreNightRule2 = myRaceScriptable.ignoreNightRule2;
		willIgnoreNightRule3 = myRaceScriptable.ignoreNightRule3;
	}
}
