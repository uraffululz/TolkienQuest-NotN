using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScriptableReader : MonoBehaviour {

	public ScriptableObject objectScript;
	public string objectName;
	public InventoryItemScriptable itemScript;
	public InventoryWeaponScriptable weaponScript;
	public InventoryArmorScriptable armorScript;
	public enum objectType {item, weapon, armor};
	public objectType myObjectType;

	//ITEM Variables
	public bool itemAlreadyInitialized;
	public int itemQuantity;
//TODO I may not need these \/ variables, as if I need to search for their values, rather than searching for "this.isInfinite",
//I could just add one more word for "this.itemScript.isInfinite", which is probably less work overall.
//itemQuantity, though, isn't solely dependent on the item's ScriptableObject, but on how many of them I possess, so it stays here.
	public bool isInfinite;
	public bool isAMeal;
	public bool isACure;

	public bool fromTakeOneList;


	//WEAPON Variables


	//ARMOR Variables



	public void SetMyObjectType () {
		if (objectScript != null) {
			if (objectScript.GetType() == typeof(InventoryItemScriptable)) {
				itemScript = objectScript as InventoryItemScriptable;
				myObjectType = objectType.item;
				objectName = itemScript.itemName;
				
				InitializeItem();
			}
			else if (objectScript.GetType() == typeof(InventoryWeaponScriptable)) {
				weaponScript = objectScript as InventoryWeaponScriptable;
				myObjectType = objectType.weapon;
				objectName = weaponScript.itemName;
				

				//itemQuantity = weaponScript.weaponQuantity;

				//if (weaponScript.MyWeaponType == InventoryWeaponScriptable.weaponTypes.Dagger) {

				//}

				InitializeWeapon();
			}
			else if (objectScript.GetType() == typeof(InventoryArmorScriptable)) {
				armorScript = objectScript as InventoryArmorScriptable;
				myObjectType = objectType.armor;
				objectName = armorScript.itemName;

				//itemQuantity = armorScript.armorQuantity;

				InitializeArmor();
			}
		}
    }


	public void InitializeItem() {
		//gameObject.GetComponent<Text>().text = itemScript.itemName;
		if (!itemAlreadyInitialized) {
			if (itemQuantity == 0) {
				itemQuantity = itemScript.itemQuantity;
			}
			isInfinite = itemScript.isInfinite;

			isAMeal = itemScript.isAMeal;
			isACure = itemScript.isACure;

			itemAlreadyInitialized = true;
		}

		if (itemQuantity > 0) {
			//Debug.Log("Item was already initialized, so starting here.");
			if (itemScript.isInfinite) {
				gameObject.GetComponent<Text>().text = (itemScript.itemName);
			}
			else {
			//Debug.Log("Item is not infinite. New quantity: " + itemQuantity);
			gameObject.GetComponent<Text>().text = (itemScript.itemName + " x " + itemQuantity);
			//print(itemScript.itemName + " x " + itemQuantity);
			}
		}
		else {
			//Destroy this gameObject
			Destroy(gameObject);
		}
		
	}


	void InitializeWeapon() {
		if (!itemAlreadyInitialized) {
			if (itemQuantity == 0) {
				itemQuantity = weaponScript.weaponQuantity;
			}

			itemAlreadyInitialized = true;
		}

		if (itemQuantity > 0) {
			gameObject.GetComponent<Text>().text = weaponScript.itemName;
		}
		else {
			//Destroy this gameObject
			Destroy(gameObject);
		}
	}


	void InitializeArmor() {
		if (!itemAlreadyInitialized) {
			if (itemQuantity == 0) {
				itemQuantity = armorScript.armorQuantity;
			}

			itemAlreadyInitialized = true;
		}

		if (itemQuantity > 0) {
			gameObject.GetComponent<Text>().text = armorScript.itemName;
		}
		else {
			//Destroy this gameObject
			Destroy(gameObject);
		}
	}
}
