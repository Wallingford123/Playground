using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour {

    public float range, damage, reloadTime, clipSize, recoilForce;
    public int ammoID, weaponID;
    List<GameObject> ammoList;
    private void Start()
    {
        ammoList = GameObject.Find("Manager").GetComponent<Lists>().ammo;
    }
    public void Fire(Vector2 targetPos) {
        switch (weaponID)
        {
            case 0:
                Debug.Log("Fired at: " + targetPos);
                break;
            default:
                break;
        }
    }

}
