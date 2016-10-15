using UnityEngine;
using System.Collections;

public class UnitPlayer : Unit {

    public Rigidbody ball;

    public float pushPower = 20.0F;

    // Use this for initialization
    public override void Start () {
        base.Start();
	}

    // Update is called once per frame
    public override void Update () {

        // Rotation

        transform.Rotate(0f, Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime, 0f);

        move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        move.Normalize();

        move = transform.TransformDirection(move);

        //if (Input.GetKeyDown(KeyCode.Space)) {
        //    jump = true;
        //}

      //  running = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        base.Update();
	}

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.name == "Ball")
        {
            Debug.Log("Hit Ball");
            Rigidbody body = hit.collider.attachedRigidbody;
            if (body == null || body.isKinematic)
                return;

            //if (hit.moveDirection.y < -0.3F)
            //    return;

             Vector3 pushDir = new Vector3(this.move.x, this.move.y, this.move.z);
            ball.AddForce(pushDir * pushPower);
        }
    }
}
