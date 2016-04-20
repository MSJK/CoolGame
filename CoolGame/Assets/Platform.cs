using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {
    Mover myMover;
	// Use this for initialization
	void Start () {
        myMover = (Mover)gameObject.GetComponent(typeof(Mover));
	}
	
	// Update is called once per frame
	void Update () {
        myMover.SetVelocity(Vector3.left, 1.0f);
	}
}
