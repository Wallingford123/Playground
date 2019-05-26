using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour {

    public float damage, knockbackForce;
    PlayerController pCon;
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(pCon == null)pCon = collision.gameObject.GetComponent<PlayerController>();
        if (collision.gameObject.tag != "Player")
        {
            Destroy(collision.gameObject);
        }
        else if(pCon.canTakeDamage)
        {
            pCon.TakeDamage(damage);
            pCon.Bounce(knockbackForce, Vector2.up, 0f);
        }
    }
}
