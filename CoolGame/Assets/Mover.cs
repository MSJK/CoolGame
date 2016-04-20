using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {
    Vector3 acceleration;
    Vector3 velocity;
    Transform myTransform;
    [SerializeField]
    float maxMoveSpeed;
    [SerializeField]
    float jumpHeight;
    BoxCollider myCollider;

	// Use this for initialization
	void Start () {
        myTransform = transform;
        myCollider = (BoxCollider)gameObject.GetComponent(typeof(BoxCollider));	
	}
	
	// Update is called once per frame
	void Update () {
        myTransform.position += velocity * Time.deltaTime;
        velocity += acceleration * Time.deltaTime;
        /*if (myTransform.position.y < 0)
        {
            myTransform.position = new Vector3(myTransform.position.x, 0, myTransform.position.z);
            velocity.y = 0;
        }*/
        if (Input.GetKeyDown(KeyCode.Space))
            velocity.y = jumpHeight;
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
        myTransform.position = new Vector3(myTransform.position.x, col.transform.position.y+myCollider.size.y/2+col.bounds.extents.y, myTransform.position.z);
        if(velocity.y< 0) velocity.y = 0;
        //acceleration.y = 0;
    }
}
