using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemListBGScript : MonoBehaviour {

	[SerializeField] GameObject itemListInventoryParent;
   
	public Text silverText;
	public Text copperText;

	public Image position1Parent;
	public Image position2Parent;
	public Image position3Parent;
	public Image position4Parent;
	public Image position5Parent;
	public Image position6Parent;

	public Image[] positionParentList;

	public Button itemListDoneButton;


	public void TakeAll() {
		foreach (Image objectPosition in positionParentList) {
			if (objectPosition.transform.childCount > 0) {
				FindNewBackpackParentSlot(itemListInventoryParent, objectPosition.transform.GetChild(0).gameObject);
			}
		}
	}


	//TOMAYBEDO Move this method to the MapSceneInventoryManager
	void FindNewBackpackParentSlot (GameObject inventoryManager, GameObject movedItemHost) {
		GameObject foundNewBackpackParent = null;
		bool foundEmptySlot = false;

		for (int i = 3; i < inventoryManager.GetComponent<MapSceneInventoryManager>().inventoryParents.Length; i++) {
			GameObject backpackParent = inventoryManager.GetComponent<MapSceneInventoryManager>().inventoryParents[i];

			if (backpackParent.transform.childCount > 0) {
				if (movedItemHost.GetComponent<InventoryScriptableReader>() != null && backpackParent.transform.GetChild(0).GetComponent<InventoryScriptableReader>() != null) {
					//STACK SIMILAR ITEMS
					if (movedItemHost.GetComponent<InventoryScriptableReader>().objectScript == backpackParent.transform.GetChild(0).GetComponent<InventoryScriptableReader>().objectScript) {
						if (movedItemHost.GetComponent<InventoryScriptableReader>().itemScript != null && movedItemHost.GetComponent<InventoryScriptableReader>().itemScript.isStackable) {
							backpackParent.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity += movedItemHost.GetComponent<InventoryScriptableReader>().itemQuantity;
							//Display itemHost Item text and item quantity
							backpackParent.transform.GetChild(0).GetComponent<Text>().text = (backpackParent.transform.GetChild(0).GetComponent<InventoryScriptableReader>().objectName + " x " +
																													backpackParent.transform.GetChild(0).GetComponent<InventoryScriptableReader>().itemQuantity);
							Destroy(movedItemHost.gameObject);

							foundNewBackpackParent = backpackParent.transform.gameObject;
							break;
						}
					}
				}
			}
			else {
				if (!foundEmptySlot) {
					foundNewBackpackParent = backpackParent.transform.gameObject;
					foundEmptySlot = true;
				}
			}
		}

		if (foundNewBackpackParent) {
			movedItemHost.transform.SetParent(foundNewBackpackParent.transform);
			movedItemHost.transform.position = foundNewBackpackParent.transform.position;

			//foundNewBackpackParent = true;
			//break;
		}
		else {
			print("Couldn't find a slot to move this item to");
		}
	}
}
