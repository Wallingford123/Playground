using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSwing : MonoBehaviour {

    GameObject player;
    PlayerController pop;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        pop = player.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        pop.canSwing = true;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        pop.canSwing = false;
    }
}
