using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalScript : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {       
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}