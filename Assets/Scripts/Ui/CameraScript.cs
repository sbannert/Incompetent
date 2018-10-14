using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    [SerializeField]
    GameObject player;
    Vector3 offset;

    private void LateUpdate()
    {
        offset = new Vector3(player.transform.position.x, player.transform.position.y+2, player.transform.position.z - 12);
        transform.position = offset;
    }
}
