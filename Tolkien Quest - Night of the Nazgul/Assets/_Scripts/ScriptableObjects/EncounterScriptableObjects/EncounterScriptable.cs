using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEncounterScriptable", menuName = "ScriptableObjects/EncounterScriptable", order = 0)]
public class EncounterScriptable : ScriptableObject {

	public int encounterIndex;

	[TextArea(2, 10, order = 0)] public string[] encounterText;

	public bool isCombat;

	public bool isMerchant;
}
