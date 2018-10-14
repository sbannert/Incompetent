using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomingEnemyScript : MonoBehaviour {
    //[SerializeField]
    Transform player;
    GameObject temp;
    [SerializeField]
    float speed;
    [SerializeField]
    float moveDelay;
    bool run;


    // Use this for initialization
    void Awake () {
        temp = GameObject.FindWithTag("Player");
        player = temp.transform;
        StartCoroutine(DelayTime());
        transform.LookAt(player);
        run = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (run)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    private IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(moveDelay);
        run = true;        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        } else
        {
            Destroy(gameObject);
        }        
    }
}
