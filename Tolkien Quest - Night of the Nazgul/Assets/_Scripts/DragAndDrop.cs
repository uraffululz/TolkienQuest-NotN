using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour {

	[SerializeField] GameObject sceneManager;
	[SerializeField] GameObject inventoryManager1;
	[SerializeField] GameObject inventoryManager1ArmorSlot;
	[SerializeField] GameObject inventoryManager1CloakSlot;
	[SerializeField] GameObject inventoryManager1DaggerSlot;

	[SerializeField] GameObject inventoryManager2;
	[SerializeField] GameObject inventoryManager2ArmorSlot;
	[SerializeField] GameObject inventoryManager2CloakSlot;
	[SerializeField] GameObject inventoryManager2DaggerSlot;

	[SerializeField] GameObject itemListBG;

	GraphicRaycaster myGRaycaster;
	PointerEventData myPointerEventData;
	EventSystem myEventSystem;
	Transform dragTransform;

	GameObject originalUIParent;
	GameObject newUIParent;

	RaycastResult originalResultUI;

	Vector3 mapPositionZOffset = new Vector3(0, 0, -.3f);


	void Start () {
		myGRaycaster = GetComponent<GraphicRaycaster>();
		myEventSystem = GetComponent<EventSystem>();
    }


	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			myPointerEventData = new PointerEventData(myEventSystem);
			myPointerEventData.position = Input.mousePosition;

			List<RaycastResult> results = new List<RaycastResult>();
			myGRaycaster.Raycast(myPointerEventData, results);

			foreach (RaycastResult originalResult in results) {
				if (originalResult.gameObject.tag == "DraggableUI") {
					dragTransform = originalResult.gameObject.transform;
					originalUIParent = originalResult.gameObject.transform.parent.gameObject;
					originalResultUI = originalResult;
				}
			}
		}

		else if (Input.GetMouseButton(0) && dragTransform != null) {
			//Each frame, move the dragTransform GameObject (if any) to the position of the mouse cursor
//TODO Try to get the dragged item to show up in front of the inventory backgrounds. For now, it slides behind them while being dragged
			//print(Input.mousePosition.z);
			dragTransform.position = Input.mousePosition + mapPositionZOffset;
		}

		else if (Input.GetMouseButtonUp(0) && dragTransform != null) {
			DropDraggedObject();

			if (newUIParent == null) {
				dragTransform.position = dragTransform.parent.position;
			}

			newUIParent = null;
			//originalUIParent = null;
			dragTransform = null;
		}
	}


	void DropDraggedObject () {
		myPointerEventData = new PointerEventData(myEventSystem);
		myPointerEventData.position = Input.mousePosition;

		List<RaycastResult> endResults = new List<RaycastResult>();
		myGRaycaster.Raycast(myPointerEventData, endResults);

		foreach (RaycastResult newResult in endResults) {
			//Debug.Log(result.gameObject.name);
			if (newResult.gameObject.tag == "DraggableUIParent") {
				if (newResult.gameObject.transform.childCount == 0) {
					if (sceneManager.name == "MapSceneManager") {
						if (itemListBG.GetComponent<ItemListBGScript>().canOnlyTakeOneItem) {
							bool placedInInventory = true;
							bool alreadyTookOne = false;

							for (int i = 0; i < itemListBG.GetComponent<ItemListBGScript>().positionParentList.Length; i++) {
								if (newResult.gameObject == itemListBG.GetComponent<ItemListBGScript>().positionParentList[i].gameObject) {
									//print("Item placed outside of inventory");
									placedInInventory = false;
									break;
								}
							}

							if (placedInInventory) {
								for (int i = 0; i < inventoryManager2.GetComponent<MapSceneInventoryManager>().inventoryParents.Length; i++) {
									if (inventoryManager2.GetComponent<MapSceneInventoryManager>().inventoryParents[i].transform.childCount != 0) {
										if (inventoryManager2.GetComponent<MapSceneInventoryManager>().inventoryParents[i].transform.GetChild(0).GetComponent<InventoryScriptableReader>().fromTakeOneList) {
											alreadyTookOne = true;
										}
									}
								}

								if (!alreadyTookOne) {
									DropItem(newResult);
								}
								else {
									print("You already took one!");
									dragTransform.position = dragTransform.parent.position;
								}
							}
							else {
								DropItem(newResult);
							}
						}
						else {
							DropItem(newResult);
						}
					}
					else {
						DropItem(newResult);
					}
					
				}
				else {
					//SWAP OR STACK ITEMS
					//TODO Objects should only stack if they are ITEMS of the same type
					if (newResult.gameObject != null && originalUIParent.gameObject != null) {
						if (newResult.gameObject != originalUIParent.gameObject) {
							if (dragTransform.gameObject.GetComponent<InventoryScriptableReader>() != null && newResult.gameObject.transform.GetChild(0).gameObject.GetComponent<InventoryScriptableReader>() != null) {
								//STACK SIMILAR ITEMS
								if (dragTransform.gameObject.GetComponent<InventoryScriptableReader>().objectScript == newResult.gameObject.transform.GetChild(0).gameObject.GetComponent<InventoryScriptableReader>().objectScript && !itemListBG.GetComponent<ItemListBGScript>().canOnlyTakeOneItem) {
									if (dragTransform.gameObject.GetComponent<InventoryScriptableReader>().itemScript != null && dragTransform.gameObject.GetComponent<InventoryScriptableReader>().itemScript.isStackable) {
										newResult.gameObject.transform.GetChild(0).gameObject.GetComponent<InventoryScriptableReader>().itemQuantity += dragTransform.gameObject.GetComponent<InventoryScriptableReader>().itemQuantity;
										//Display itemHost Item text and item quantity
										newResult.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = (newResult.gameObject.transform.GetChild(0).gameObject.GetComponent<InventoryScriptableReader>().objectName + " x " +
																																newResult.gameObject.transform.GetChild(0).gameObject.GetComponent<InventoryScriptableReader>().itemQuantity);
										Destroy(dragTransform.gameObject);
									}
									else {
										//Swap their parents/transform positions
										SwapObjectParents(newResult, newResult.gameObject);
									}
								}
								else {
									//If the objects attempting to stack/swap are different
									GameObject swappedUIParent = newResult.gameObject;
									bool canSwapHere = false;
									
									//If the new parent is the "Armor" slot
									if (swappedUIParent == inventoryManager1ArmorSlot || swappedUIParent == inventoryManager2ArmorSlot) {
										if (dragTransform.GetComponent<InventoryScriptableReader>().armorScript != null) {
											//print("Object put here is ARMOR");
											canSwapHere = true;
										}
									}
									//Else If the new parent is the "Cloak" slot
									else if (swappedUIParent == inventoryManager1CloakSlot || swappedUIParent == inventoryManager2CloakSlot) {
										//Probably nothing even needs to happen here. I just don't want the player to be able to put anything in this slot
										//print("I don't even think there ARE ANY cloaks in this game.");
									}
									//Else If the new parent is the "Dagger" slot
									else if (swappedUIParent == inventoryManager1DaggerSlot ||swappedUIParent == inventoryManager2DaggerSlot) {
										if (dragTransform.GetComponent<InventoryScriptableReader>().weaponScript != null) {
											if (dragTransform.GetComponent<InventoryScriptableReader>().weaponScript.MyWeaponType == InventoryWeaponScriptable.weaponTypes.Dagger) {
												//print("Weapon put here is a dagger");
												canSwapHere = true;
											}
										}
									}
									//Else if the new parent is NOT one of the "worn" item slots
									else {
										//If the item is being DRAGGED TO THE BACKPACK
										//If the object is dragged from the ARMOR SLOT to the BACKPACK
										if (originalUIParent == inventoryManager1ArmorSlot || originalUIParent == inventoryManager2ArmorSlot) {
											//print("Dragging item FROM ARMOR SLOT to BACKPACK");
											if (swappedUIParent.gameObject.transform.GetChild(0).GetComponent<InventoryScriptableReader>().armorScript != null) {
												canSwapHere = true;
											}
										}
										else if (originalUIParent == inventoryManager1CloakSlot || originalUIParent == inventoryManager2CloakSlot) {
											print("Dropping something into the CLOAK slot? I don't think so");
										}
										//If the object is dragged from the DAGGER SLOT to the BACKPACK
										else if (originalUIParent == inventoryManager1DaggerSlot || originalUIParent == inventoryManager2DaggerSlot) {
											//print("Dragging item FROM DAGGER SLOT to BACKPACK");
											if (swappedUIParent.gameObject.transform.GetChild(0).GetComponent<InventoryScriptableReader>().weaponScript != null &&
												swappedUIParent.gameObject.transform.GetChild(0).GetComponent<InventoryScriptableReader>().weaponScript.MyWeaponType == InventoryWeaponScriptable.weaponTypes.Dagger) {
												//print("Both items are daggers");
												canSwapHere = true;
											}
										}
										else {
											//If the object is dragged from the BACKPACK to the BACKPACK
											canSwapHere = true;
										}
									}

									if (canSwapHere) {
										SwapObjectParents(newResult, swappedUIParent);
									}
									else {
										print("I can't swap those items");
										dragTransform.position = dragTransform.parent.position;
									}
								}
							}
						}
						else {
							dragTransform.position = dragTransform.parent.position;
						}
					}
				}
			}
		}
	}

	void DropItem(RaycastResult result) {
		if (result.gameObject == inventoryManager2ArmorSlot || result.gameObject == inventoryManager1ArmorSlot) {
			Debug.Log("Dropped the item into the ARMOR slot");

			if (dragTransform.GetComponent<InventoryScriptableReader>().armorScript == null) {
				print("Object put here is NOT ARMOR");
				dragTransform.position = dragTransform.parent.position;
			}
			else {
				print("Object put here is ARMOR");

				newUIParent = result.gameObject;
				dragTransform.SetParent(newUIParent.transform);
				dragTransform.position = dragTransform.parent.position;
			}
		}
		else if (result.gameObject == inventoryManager2CloakSlot || result.gameObject == inventoryManager1CloakSlot) {
			print("I don't even think there ARE ANY cloaks in this game.");
			dragTransform.position = dragTransform.parent.position;
		}
		//If the new parent is the "Dagger" slot on the MapSceneInventoryManager or ItemListInventoryManager
		else if (result.gameObject == inventoryManager2DaggerSlot || result.gameObject == inventoryManager1DaggerSlot) {
			Debug.Log("Dropped the item into the DAGGER slot");

			if (dragTransform.GetComponent<InventoryScriptableReader>().weaponScript != null) {
				print("Dropped a WEAPON in this slot");

				if (dragTransform.GetComponent<InventoryScriptableReader>().weaponScript.MyWeaponType != InventoryWeaponScriptable.weaponTypes.Dagger) {
					print("Weapon put here is NOT a dagger");

					dragTransform.position = dragTransform.parent.position;
				}
				else {
					print("Weapon put here is a dagger");

					newUIParent = result.gameObject;
					dragTransform.SetParent(newUIParent.transform);
					dragTransform.position = dragTransform.parent.position;
				}
			}
			else {
				print("Object put here is not a dagger");
			}
		}
		else {
			newUIParent = result.gameObject;
			dragTransform.SetParent(newUIParent.transform);
			dragTransform.position = dragTransform.parent.position;

			//originalUIParent = newUIParent;
		}
	}

	void SwapObjectParents(RaycastResult newTransform, GameObject newParent) {
		//Swap object parents
		newTransform.gameObject.transform.GetChild(0).transform.SetParent(originalUIParent.transform);
		newUIParent = newParent.gameObject;
		dragTransform.SetParent(newUIParent.gameObject.transform);

		//Set object positions
		originalUIParent.transform.GetChild(0).transform.position = originalUIParent.transform.position + mapPositionZOffset;
		dragTransform.position = dragTransform.parent.position + mapPositionZOffset; //Do I need the mapPositoinZOffset here?
	}
}
