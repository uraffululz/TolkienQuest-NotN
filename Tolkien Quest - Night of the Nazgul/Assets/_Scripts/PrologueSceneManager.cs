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

	[SerializeField] GameObject itemHost;

	[SerializeField] InventoryWeaponScriptable silverDagger;
	//[SerializeField] InventoryItemScriptable mapItem;
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
		//InventoryManager.startingItemTaken = itemTaken;

		//InventoryManager.silverCarried = 3;

		InventoryManager.daggerWorn = Instantiate(itemHost);
		InventoryManager.daggerWornScriptable = silverDagger as ScriptableObject;
		InventoryManager.daggerWorn.GetComponent<InventoryScriptableReader>().objectScript = silverDagger as ScriptableObject;

		//InventoryManager.slot1Item = Instantiate(itemHost);
		//InventoryManager.slot1Scriptable = mapItem as ScriptableObject;
		//InventoryManager.slot1Item.GetComponent<InventoryScriptableReader>().objectScript = mapItem as ScriptableObject;

		InventoryManager.slot1Item = Instantiate(itemHost);
		healingHerbs.itemQuantity = 3;
		InventoryManager.slot1Scriptable = healingHerbs as ScriptableObject;
		InventoryManager.slot1Item.GetComponent<InventoryScriptableReader>().objectScript = healingHerbs as ScriptableObject;
		//InventoryManager.slot1Item.GetComponent<InventoryScriptableReader>().itemQuantity = 3;

		InventoryManager.slot2Item = Instantiate(itemHost);
		lembasBread.itemQuantity = 7;
		InventoryManager.slot2Scriptable = lembasBread as ScriptableObject;
		InventoryManager.slot2Item.GetComponent<InventoryScriptableReader>().objectScript = lembasBread as ScriptableObject;
		//InventoryManager.slot2Item.GetComponent<InventoryScriptableReader>().itemQuantity = 7;



		if (itemTaken.GetType() == typeof(InventoryWeaponScriptable)) {
			TookAWeapon(itemTaken as InventoryWeaponScriptable);
		}
		else if (itemTaken.GetType() == typeof(InventoryArmorScriptable)) {
			TookArmor(itemTaken as InventoryArmorScriptable);
		}
	}


	void TookAWeapon (InventoryWeaponScriptable weaponTaken) {
		InventoryManager.slot3Item = Instantiate(itemHost);
		InventoryManager.slot3Scriptable = weaponTaken as ScriptableObject;
		InventoryManager.slot3Item.GetComponent<InventoryScriptableReader>().objectScript = weaponTaken as ScriptableObject;

		if (weaponTaken.itemIndex == 103) {
			InventoryManager.arrowsCarried = 10;
		}

		print("You took a weapon");

		//CompileInventoryItemList();
	}


	void TookArmor (InventoryArmorScriptable armorTaken) {
		InventoryManager.armorWorn = Instantiate(itemHost);
		InventoryManager.armorWornScriptable = armorTaken as ScriptableObject;
		InventoryManager.armorWorn.GetComponent<InventoryScriptableReader>().objectScript = armorTaken as ScriptableObject;
		print("You took the armor");

		//CompileInventoryItemList();

		print(InventoryManager.armorWorn.GetComponent<InventoryScriptableReader>().objectScript.name);
	}


	void CompileInventoryItemList() {
		InventoryManager.inventoryItems = new GameObject[] {InventoryManager.slot1Item, InventoryManager.slot2Item, InventoryManager.slot3Item,
			InventoryManager.slot4Item,InventoryManager.slot5Item,InventoryManager.slot6Item,InventoryManager.slot7Item,InventoryManager.slot8Item,
			InventoryManager.slot9Item,InventoryManager.slot10Item,InventoryManager.slot11Item,InventoryManager.slot12Item,};

		foreach (GameObject obj in InventoryManager.inventoryItems) {
			if (obj != null) {
				print(obj.GetComponent<InventoryScriptableReader>().objectScript.name);
			}
		}
	}


	public void ProceedToMapScene () {
		InventoryManager.silverCarried += 3;

		//Load the MapScene
		SceneManager.LoadScene("MapScene");

		print("Arrows: " + InventoryManager.arrowsCarried);
	}
}
