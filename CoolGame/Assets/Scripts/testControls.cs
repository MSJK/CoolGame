using UnityEngine;
using System.Collections;

public class testControls : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Fire1"))
            { this.GetComponent<Animator>().SetTrigger("JumpStart"); }

        if (Input.GetButtonUp("Fire1"))
        { this.GetComponent<Animator>().SetTrigger("FallStart"); }

    }
}
