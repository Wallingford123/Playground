using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item {

    public enum weaponTypeEnum
    {
        Ranged,
        Melee
    }
    public weaponTypeEnum weaponType;
    public float range, damage, clipSize;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
