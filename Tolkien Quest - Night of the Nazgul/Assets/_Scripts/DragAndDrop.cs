using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour {

	[SerializeField] GameObject sceneManager;
	[SerializeField] GameObject inventoryManager1;
	[SerializeField] GameObject inventoryManager2;

	[SerializeField] GameObject inventoryManager2ArmorSlot;
	[SerializeField] GameObject inventoryManager2CloakSlot;
	[SerializeField] GameObject inventoryManager2DaggerSlot;

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
		//RaycastResult originalResultUI;

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
			dragTransform.position = Input.mousePosition;
		}

		else if (Input.GetMouseButtonUp(0) && dragTransform != null) {
			myPointerEventData = new PointerEventData(myEventSystem);
			myPointerEventData.position = Input.mousePosition;

			List<RaycastResult> endResults = new List<RaycastResult>();
			myGRaycaster.Raycast(myPointerEventData, endResults);

			foreach (RaycastResult newResult in endResults) {
				//Debug.Log(result.gameObject.name);
				if (newResult.gameObject.tag == "DraggableUIParent") {
					if (newResult.gameObject.transform.childCount == 0) {
						Debug.Log(newResult.gameObject.name + " || " + inventoryManager2ArmorSlot.name);
						if (newResult.gameObject == inventoryManager2ArmorSlot) {
							Debug.Log("Dropped the item into the ARMOR slot");

							if (dragTransform.GetComponent<InventoryScriptableReader>().armorScript == null) {
								print("Object put here is NOT ARMOR");
								dragTransform.position = dragTransform.parent.position;
							}
							else {
								print("Object put here is ARMOR");

								newUIParent = newResult.gameObject;
								dragTransform.SetParent(newUIParent.transform);
								dragTransform.position = dragTransform.parent.position;
							}
							
						}
						else if (newResult.gameObject == inventoryManager2CloakSlot) {
							print("I don't even think there ARE ANY cloaks in this game.");
							//if (dragTransform.GetComponent<InventoryScriptableReader>().cl != null) {

							//}

							dragTransform.position = dragTransform.parent.position;
						}
						//If the new parent is the "Dagger" slot on the MapSceneInventoryManager
						else if (newResult.gameObject == inventoryManager2DaggerSlot) {
							Debug.Log("Dropped the item into the DAGGER slot");

							if (dragTransform.GetComponent<InventoryScriptableReader>().weaponScript != null) {
								print("Dropped a WEAPON in this slot");

								if (dragTransform.GetComponent<InventoryScriptableReader>().weaponScript.MyWeaponType != InventoryWeaponScriptable.weaponTypes.Dagger) {
									print("Weapon put here is NOT a dagger");

									dragTransform.position = dragTransform.parent.position;
								}
								else {
									print("Weapon put here is a dagger");

									newUIParent = newResult.gameObject;
									dragTransform.SetParent(newUIParent.transform);
									dragTransform.position = dragTransform.parent.position;
								}
							}
						}

						else {
							newUIParent = newResult.gameObject;
							dragTransform.SetParent(newUIParent.transform);
							dragTransform.position = dragTransform.parent.position;

							//originalUIParent = newUIParent;
						}
					}
					else {
//TODO Objects should only stack if they are ITEMS of the same type
						if (newResult.gameObject != null &&  originalUIParent.gameObject != null) {
							if (newResult.gameObject != originalUIParent.gameObject) {
								if (dragTransform.gameObject.GetComponent<InventoryScriptableReader>() != null && newResult.gameObject.transform.GetChild(0).gameObject.GetComponent<InventoryScriptableReader>() != null) {
//TODO Be sure to only stack/destroy ITEMS (Not weapons/armor/cloaks)
									if (dragTransform.gameObject.GetComponent<InventoryScriptableReader>().objectScript == newResult.gameObject.transform.GetChild(0).gameObject.GetComponent<InventoryScriptableReader>().objectScript) {
										//When stacking similar objects from/on the EncounterObtainedItemList
										if (dragTransform.gameObject.GetComponent<InventoryScriptableReader>().itemScript.isStackable) {
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
										//If the dragged item is different than the parented object's child object
										GameObject swappedUIParent = newResult.gameObject/*.transform.parent.gameObject*/;

										Debug.Log(swappedUIParent.gameObject.name + " || " +  inventoryManager2ArmorSlot.name);

										//If the new parent is the "Armor" slot on the MapSceneInventoryManager
										if (swappedUIParent == inventoryManager2ArmorSlot) {
											//print("You put an object in t");
											if (dragTransform.GetComponent<InventoryScriptableReader>().armorScript == null/*myObjectType != InventoryScriptableReader.objectType.armor*/) {
												//print("Object put here is NOT ARMOR");
												
												dragTransform.position = dragTransform.parent.position;
											}
											else {
												//print("Object put here is ARMOR");
												
												SwapObjectParents(newResult, swappedUIParent);
											}
										}
										//Else If the new parent is the "Cloak" slot on the MapSceneInventoryManager
										else if (swappedUIParent == inventoryManager2CloakSlot) {
											//print("I don't even think there ARE ANY cloaks in this game.");
											//if (dragTransform.GetComponent<InventoryScriptableReader>().cl != null) {

											//}

											dragTransform.position = dragTransform.parent.position;
										}
										//Else If the new parent is the "Dagger" slot on the MapSceneInventoryManager
										else if (swappedUIParent == inventoryManager2DaggerSlot) {
											//Debug.Log("Items are both " + swappedUIParent.gameObject.name);

											if (dragTransform.GetComponent<InventoryScriptableReader>().weaponScript != null) {
												//print("Dropped a WEAPON in this slot");

												if (dragTransform.GetComponent<InventoryScriptableReader>().weaponScript.MyWeaponType != InventoryWeaponScriptable.weaponTypes.Dagger) {
													//print("Weapon put here is NOT a dagger");

													dragTransform.position = dragTransform.parent.position;
												}
												else {
													//print("Weapon put here is a dagger");

													SwapObjectParents(newResult, swappedUIParent);
												} 
											}
										}
										//Else if the new parent is NOT one of the "worn" item slots
										else {
//TODO Make sure the "worn" object slots don't swap with the wrong objects when being moved FROM the "worn" slot to a "backpack" slot
//i.e. When moving a "dagger" object FROM the Dagger slot to a backpack slot with a "Healing Herb" in it, the Herb isn't automatically swapped into the Dagger slot
											

											SwapObjectParents(newResult, swappedUIParent);
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

			if (newUIParent == null) {
				dragTransform.position = dragTransform.parent.position;
			}

			newUIParent = null;
			//originalUIParent = null;
			dragTransform = null;
		}
	}

	void SwapObjectParents(RaycastResult newTransform, GameObject newParent) {
		//Swap object parents
		newTransform.gameObject.transform.GetChild(0).transform.SetParent(originalUIParent.transform);
		newUIParent = newParent.gameObject;
		dragTransform.SetParent(newUIParent.gameObject.transform);

		//Swap object positions
		originalUIParent.transform.GetChild(0).transform.position = originalUIParent.transform.position + mapPositionZOffset;
		dragTransform.position = dragTransform.parent.position;
	}
}
