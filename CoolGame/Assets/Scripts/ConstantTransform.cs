using UnityEngine;
using System.Collections;

public class ConstantTransform : MonoBehaviour {

    public float TranX;
    public float TranY;
    public float TranZ;

    public float RotX;
    public float RotY;
    public float RotZ;
	
	// Update is called once per frame
	void Update () {

        this.transform.localPosition += new Vector3(TranX, TranY, TranZ);
        this.transform.Rotate(RotX * Time.deltaTime, RotY * Time.deltaTime, RotZ * Time.deltaTime);
	
	}
}
