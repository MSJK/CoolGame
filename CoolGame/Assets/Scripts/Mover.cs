using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {
    Vector3 acceleration;
    Vector3 velocity;
    Transform myTransform;
    [SerializeField]
    float maxMoveSpeed;
    BoxCollider myCollider;
    [SerializeField]
    float jumpAcceleration = 15;
    [SerializeField]
    float jumpVelocityLimit = 10;
    [SerializeField]
    float jumpHeight;
    bool jumping = false;
    bool grounded = true;
    public bool Grounded
    {
        get { return grounded; }
    }
	// Use this for initialization
	void Start () {
        myTransform = transform;
        myCollider = (BoxCollider)gameObject.GetComponent(typeof(BoxCollider));	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 activeAccel = acceleration;
        /*if (jumping && velocity.sqrMagnitude < jumpVelocityLimit * jumpVelocityLimit)
            activeAccel = new Vector3(0, jumpAcceleration, 0);*/
        if (!jumping && velocity.y > 0)
            activeAccel = new Vector3(0, -jumpAcceleration, 0);
        /*else
        {
            jumping = false;
        }*/

        myTransform.position += velocity * Time.deltaTime;
        velocity += activeAccel * Time.deltaTime;
	}

    public IEnumerator Unground()
    {
        grounded = false;
        yield return null;
        grounded = false;
    }

    public void Jump()
    {
        if (!grounded)
            return;
        //jumpCharge = Mathf.Clamp(jumpCharge, 0, 1);
        velocity.y += jumpHeight;
        jumping = true;
        StartCoroutine(Unground());
        //jumpCharge = 0;
    }

    public void StopJump()
    {
        jumping = false;
    }

    public void SetVelocity(Vector3 direction, float percent)
    {
        velocity = direction.normalized * percent * maxMoveSpeed;
    }

    public void SetAcceleration(Vector3 accel)
    {
        acceleration = accel;
    }

    public void Collide(Collider col)
    {
        grounded = true;
        jumping = false;
        myTransform.position = new Vector3(myTransform.position.x, col.transform.position.y+myCollider.size.y/2+col.bounds.extents.y, myTransform.position.z);
        if(velocity.y< 0) velocity.y = 0;
    }
}
