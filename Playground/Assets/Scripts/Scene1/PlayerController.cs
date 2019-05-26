using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float jumpForce, speed, smoothingTime, startingHealth, damageInvulTime, ladderClimbSpeed;
    [HideInInspector]
    public bool canTakeDamage, isGrounded, doubleJump, isClimbing, canClimb, isSwinging, canSwing;
    [HideInInspector]
    public float ladderX;
    public Transform oLapA, oLapB;
    public LayerMask groundMask;
    public GameObject leftWallCheck, rightWallCheck;
    Rigidbody2D rig;
    float timer, jumpCD, addedSpeed, currentHealth;
    Vector2 targetVelocity;
    Vector2 refVel = Vector2.zero;
    Vector3 mousePos;
    public static int coins;
    GameObject currentWeapon;
    List<GameObject> weaponList;
    bool isOnWall;
    float lockoutTimer;

    private float conveyorReset;
	// Use this for initialization
	void Start () {
        canTakeDamage = true;
        weaponList = GameObject.Find("Manager").GetComponent<Lists>().weapons;
        currentWeapon = weaponList[0];
        coins = 0;
        timer = Time.time;
        jumpCD = 0.2f;
        rig = GetComponent<Rigidbody2D>();
        isGrounded = false;
        doubleJump = false;
        addedSpeed = 0;
        conveyorReset = 0;
        mousePos = Vector3.zero;
        currentHealth = startingHealth;
        isOnWall = false;
        lockoutTimer = Time.time;
        isSwinging = false;
        isClimbing = false;
	}

    void Update()
    {
        Shoot();
        Debug.Log(canClimb);
    }

    // Update is called once per frame
    void FixedUpdate () {
        if(lockoutTimer <= Time.time)
        BasicMove();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.gameObject.layer) {
            case 8:
                SpecialFloorEnter(collider.gameObject);
                break;
            case 9:
                PickupDetect(collider.gameObject);
                break;
            default:
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        switch (collider.gameObject.layer)
        {
            case 8:
                SpecialFloorExit(collider.gameObject);
                break;
            default:
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 8:
                rig.velocity = new Vector2(0, rig.velocity.y);
                if(conveyorReset != 0) {
                    addedSpeed -= conveyorReset;
                    conveyorReset = 0;
                }
                break;
            default:
                break;
        }
    }

    void Die()
    {
        Debug.Log("DED");
    }
    void BasicMove()
    {
        if (isGrounded != Physics2D.OverlapArea(oLapA.position, oLapB.position, groundMask)){
            isGrounded = Physics2D.OverlapArea(oLapA.position, oLapB.position, groundMask);
            doubleJump = true;
        }
        if (leftWallCheck.GetComponent<WallJump>().isOnWall || rightWallCheck.GetComponent<WallJump>().isOnWall) isOnWall = true;
        else isOnWall = false;
        if (!canClimb)
        {
            isClimbing = false;
            if (Input.GetKey(KeyCode.W) && isGrounded && timer < Time.time)
            {
                Bounce(jumpForce, Vector2.up, 0f);
            }
            else if (Input.GetKeyDown(KeyCode.W) && doubleJump && !isOnWall)
            {
                Bounce(jumpForce, Vector2.up, 0f);
                doubleJump = false;
            }
        }
        else if(canClimb)
        {
            if (Input.GetKey(KeyCode.W) && isClimbing)
            {
                rig.velocity = new Vector2(0,ladderClimbSpeed);
            }
            if (Input.GetKey(KeyCode.S) && isClimbing)
            {
                if (isGrounded)
                {
                    isClimbing = false;
                    rig.gravityScale = 1;
                }
                else rig.velocity = new Vector2(0, -ladderClimbSpeed);
            }
            if (Input.GetKeyUp(KeyCode.W) && isClimbing) rig.velocity = new Vector2(0, 0);
            if (Input.GetKeyUp(KeyCode.S) && isClimbing) rig.velocity = new Vector2(0, 0);
            if (Input.GetKeyDown(KeyCode.W) && !isClimbing)
            {
                isClimbing = true;
                transform.position = new Vector2(ladderX, transform.position.y + 0.125f);
                rig.velocity = new Vector2(0, 0);
                rig.gravityScale = 0;
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && isClimbing)
            {
                isClimbing = false;
                rig.gravityScale = 1;
            }
        }
        else if (canSwing)
        {
            if (Input.GetKey(KeyCode.W) && !isSwinging)
            {
                isSwinging = true;
                rig.velocity = new Vector2(0, 0);
                rig.gravityScale = 0;
            }
        }
        float h = Input.GetAxisRaw("Horizontal");
        if (conveyorReset != 0 && h != 0 && h != Mathf.Sign(conveyorReset))
        {
            addedSpeed -= conveyorReset;
            conveyorReset = 0;
        }
        if (!isClimbing && !isSwinging)
        {
            targetVelocity = new Vector2((speed * h) + addedSpeed, rig.velocity.y);
            rig.velocity = Vector2.SmoothDamp(rig.velocity, targetVelocity, ref refVel, smoothingTime, 1000, Time.fixedDeltaTime);
        }

    }
    void PickupDetect(GameObject collObj)
    {
        string tag = collObj.tag;
        switch (tag)
        {
            case "Coin":
                coins++;
                Destroy(collObj);
                break;

            default:
                break;
        }
    }
    void SpecialFloorEnter(GameObject collObj)
    {
        switch (collObj.tag)
        {
            case "SpecialFloor":
                SpecialPad padInst = collObj.GetComponent<SpecialPad>();
                switch (padInst.padType)
                {
                    case SpecialPad.Pad.Conveyor:
                        addedSpeed += padInst.conveyorSpeed;
                        break;
                    case SpecialPad.Pad.Gravity:
                        if (padInst.negativeGravity && Mathf.Sign(rig.gravityScale) > 0)
                        {
                            isGrounded = true;
                            rig.gravityScale = 0 - rig.gravityScale;
                            oLapA.localPosition = new Vector2(oLapA.localPosition.x, 0 - oLapA.localPosition.y);
                            oLapB.localPosition = new Vector2(oLapB.localPosition.x, 0 - oLapB.localPosition.y);
                        }
                        else if (!padInst.negativeGravity && Mathf.Sign(rig.gravityScale) < 0)
                        {
                            isGrounded = true;
                            rig.gravityScale = 0 - rig.gravityScale;
                            oLapA.localPosition = new Vector2(oLapA.localPosition.x, 0 - oLapA.localPosition.y);
                            oLapB.localPosition = new Vector2(oLapB.localPosition.x, 0 - oLapB.localPosition.y);
                        }
                        break;
                    case SpecialPad.Pad.Bounce:
                        Bounce(padInst.bounceForce, Vector2.up, 0f);
                        break;
                    case SpecialPad.Pad.Speed:
                        speed *= padInst.speedMultiplier;
                        break;
                    default:
                        break;
                }
                break;
        }
    }
    void SpecialFloorExit(GameObject collObj)
    {
        switch (collObj.tag)
        {
            case "SpecialFloor":
                SpecialPad padInst = collObj.GetComponent<SpecialPad>();
                switch (padInst.padType)
                {
                    case SpecialPad.Pad.Conveyor:
                        if (timer > Time.time)
                        {
                            conveyorReset += addedSpeed;
                        }
                        else addedSpeed -= padInst.conveyorSpeed;
                        break;
                    case SpecialPad.Pad.Speed:
                        speed /= padInst.speedMultiplier;
                        break;
                    default:
                        break;
                }
                break;
        }
    }
    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Input.mousePosition;
            mousePos.z = 20;
            currentWeapon.GetComponent<AttackScript>().Fire(Camera.main.ScreenToWorldPoint(mousePos));
        }
    }

    private IEnumerator InvulReset()
    {
        yield return new WaitForSeconds(damageInvulTime);
        canTakeDamage = true;
    }

    public void Bounce(float force, Vector2 direction, float lockout)
    {
        rig.velocity = new Vector2(rig.velocity.x, 0);
        rig.AddForce(new Vector2(force * direction.x, force * direction.y * Mathf.Sign(rig.gravityScale)), ForceMode2D.Impulse);
        timer = Time.time + jumpCD;
        lockoutTimer = Time.time + lockout;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        canTakeDamage = false;
        StartCoroutine(InvulReset());
        if (currentHealth <= 0) Die();
    }
}
