using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        Vector3 vect = new Vector3(1, 0, 0);
        rb.MovePosition(transform.position + vect * Time.deltaTime);
    }
	
	// Update is called once per frame
	void Update () {
        //rb.MovePosition(transform.position + transform.up * Time.deltaTime);
	}
}
