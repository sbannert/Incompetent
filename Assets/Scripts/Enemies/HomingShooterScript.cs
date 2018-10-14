using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingShooterScript : MonoBehaviour {
    [SerializeField]
    float shootDelay;
    [SerializeField]
    GameObject homing;
    Vector3 vectLocation;

    // Update is called once per frame
    void Start()
    {
        vectLocation = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        StartCoroutine(DelayTime());
    }

    private IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(shootDelay);
        GameObject homingObj = (GameObject)Instantiate(homing, vectLocation, transform.rotation);
        //spikeObj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        homingObj.transform.parent = transform;
        Start();
    }

    /*public void StartHoming()
    {
        StartCoroutine(DelayTime());
    }*/
}
