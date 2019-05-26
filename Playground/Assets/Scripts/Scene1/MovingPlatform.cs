using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public LayerMask affectedMask;
    Vector2 startPoint;
    public float distance, speed, smoothTime, endWaitTime;
    bool goingRight = true;
    List<GameObject> currentObjects = new List<GameObject>();
    Vector2 endPoint;
    Vector3 refPoint = Vector3.zero;
    float currentSpeed;
    float targetSpeed;
    GameObject parent;

    private void Start()
    {
        parent = transform.parent.gameObject;
        startPoint = parent.transform.position;
        targetSpeed = speed;
        endPoint = new Vector2(startPoint.x + distance, startPoint.y);
        currentSpeed = 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(affectedMask == (affectedMask | (1 << collision.gameObject.layer))){
            currentObjects.Remove(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (affectedMask == (affectedMask | (1 << collision.gameObject.layer))){
            currentObjects.Add(collision.gameObject);
        }
    }

        private void FixedUpdate()
        {
        parent.transform.Translate(Vector3.SmoothDamp(new Vector3(currentSpeed, 0, 0), new Vector3(targetSpeed, 0, 0), ref refPoint, smoothTime) * Time.deltaTime * 100);
        if (currentObjects.Count > 0)
        {
            foreach (GameObject go in currentObjects)
            {
                go.transform.Translate(Vector3.SmoothDamp(new Vector3(currentSpeed, 0, 0), new Vector3(targetSpeed, 0, 0), ref refPoint, smoothTime) * Time.deltaTime * 100);
            }
        }
        if (goingRight && parent.transform.position.x >= startPoint.x + distance) {
            goingRight = false;
            targetSpeed = 0;
            StartCoroutine(waitAtEnd());
        }
        if (!goingRight && parent.transform.position.x < startPoint.x)
        {
            goingRight = true;
            targetSpeed = 0;
            StartCoroutine(waitAtEnd());
        }
    }
    private IEnumerator waitAtEnd()
    {
        yield return new WaitForSeconds(endWaitTime);
        if (goingRight) targetSpeed = speed;
        if (!goingRight) targetSpeed = -speed;
    }
}
