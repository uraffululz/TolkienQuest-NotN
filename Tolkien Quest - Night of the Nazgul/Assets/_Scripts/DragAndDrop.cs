using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour {

	GraphicRaycaster myGRaycaster;
	PointerEventData myPointerEventData;
	EventSystem myEventSystem;
	Transform dragTransform;

	GameObject originalUIParent;
	GameObject newUIParent;


	void Start() {
		myGRaycaster = GetComponent<GraphicRaycaster>();
		myEventSystem = GetComponent<EventSystem>();
    }


	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			myPointerEventData = new PointerEventData(myEventSystem);
			myPointerEventData.position = Input.mousePosition;

			List<RaycastResult> results = new List<RaycastResult>();
			myGRaycaster.Raycast(myPointerEventData, results);

			foreach (RaycastResult result in results) {
				if (result.gameObject.tag == "DraggableUI") {
					dragTransform = result.gameObject.transform;
					originalUIParent = result.gameObject.transform.parent.gameObject;
				}
			}
		}
		else if (Input.GetMouseButton(0) && dragTransform != null) {
			dragTransform.position = Input.mousePosition;
		}
		else if (Input.GetMouseButtonUp(0) && dragTransform != null) {
			myPointerEventData = new PointerEventData(myEventSystem);
			myPointerEventData.position = Input.mousePosition;

			List<RaycastResult> endResults = new List<RaycastResult>();
			myGRaycaster.Raycast(myPointerEventData, endResults);

			foreach (RaycastResult result in endResults) {
				//Debug.Log(result.gameObject.name);
				if (result.gameObject.tag == "DraggableUIParent") {
					if (result.gameObject.transform.childCount == 0) {
						newUIParent = result.gameObject;
						dragTransform.SetParent(result.gameObject.transform);
						dragTransform.position = dragTransform.parent.position;
					}
					else {
//TOMAYBEDO Have the new "dragged UI object" REPLACE the one that's already there
//and parent the old one to the new one's "originalUIParent"
						dragTransform.position = dragTransform.parent.position;
					}
				}
			}

			if (newUIParent == null) {
				dragTransform.position = dragTransform.parent.position;
			}

			newUIParent = null;
			originalUIParent = null;
			dragTransform = null;
		}
	}
}
