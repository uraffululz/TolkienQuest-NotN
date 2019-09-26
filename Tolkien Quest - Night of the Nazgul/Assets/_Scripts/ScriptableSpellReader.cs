using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableSpellReader : MonoBehaviour {

	public SpellScriptable mySpellScriptable;

	public string spellName;
	public enum whichSpell{itemAnalysis, balance, calm, camouflage, charmAnimal, clairvoyance, fireBolt, healing, luck,
							protectionFromMagic, shield, speed, strength, sustainSelf, telekinesis, nothing};
	public whichSpell thisSpell;

	public int playerDamage;

	[TextArea(3, 10, order = 0)] public string spellDescriptionFromBook;

	public bool noDwarves;
	public bool noHobbits;


	void Awake () {
		InitializeSpell();
	}

	public void InitializeSpell () {
		if (mySpellScriptable != null) {
			spellName = mySpellScriptable.mySpellName;

			switch (mySpellScriptable.spellIndex) {
				case 0:
					thisSpell = whichSpell.itemAnalysis;
					break;
				case 1:
					thisSpell = whichSpell.balance; 
					break;
				case 2:
					thisSpell = whichSpell.calm;
					break;
				case 3:
					thisSpell = whichSpell.camouflage;
					break;
				case 4:
					thisSpell = whichSpell.charmAnimal;
					break;
				case 5:
					thisSpell = whichSpell.clairvoyance;
					break;
				case 6:
					thisSpell = whichSpell.fireBolt;
					break;
				case 7:
					thisSpell = whichSpell.healing;
					break;
				case 8:
					thisSpell = whichSpell.luck;
					break;
				case 9:
					thisSpell = whichSpell.protectionFromMagic;
					break;
				case 10:
					thisSpell = whichSpell.shield;
					break;
				case 11:
					thisSpell = whichSpell.speed;
					break;
				case 12:
					thisSpell = whichSpell.strength;
					break;
				case 13:
					thisSpell = whichSpell.sustainSelf;
					break;
				case 14:
					thisSpell = whichSpell.telekinesis;
					break;
				case 15:
					thisSpell = whichSpell.nothing;
					break;
				default:
					break;
			}

			playerDamage = mySpellScriptable.playerDamage;

			noDwarves = mySpellScriptable.notForDwarves;
			noHobbits = mySpellScriptable.notForHobbits;

			spellDescriptionFromBook = mySpellScriptable.fullSpellDescriptionFromBook;
		}
	}

	public void ClearSpellSlotScriptable () {
		mySpellScriptable = null;
		spellName = "";
		thisSpell = whichSpell.nothing;
		playerDamage = 0;
		spellDescriptionFromBook = "";
		noDwarves = false;
		noHobbits = false;
	}
}
