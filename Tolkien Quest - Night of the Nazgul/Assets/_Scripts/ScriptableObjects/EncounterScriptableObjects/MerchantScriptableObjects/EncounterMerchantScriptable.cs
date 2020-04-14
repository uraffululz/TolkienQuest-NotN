using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewMerchantScriptable", menuName = "ScriptableObjects/EncounterMerchantScriptable", order = 0)]
public class EncounterMerchantScriptable : ScriptableObject {

	[SerializeField] string[] notes;
	public string[] itemsForSale;
	public ScriptableObject[] itemScripts;
	public int[] objectQuantities;

	public int[] itemPrices;
	public enum coinTypes { silver, copper }
	public coinTypes[] coinType;


	void Start() {
        
    }


    void Update() {
        
    }
}
