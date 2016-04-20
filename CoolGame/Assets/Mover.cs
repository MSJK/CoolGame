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
    bool grounded = true;
	// Use this for initialization
	void Start () {
        myTransform = transform;
        myCollider = (BoxCollider)gameObject.GetComponent(typeof(BoxCollider));	
	}
	
	// Update is called once per frame
	void Update () {
        myTransform.position += velocity * Time.deltaTime;
        velocity += acceleration * Time.deltaTime;
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpHeight;
            StartCoroutine(Unground());
        }
	}

    IEnumerator Unground()
    {
        yield return null;
        grounded = false;
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
        myTransform.position = new Vector3(myTransform.position.x, col.transform.position.y+myCollider.size.y/2+col.bounds.extents.y, myTransform.position.z);
        if(velocity.y< 0) velocity.y = 0;
    }
}
