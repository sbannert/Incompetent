using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdScript : MonoBehaviour {

    private Rigidbody rb;
    private float holdSpeed;
    [SerializeField]
    float speed;
    [SerializeField]
    float left;
    [SerializeField]
    float right;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        holdSpeed = speed;
    }

    private void FixedUpdate()
    {
        Vector3 vect = new Vector3(speed, 0, 0);
        rb.MovePosition(transform.position + vect * Time.deltaTime);
        /*if (rb.position.x < 19) {
            speed = -2;
        }*/
        /*if (rb.position.x > 13)
        {
            speed = 2;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(rb.position.x);
        if (rb.position.x >= right)
        {
            transform.position = new Vector3(right - 0.1f, rb.position.y, rb.position.z);
            speed = speed * -1;
        }
        if (rb.position.x <= left)
        {
            transform.position = new Vector3(left + 0.1f, rb.position.y, rb.position.z);
            speed = speed * -1;
        }

        if (holdSpeed != speed) {
            transform.Rotate(Vector3.up * 180);
            holdSpeed = speed;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //Debug.Log("collision");
        }
    }
}
