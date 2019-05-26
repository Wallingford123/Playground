using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupEffect : MonoBehaviour {

    // Use this for initialization
    public float pickupRadius, moveSpeed;
    public LayerMask playerMask;
    bool collecting = false;
    GameObject player;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        gameObject.layer = 9;
    }


    void Update()
    {
        if (Physics2D.OverlapCircle(this.transform.position, pickupRadius, playerMask) && gameObject.layer == 9)
        {
            player = Physics2D.OverlapCircle(this.transform.position, pickupRadius, playerMask).gameObject;
            GetComponent<Collider2D>().isTrigger = true;
            collecting = true;
        }
        if (collecting)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, moveSpeed);
            moveSpeed *= 1.05f;
        }
    }

}
