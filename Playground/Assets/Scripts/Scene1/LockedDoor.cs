using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour {

    public enum doorUnlockType {
        KEY,
        BUTTON,
        LEVER
    }
    public doorUnlockType unlockType;
    public LayerMask playerMask;
    [Tooltip("Time in seconds door will remain open after BUTTON press. (0 = Permanently unlocked)")]
    public float openTime;
    public GameObject door;
    public Vector2 keyUnlockSize;

    bool conditionComplete;
    bool open;
    float buttonTimer;

    // Use this for initialization
    void Start () {
        conditionComplete = false;
        buttonTimer = Time.time;
        open = false;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (unlockType) {
            case doorUnlockType.KEY:
                if (collision.gameObject.tag == "Player" && !conditionComplete)
                {
                    conditionComplete = true;
                    GetComponent<SpriteRenderer>().enabled = false;
                    GetComponent<BoxCollider2D>().enabled = false;
                }
                break;
            case doorUnlockType.BUTTON:
                if (collision.gameObject.tag == "Player" && !conditionComplete)
                {
                    transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y / 1.5f);
                    transform.position = new Vector2(transform.position.x, transform.position.y - 0.1f);
                    buttonTimer = Time.time + openTime;
                    conditionComplete = true;
                }
                break;
            case doorUnlockType.LEVER:
                if (collision.gameObject.tag == "Player" && !conditionComplete)
                {
                    transform.Rotate(new Vector3(0, 0, 90));
                    conditionComplete = true;
                }
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update () {
        switch (unlockType) {
            case doorUnlockType.KEY:
                if(Physics2D.OverlapBox(door.transform.position, keyUnlockSize, 0, playerMask)){
                    if (conditionComplete)
                    {
                        door.GetComponent<SpriteRenderer>().enabled = false;
                        door.GetComponent<BoxCollider2D>().enabled = false;
                    }
                }
                break;
            case doorUnlockType.BUTTON:
                if (conditionComplete && !open)
                {
                    door.GetComponent<SpriteRenderer>().enabled = false;
                    door.GetComponent<BoxCollider2D>().enabled = false;
                    open = true;
                }
                else if (Time.time > buttonTimer && openTime != 0 && open)
                {
                    conditionComplete = false;
                    transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y * 1.5f);
                    transform.position = new Vector2(transform.position.x, transform.position.y + 0.1f);
                    door.GetComponent<SpriteRenderer>().enabled = true;
                    door.GetComponent<BoxCollider2D>().enabled = true;
                    open = false;
                }
                break;
            case doorUnlockType.LEVER:
                if (conditionComplete)
                {
                    door.GetComponent<SpriteRenderer>().enabled = false;
                    door.GetComponent<BoxCollider2D>().enabled = false;
                }
                break;
            default:
                break;
        }
	}
}
