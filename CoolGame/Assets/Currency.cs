using UnityEngine;
using System.Collections;

public class Currency : MonoBehaviour {
    Player pl;
    Platform pf = null;
    GameManager gm;
    Transform myTransform;
    [SerializeField]
    float collisionRadius;
    [SerializeField]
    int points;
	// Use this for initialization
	void Start () {
        pl = ((Player)GameObject.Find("Player").GetComponent(typeof(Player)));
        gm = ((GameManager)GameObject.Find("GameManager").GetComponent(typeof(GameManager)));
        myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (pf != null && pf.IsActive && (myTransform.position - pl.MyTransform.position).sqrMagnitude < collisionRadius * collisionRadius)
            Pickup();
        if (pf != null) myTransform.position = pf.transform.position + new Vector3(0, collisionRadius, 0);
	}

    public void AssignPlatform(Platform _pf)
    {
        if (pf == null)
            pf = _pf;
    }

    void Pickup()
    {
        gm.GivePoints(points);
        Destroy(gameObject);
    }
}
