using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSceneInventoryManager : MonoBehaviour {

	[SerializeField] GameObject itemHost;

	public Text pouchParent;
	public GameObject[] inventoryParents;
	//public GameObject[] itemHosts;

	public ScriptableObject[] inventoryScriptables;
	
	
	void Start() {
		InitializeInventory();
	}


	public void InitializeInventory () {
		//itemHosts = new GameObject[15] {null, null, null, null, null, null, null, null, null, null, null, null, null, null, null};
		inventoryScriptables = new ScriptableObject[15] {InventoryManager.cloakWornScriptable, InventoryManager.armorWornScriptable, InventoryManager.daggerWornScriptable,
			InventoryManager.slot1Scriptable, InventoryManager.slot2Scriptable, InventoryManager.slot3Scriptable, InventoryManager.slot4Scriptable, InventoryManager.slot5Scriptable,
			InventoryManager.slot6Scriptable, InventoryManager.slot7Scriptable, InventoryManager.slot8Scriptable, InventoryManager.slot9Scriptable, InventoryManager.slot10Scriptable,
			InventoryManager.slot11Scriptable, InventoryManager.slot12Scriptable};

		for (int i = 0; i < inventoryScriptables.Length; i++) {
			if (inventoryScriptables[i] != null && inventoryParents[i].transform.childCount == 0) {
				GameObject newItemHost = Instantiate(itemHost, inventoryParents[i].transform.position, Quaternion.identity, inventoryParents[i].transform);

				newItemHost.GetComponent<InventoryScriptableReader>().objectScript = inventoryScriptables[i];
				//newItemHost.GetComponent<InventoryScriptableReader>().itemQuantity = ;

				newItemHost.GetComponent<Text>().text = newItemHost.GetComponent<InventoryScriptableReader>().objectScript.name;
				//itemHosts[i] = newItemHost;

				newItemHost.GetComponent<InventoryScriptableReader>().SetMyObjectType();

				print("Populated inventory");
			}
		}

		pouchParent.text = ("Silver: " + InventoryManager.silverCarried + " | Copper: " + InventoryManager.copperCarried);
	}


	public void LogInventory() {
		for (int i = 0; i < inventoryParents.Length; i++) {
			if (inventoryParents[i].transform.childCount != 0) {
				inventoryScriptables[i] = inventoryParents[i].GetComponentInChildren<InventoryScriptableReader>().objectScript;

				switch (i) {
					case 0:
						InventoryManager.cloakQuantity = inventoryParents[0].GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
						break;
					case 1:
						InventoryManager.armorQuantity = inventoryParents[1].GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
						break;
					case 2:
						InventoryManager.daggerQuantity = inventoryParents[2].GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
						break;
					case 3:
						InventoryManager.slot1Quantity = inventoryParents[3].GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
						break;
					case 4:
						InventoryManager.slot2Quantity = inventoryParents[4].GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
						break;
					case 5:
						InventoryManager.slot3Quantity = inventoryParents[5].GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
						break;
					case 6:
						InventoryManager.slot4Quantity = inventoryParents[6].GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
						break;
					case 7:
						InventoryManager.slot5Quantity = inventoryParents[7].GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
						break;
					case 8:
						InventoryManager.slot6Quantity = inventoryParents[8].GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
						break;
					case 9:
						InventoryManager.slot7Quantity = inventoryParents[9].GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
						break;
					case 10:
						InventoryManager.slot8Quantity = inventoryParents[10].GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
						break;
					case 11:
						InventoryManager.slot9Quantity = inventoryParents[11].GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
						break;
					case 12:
						InventoryManager.slot10Quantity = inventoryParents[12].GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
						break;
					case 13:
						InventoryManager.slot11Quantity = inventoryParents[13].GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
						break;
					case 14:
						InventoryManager.slot12Quantity = inventoryParents[14].GetComponentInChildren<InventoryScriptableReader>().itemQuantity;
						break;
					default:
						print("Something fucked up here.");
						break;
				}
			}
		}

		InventoryManager.cloakWornScriptable = inventoryScriptables[0];
		InventoryManager.armorWornScriptable = inventoryScriptables[1];
		InventoryManager.daggerWornScriptable = inventoryScriptables[2];
		InventoryManager.slot1Scriptable = inventoryScriptables[3];
		InventoryManager.slot2Scriptable = inventoryScriptables[4];
		InventoryManager.slot3Scriptable = inventoryScriptables[5];
		InventoryManager.slot4Scriptable = inventoryScriptables[6];
		InventoryManager.slot5Scriptable = inventoryScriptables[7];
		InventoryManager.slot6Scriptable = inventoryScriptables[8];
		InventoryManager.slot7Scriptable = inventoryScriptables[9];
		InventoryManager.slot8Scriptable = inventoryScriptables[10];
		InventoryManager.slot9Scriptable = inventoryScriptables[11];
		InventoryManager.slot10Scriptable = inventoryScriptables[12];
		InventoryManager.slot11Scriptable = inventoryScriptables[13];
		InventoryManager.slot12Scriptable = inventoryScriptables[14];

		InitializeInventory();

/*		print("Item Quantities: " + InventoryManager.cloakQuantity + ", " +
			InventoryManager.armorQuantity + ", " +
			InventoryManager.daggerQuantity + ", " +
			InventoryManager.slot1Quantity + ", " +
			InventoryManager.slot2Quantity + ", " +
			InventoryManager.slot3Quantity + ", " +
			InventoryManager.slot4Quantity + ", " +
			InventoryManager.slot5Quantity + ", " +
			InventoryManager.slot6Quantity + ", " +
			InventoryManager.slot7Quantity + ", " +
			InventoryManager.slot8Quantity + ", " +
			InventoryManager.slot9Quantity + ", " +
			InventoryManager.slot10Quantity + ", " +
			InventoryManager.slot11Quantity + ", " +
			InventoryManager.slot12Quantity);
*/
	}
}
