using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRaceScriptable", menuName = "ScriptableObjects/RaceScriptable", order = 0)]
public class RaceScriptable : ScriptableObject
{
	public string raceName;

	[TextArea(3, 10, order = 0)] public string FullRaceDescriptionFromBook;

	public int AdjustMeleeOB;
	public int AdjustRunning;
	public int AdjustGeneral;
	public int AdjustTrickery;

	public int AdjustWhenUndergroundGeneral;
	public int AdjustWhenUndergroundPerception;
	public int AdjustWhenUndergroundMagical;

	public int AdjustWhenOutdoorsPerception;
	public int AdjustWhenOutdoorsMagical;

	public bool ignoreNightRule1;
	public bool ignoreNightRule2;
	public bool ignoreNightRule3;
}
