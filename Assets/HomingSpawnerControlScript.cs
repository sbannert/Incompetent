using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingSpawnerControlScript : MonoBehaviour {

    [SerializeField]
    float spawnDelay;
    [SerializeField]
    GameObject homing;
    Vector3 vectLocation;
    bool hasSpawned;

    void Start()
    {
        vectLocation = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        hasSpawned = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.name == "Player") && (!hasSpawned))
        {
            GameObject homingObj = (GameObject)Instantiate(homing, vectLocation, transform.rotation);
            //spikeObj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            homingObj.transform.parent = transform;
            hasSpawned = true;
        }
    }
}
