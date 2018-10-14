using UnityEngine;
using UnityEngine.SceneManagement;

public class HazardScript : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Temp Player")
        {
            SceneManager.LoadScene("WTTestScene");
        }
    }
}
