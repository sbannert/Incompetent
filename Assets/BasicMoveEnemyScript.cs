using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicMoveEnemyScript : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    float speed;
    [SerializeField]
    string obstacle1;
    [SerializeField]
    string obstacle2;
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
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == obstacle1)
        {
            speed = speed * -1;
        }
        if (collision.gameObject.name == obstacle2)
        {
            speed = speed * -1;
        }
        if (collision.gameObject.name == "Player")
        {
            GetComponent<AudioSource>().Play();
            Invoke("restart", 0.1f);//this will happen after 2 seconds
        }
    }
    private void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
