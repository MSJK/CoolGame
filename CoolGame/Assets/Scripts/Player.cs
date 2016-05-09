using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    Transform myTransform;
    PlatformTracker myPT;

    public Transform MyTransform
    {
        get { return myTransform; }
    }
    Mover myMover;
	// Use this for initialization
	void Start () {
        myTransform = transform;
        myMover = (Mover)gameObject.GetComponent(typeof(Mover));
        myPT = (PlatformTracker)gameObject.GetComponent(typeof(PlatformTracker));
	}
	
	// Update is called once per frame
	void Update () {
        myPT.Report(this);
        myMover.SetAcceleration(Vector3.down*20);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.GetComponentsInChildren<Animator>()[0].SetTrigger("JumpStart");
            myMover.Jump();            
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            this.GetComponentsInChildren<Animator>()[0].SetTrigger("FallStart");
            myMover.StopJump();            
        }
        //Camera.main.transform.position = new Vector3(myTransform.position.x, 0, myTransform.position.z - 5);

        var camY = Camera.main.transform.position.y;
        if (transform.position.y < camY - 10)
        {
            // Game Over
#if UNITY_5_3_OR_NEWER
            SceneManagement.SceneManager.LoadScene("GameOver");
#else
            Application.LoadLevel("GameOver");
#endif
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(((Platform)col.gameObject.GetComponent(typeof(Platform))).IsActive)
            myMover.Collide(col);
    }
    void OnTriggerStay(Collider col)
    {
        if (((Platform)col.gameObject.GetComponent(typeof(Platform))).IsActive)
            myMover.Collide(col);
    }
}
