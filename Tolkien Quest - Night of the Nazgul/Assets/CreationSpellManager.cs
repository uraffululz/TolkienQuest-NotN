using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreationSpellManager : MonoBehaviour {

	[SerializeField] Button[] spellSlots;

	int skillPointsAssignedToSpells;

	[SerializeField] GameObject spellChoiceBG;



	void Start () {
        
    }


    void Update() {
        
    }


	public void IncreaseSpellSlots () {
		if (CharacterManager.skillPointsAvailable > 0) {
			skillPointsAssignedToSpells++;

			for (int i = 0; i < spellSlots.Length; i++) {
				if (i < skillPointsAssignedToSpells * 2) {
					spellSlots[i].interactable = true;
				}
				else {
					spellSlots[i].interactable = false;
				}
			}

			CharacterManager.skillPointsAvailable--;
			GetComponent<CreationSkillManager>().skillPointsRemainingText.text = ("Skill Points: " + CharacterManager.skillPointsAvailable);
		}
	}


	public void DecreaseSpellSlots () {
		if (CharacterManager.skillPointsAvailable < GetComponent<CreationSkillManager>().maxSkillPointsAvailable) {
			if (skillPointsAssignedToSpells > 0) {
				skillPointsAssignedToSpells--;

				for (int i = 0; i < spellSlots.Length; i++) {
					if (i < skillPointsAssignedToSpells * 2) {
						spellSlots[i].interactable = true;
					}
					else {
						spellSlots[i].interactable = false;
					}
				}

				CharacterManager.skillPointsAvailable++;
				GetComponent<CreationSkillManager>().skillPointsRemainingText.text = ("Skill Points: " + CharacterManager.skillPointsAvailable);
			}
		}
	}


	public void ConfirmSpells () {
//TODO Make sure all "Spell Slots" have a spell assigned to them before continuing
		GetComponent<CreationSceneManager>().CloseNewUIWindow(spellChoiceBG);
	}
}
