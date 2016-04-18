using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    Transform myTransform;
    Mover myMover;
	// Use this for initialization
	void Start () {
        myTransform = transform;
        myMover = (Mover)gameObject.GetComponent(typeof(Mover));
	}
	
	// Update is called once per frame
	void Update () {
        myMover.SetAcceleration(Vector3.down*20);

        Camera.main.transform.position = new Vector3(myTransform.position.x, 0, myTransform.position.z - 2);
	}

    void OnTriggerEnter(Collider col)
    {
        myMover.Collide(col);
    }
    void OnTriggerStay(Collider col)
    {
        myMover.Collide(col);
    }
}
