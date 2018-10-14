using UnityEngine;

public class GroundScript: MonoBehaviour {

    [SerializeField]
    Transform player;

    public void FixedUpdate()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Temp Player")
        {
            
        }
    }

}
