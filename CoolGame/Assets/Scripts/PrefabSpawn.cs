using UnityEngine;
using System.Collections;

public class PrefabSpawn : MonoBehaviour {

    //bgAssets Object
    private Transform bgGroup;

    //Prefab Slot
    public GameObject prefabObj;
    public float spawnInterval;
    public float depthLevel;

    //ScreenBoundValues
    public float boundLeft;
    public float boundRight;
    public float boundTop;
    public float boundBottom;

	// Use this for initialization
	void Start () {

        //set background group variable to this object's parent
        bgGroup = this.transform.parent;

        //begin coroutine for spawning prefabs on interval
        StartCoroutine(InstancePrefab());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Coroutine for instancing prefab on interval
    IEnumerator InstancePrefab()
    {
        for(;;)
        {
            //the object to be instanced
            GameObject instance = Instantiate(prefabObj);

            //set object's starting position to be right of screen at random height
            instance.transform.position = new Vector3 (boundRight, Random.Range(boundTop, boundBottom), depthLevel);

            //make spawned object child of the game object this script is attached to
            instance.transform.parent = this.transform;

            //set spawned object's bound for self-culling based on left of screen
            instance.GetComponent<ScreenCull>().cullBound = boundLeft;

            //do nothing until next interval
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
