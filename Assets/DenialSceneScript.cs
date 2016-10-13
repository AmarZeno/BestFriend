using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DenialSceneScript : MonoBehaviour {

    public GameObject Player;
    public GameObject Camera;
    public GameObject RotatePoint;

    private UnitPlayer unitPlayer;
    private CameraController cameraController;

    float currTime;


    void Awake()
    {
        unitPlayer = Player.GetComponent<UnitPlayer>();
        cameraController = Camera.GetComponent<CameraController>();

    }

    // Use this for initialization
    void Start () {

        unitPlayer.enabled = false;
        cameraController.enabled = false;
        currTime = Time.realtimeSinceStartup;

    }
	
	// Update is called once per frame
	void Update () {

        if(Time.realtimeSinceStartup > currTime + 9 && Time.realtimeSinceStartup < currTime + 11)
        {
            Camera.transform.Rotate(Vector3.up * Time.deltaTime*-35);
        }

        if (Time.realtimeSinceStartup > currTime + 13 && Camera.transform.rotation.x > 0)
        {
            Camera.transform.RotateAround(new Vector3(-5,1.5f,0), Vector3.right, -45 * Time.deltaTime);
        }
        if (Time.realtimeSinceStartup > currTime + 14 && Camera.transform.position.x < -2.2)
        {
            Camera.transform.Translate(Vector3.forward * Time.deltaTime);
        }

        if (Time.realtimeSinceStartup > currTime + 18)
        {
            unitPlayer.enabled = true;
            cameraController.enabled = true;
        }

    }
}
