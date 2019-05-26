using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour {

    GameObject player;
    public int hopForce;
    PlayerController pop;
    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        pop = player.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        pop.canClimb = true;
        pop.ladderX = transform.position.x;

    }
    private void OnTriggerExit2D(Collider2D collision) {

        pop.canClimb = false;
        if (!pop.isGrounded && pop.isClimbing) {
            pop.Bounce(hopForce, Vector2.up, 0);
            player.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
}
