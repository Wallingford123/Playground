using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonScript : MonoBehaviour {

    [HideInInspector]
    public GameObject currentObject;

	// Use this for initialization
	void Start () {
        Debug.Log("wat");
        currentObject = Instantiate(GameObject.Find("Inventory").GetComponent<Inventory>().empty,this.transform);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
