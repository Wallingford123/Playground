using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPad : MonoBehaviour {

    public enum Pad
    {
        Bounce,
        Speed,
        Conveyor,
        Gravity
    }
    public Pad padType;

    public float conveyorSpeed, bounceForce, speedMultiplier;
    [Tooltip("If true, gravity pad will set gravity to negative value, if false, will set to positive (likely -1/1).")]
    public bool negativeGravity;
    public LayerMask affectedLayers;

    Vector2 refVal = Vector2.zero;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionStay2D(Collision2D collision)
    {
        switch (padType) {
            case Pad.Conveyor:
                if (collision.gameObject.tag != "Player" && collision.gameObject.GetComponent<Rigidbody2D>() != null)
                {
                    if (affectedLayers == (affectedLayers | 1 << collision.gameObject.layer))
                    {
                        Rigidbody2D rig = collision.gameObject.GetComponent<Rigidbody2D>();
                        rig.velocity = Vector2.SmoothDamp(rig.velocity, new Vector2(conveyorSpeed, 0), ref refVal, 0.1f, 1000, Time.fixedDeltaTime);
                    }
                }
                break;
        }

    }
}
