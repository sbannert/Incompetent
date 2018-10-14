using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShockWaveScript : MonoBehaviour {

    [SerializeField]
    float speed;
    [SerializeField]
    string obstacle;

    // Update is called once per frame
    void Update () {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(collision.gameObject.tag == "Floor")
        {
            Debug.Log("collided with obstacle");
            Destroy(gameObject);
        }
    }
}
