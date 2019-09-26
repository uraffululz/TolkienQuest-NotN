using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpellScriptable", menuName = "ScriptableObjects/SpellScriptable", order = 0)]
public class SpellScriptable : ScriptableObject {

	public string mySpellName;
	public int spellIndex;

	public int playerDamage;

	[TextArea(3, 10, order = 0)] public string fullSpellDescriptionFromBook;

	public bool notForDwarves;
	public bool notForHobbits;
}
