using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour {

    public enum grabSide {
        Right,
        Left
    }
    public grabSide side;

    PlayerController con;
    Rigidbody2D rig;
    Vector2 currentVelocity;
    public GameObject Player;
    public float leapAngle;
    float timer;
    [HideInInspector]
    public bool isOnWall;
    float cd = 0.3f;
	// Use this for initialization
	void Start () {
        con = Player.GetComponent<PlayerController>();
        rig = Player.GetComponent<Rigidbody2D>();
        timer = Time.time;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        currentVelocity = rig.velocity;
        if(isOnWall && Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("TRYING TO JUMP");
            if (side == grabSide.Left)
            {
                con.Bounce(con.jumpForce, new Vector2(leapAngle, 180).normalized,cd);
                timer = Time.time + cd;
                Debug.Log("ASDASD");
            }
            if (side == grabSide.Right)
            {
                con.Bounce(con.jumpForce, new Vector2(-leapAngle, 180).normalized, cd);
                Debug.Log("ASDASD");
                timer = Time.time + cd;
            }
        }
	}
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == ("Wall") && !con.isGrounded)
        {
            switch (side)
            {
                case grabSide.Left:
                    if (Input.GetKey(KeyCode.A) && timer < Time.time)
                    {
                        isOnWall = true;
                        rig.velocity = Vector2.zero;
                    }
                    else
                    {
                        isOnWall = false;
                    }
                    break;
                case grabSide.Right:
                    if (Input.GetKey(KeyCode.D) && timer < Time.time)
                    {
                        isOnWall = true;
                        rig.velocity = Vector2.zero;
                    }
                    else isOnWall = false;
                    Debug.Log("WASD");
                    break;
                default:
                    break;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == ("Wall"))
        {
            switch (side)
            {
                case grabSide.Left:
                    isOnWall = false;
                    Debug.Log("LeftWall");
                    break;
                case grabSide.Right:
                    isOnWall = false;
                    Debug.Log("RightWall");
                    break;
                default:
                    break;
            }
        }
    }
}
