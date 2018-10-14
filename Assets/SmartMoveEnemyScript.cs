using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SmartMoveEnemyScript : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    float speed;
    [SerializeField]
    float left;
    [SerializeField]
    float right;
    [SerializeField]
    AudioClip crash;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = crash;
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
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GetComponent<AudioSource>().Play();
            Invoke("restart", 0.1f);//this will happen after 2 seconds
        }
    }
    private void restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
