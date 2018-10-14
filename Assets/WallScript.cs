using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Shockwave")
        {
            Destroy(collision.gameObject);
        }
    }
}
