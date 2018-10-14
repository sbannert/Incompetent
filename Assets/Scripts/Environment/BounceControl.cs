using UnityEngine;

public class BounceControl : MonoBehaviour {

    [SerializeField]
    Transform playerObj;
    [SerializeField]
    Rigidbody rb;

	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Temp Player")
        {
            Debug.Log("collision true");
            rb.AddForce(0, 500, 0);
        }
    }

}
