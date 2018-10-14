using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeScript : MonoBehaviour
{
    [SerializeField]
    SpikeTowerScript spikeTow;
    [SerializeField]
    float spikeDuration;
    GameObject mom;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void Start()
    {
        StartCoroutine(StartDestroy());
        spikeTow = FindObjectOfType<SpikeTowerScript>();
    }

    private IEnumerator StartDestroy()
    {
        yield return new WaitForSeconds(spikeDuration);
        spikeTow = gameObject.GetComponentInParent<SpikeTowerScript>();
            spikeTow.StartSpike();
        Destroy(gameObject);
    }
}