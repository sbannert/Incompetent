using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedBlockSpawnerScript : MonoBehaviour {

    [SerializeField]
    float spikeTimer;
    [SerializeField]
    GameObject spike;
    [SerializeField]
    float spikeHeight;
    [SerializeField]
    float spikeYChange;
    Vector3 vectLocation;

    // Update is called once per frame
    void Start()
    {
        vectLocation = new Vector3(transform.position.x, transform.position.y + spikeYChange, transform.position.z);
        StartCoroutine(DelayTime());
    }

    private IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(spikeTimer);
        GameObject spikeObj = (GameObject)Instantiate(spike, vectLocation, transform.rotation);
        spikeObj.transform.localScale = new Vector3(0.25f, spikeHeight, 0.25f);
        spikeObj.transform.parent = transform;
    }

    public void StartSpike()
    {
        StartCoroutine(DelayTime());
    }
}
