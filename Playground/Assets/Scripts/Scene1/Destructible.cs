using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

    public float xForce, yForce;
    public int dropNum, dropID;
    List<GameObject> dropList;
	// Use this for initialization
	void Start () {
		dropList = GameObject.Find("Manager").GetComponent<Lists>().drops;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && collider.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0) DestroyObject(collider.gameObject);
        Debug.Log(collider.gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void DestroyObject(GameObject player)
    {
        GameObject drop = dropList[dropID];
        for (int i = 0; i < dropNum; i++)
        {
            GameObject o = Instantiate(drop, new Vector2(transform.position.x,transform.position.y +0.1f), Quaternion.identity);
            o.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-xForce, xForce), Random.Range(0, yForce)), ForceMode2D.Impulse);
        }
        player.GetComponent<PlayerController>().Bounce(20,Vector2.up, 0f);
        Destroy(transform.parent.gameObject);
    }
}
