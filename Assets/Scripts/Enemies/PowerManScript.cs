using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PowerManScript : MonoBehaviour {

    [SerializeField]
    float waveTimer;
    [SerializeField]
    GameObject wave;

    Vector3 vectLocation;
    int count;

    // Update is called once per frame
    void Start()
    {
        vectLocation = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        StartCoroutine(DelayTime());
    }

    private IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(waveTimer);
        GameObject spikeObj = (GameObject)Instantiate(wave, vectLocation, transform.rotation);
        //spikeObj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        spikeObj.transform.parent = transform;
        Start();
    }

    public void StartWave()
    {
        StartCoroutine(DelayTime());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
