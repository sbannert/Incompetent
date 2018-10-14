using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWaveBehavior : MonoBehaviour
{

    private Rigidbody rb;
    bool didFire;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        didFire = false;
    }


    // Update is called once per frame
    void Update()
    {
        //Vector3 vect = new Vector3(10, 0, 0);
        //rb.MovePosition(transform.position + vect * Time.deltaTime);
        if (didFire == false) {
            StartCoroutine(TimeDelay());
        }

    }

    private IEnumerator TimeDelay()
    {
        yield return new WaitForSeconds(5);
        Vector3 vect = new Vector3(50, 0, 0);
        rb.MovePosition(transform.position + vect * Time.deltaTime);
        didFire = true;
        //Vector3 vect = new Vector3(-10, 0, 0);
        //rb.MovePosition(transform.position + vect * Time.deltaTime);
        //yield return new WaitForSeconds(10);
        //yield return new WaitForSeconds(5);
        //Destroy(rb.GetComponent<Rigidbody>());
    }
}
