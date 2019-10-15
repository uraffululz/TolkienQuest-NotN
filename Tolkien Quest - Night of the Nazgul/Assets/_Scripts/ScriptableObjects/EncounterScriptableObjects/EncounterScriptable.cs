using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEncounterScriptable", menuName = "ScriptableObjects/EncounterScriptable", order = 0)]
public class EncounterScriptable : ScriptableObject {

	public int encounterIndex;

	[TextArea(2, 10, order = 0)] public string[] encounterText;

	public bool isCombat;
	public bool isMerchant;
	public bool canMoveOn;

	[Header("Further Encounter Options", order = 0)]
	public string furtherEncounter1Text;
	public int furtherEncounter1Index;

	public string furtherEncounter2Text;
	public int furtherEncounter2Index;

	public string furtherEncounter3Text;
	public int furtherEncounter3Index;

	[Header("Misc. Variables", order = 1)]
	public int timeTaken;

	[Header("Experience Point Variables")]
	public int XPGained;
}
