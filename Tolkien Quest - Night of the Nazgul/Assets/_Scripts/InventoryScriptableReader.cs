using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScriptableReader : MonoBehaviour {

	public ScriptableObject objectScript;
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


	//WEAPON Variables


	//ARMOR Variables



	public void SetMyObjectType () {
		if (objectScript != null) {
			if (objectScript.GetType() == typeof(InventoryItemScriptable)) {
				itemScript = objectScript as InventoryItemScriptable;
				myObjectType = objectType.item;
				
				InitializeItem();
			}
			else if (objectScript.GetType() == typeof(InventoryWeaponScriptable)) {
				weaponScript = objectScript as InventoryWeaponScriptable;
				myObjectType = objectType.weapon;

				//itemQuantity = weaponScript.weaponQuantity;

				InitializeWeapon();
			}
			else if (objectScript.GetType() == typeof(InventoryArmorScriptable)) {
				armorScript = objectScript as InventoryArmorScriptable;
				myObjectType = objectType.armor;

				//itemQuantity = armorScript.armorQuantity;

				InitializeArmor();
			}
		}
    }


	public void InitializeItem() {
		//gameObject.GetComponent<Text>().text = itemScript.itemName;
		if (!itemAlreadyInitialized) {
			itemQuantity = itemScript.itemQuantity;
			isInfinite = itemScript.isInfinite;

			isAMeal = itemScript.isAMeal;
			isACure = itemScript.isACure;

			itemAlreadyInitialized = true;
		}

		if (itemQuantity > 0) {
			gameObject.GetComponent<Text>().text = (itemScript.itemName + " x " + itemQuantity);
		}
		else {
			//Destroy this gameObject
			Destroy(gameObject);
		}
		
	}


	void InitializeWeapon() {

	}


	void InitializeArmor() {

	}
}
