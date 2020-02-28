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


	void InitializeInventory () {
		//itemHosts = new GameObject[15] {null, null, null, null, null, null, null, null, null, null, null, null, null, null, null};
		inventoryScriptables = new ScriptableObject[15] {InventoryManager.cloakWornScriptable, InventoryManager.armorWornScriptable, InventoryManager.daggerWornScriptable,
			InventoryManager.slot1Scriptable, InventoryManager.slot2Scriptable, InventoryManager.slot3Scriptable, InventoryManager.slot4Scriptable, InventoryManager.slot5Scriptable,
			InventoryManager.slot6Scriptable, InventoryManager.slot7Scriptable, InventoryManager.slot8Scriptable, InventoryManager.slot9Scriptable, InventoryManager.slot10Scriptable,
			InventoryManager.slot11Scriptable, InventoryManager.slot12Scriptable};

		for (int i = 0; i < inventoryScriptables.Length; i++) {
			if (inventoryScriptables[i] != null) {
				GameObject newItemHost = Instantiate(itemHost, inventoryParents[i].transform.position, Quaternion.identity, inventoryParents[i].transform);

				newItemHost.GetComponent<InventoryScriptableReader>().objectScript = inventoryScriptables[i];
				newItemHost.GetComponent<Text>().text = newItemHost.GetComponent<InventoryScriptableReader>().objectScript.name;
				//itemHosts[i] = newItemHost;

				newItemHost.GetComponent<InventoryScriptableReader>().SetMyObjectType();

				print("Populated inventory");
			}
		}

		pouchParent.text = ("Silver: " + InventoryManager.silverCarried + " | Copper: " + InventoryManager.copperCarried);
	}
}
