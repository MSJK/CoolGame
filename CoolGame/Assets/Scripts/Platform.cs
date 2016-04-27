using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {
    Mover myMover;
    PlatformTracker myPT;
    Transform myTransform;
    bool isActive = false;
    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }
    public float PlatformStart
    {
        get
        {
            return myTransform.position.x-myTransform.localScale.x/2;
        }
    }
    public float PlatformEnd
    {
        get
        {
            return myTransform.position.x + myTransform.localScale.x/2;
        }
    }
    public float PlatformHeight
    {
        get
        {
            return myTransform.position.y;
        }
    }

	// Use this for initialization
	void Start () {
        myTransform = transform;
        myPT = (PlatformTracker)GameObject.Find("Player").GetComponent(typeof(PlatformTracker));
        myMover = (Mover)gameObject.GetComponent(typeof(Mover));
        myPT.Report(this);
	}
	
	// Update is called once per frame
	void Update () {
        myMover.SetVelocity(Vector3.left, 1.0f);
        if (myTransform.position.x < -100)
            myPT.ReportOffScreen(this);
	}
    
    /*void OnBecameInvisible()
    {
        myPT.ReportOffScreen(this);
    }*/
}
