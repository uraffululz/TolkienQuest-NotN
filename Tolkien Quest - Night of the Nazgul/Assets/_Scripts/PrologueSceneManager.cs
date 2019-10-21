using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PrologueSceneManager : MonoBehaviour{

	[SerializeField] EncounterScriptable prologueScriptable;

	[SerializeField] string[] dialogues;
	[SerializeField] Text dialogueText;
	int dialogueIndex = 0;
	[SerializeField] GameObject continueDialogueButton;

	[SerializeField] GameObject striderImage;

	[SerializeField] GameObject takeSwordButton;
	[SerializeField] GameObject takeMaceButton;
	[SerializeField] GameObject takeBowButton;
	[SerializeField] GameObject takeArmorButton;

	[SerializeField] InventoryWeaponScriptable silverDagger;
	[SerializeField] InventoryItemScriptable mapItem;
	[SerializeField] InventoryItemScriptable healingHerbs;
	[SerializeField] InventoryItemScriptable lembasBread;


	void Awake () {
        dialogues = prologueScriptable.encounterText;
		dialogueText.text = dialogues[dialogueIndex];
    }


	public void ContinueDialogue () {
		dialogueIndex++;
		if (dialogueIndex == prologueScriptable.encounterText.Length) {
			continueDialogueButton.SetActive(false);
			takeSwordButton.SetActive(true);
			takeMaceButton.SetActive(true);
			takeBowButton.SetActive(true);
			takeArmorButton.SetActive(true);

		}
		else if (dialogueIndex < prologueScriptable.encounterText.Length) {
			dialogueText.text = dialogues[dialogueIndex];
			if (dialogueIndex == 5) {
				striderImage.SetActive(true);
			}
		}
	}

	public void TakeItem (ScriptableObject itemTaken) {
		InventoryManager.startingItemTaken = itemTaken;
		InventoryManager.silverCarried = 3;

		InventoryManager.daggerWorn = silverDagger;
		InventoryManager.slot1Item = mapItem;
		InventoryManager.slot2Item = healingHerbs;
		InventoryManager.slot3Item = lembasBread;


		if (itemTaken.GetType() == typeof(InventoryWeaponScriptable)) {
			TookAWeapon(itemTaken as InventoryWeaponScriptable);
		}
		else if (itemTaken.GetType() == typeof(InventoryArmorScriptable)) {
			TookArmor(itemTaken as InventoryArmorScriptable);
		}
	}


	void TookAWeapon (InventoryWeaponScriptable weaponTaken) {
		print("You took a weapon");
		InventoryManager.slot4Item = weaponTaken;
		if (weaponTaken.itemIndex == 103) {
			InventoryManager.arrowsCarried = 10;
		}
	}


	void TookArmor (InventoryArmorScriptable armorTaken) {
		print("You took the armor");
		InventoryManager.armorWorn = armorTaken;
	}


	public void ProceedToMapScene () {
		//Load the MapScene
		SceneManager.LoadScene("MapScene");

		print("Arrows: " + InventoryManager.arrowsCarried);
	}
}
