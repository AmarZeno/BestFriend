using UnityEngine;
using System.Collections;

public class BallHit : MonoBehaviour {

    public float forceApplied = 50;

    void OnTriggerEnter(Collider col)
    {
        // Look to see if the player collided with the ball
        if (col.gameObject.name == "Player")
        {
            Debug.Log("Oops");
            // if yes then do your kick/move the ball however you want
            Debug.Log("ouch you kicked me!");
            var vec = new Vector3(10, 10, 0); //x: float, y: float, z: float)
         //   this.gameObject.rigidbody.addf .rigidbody.AddForce(vec); // , Impluse);


          //  this.GetComponent<Rigidbody>().AddForce(vec);
        }
    }
}
