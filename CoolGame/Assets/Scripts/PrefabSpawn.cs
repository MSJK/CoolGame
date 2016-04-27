using UnityEngine;
using System.Collections;

public class PrefabSpawn : MonoBehaviour {

    //bgAsset Vars
    private Transform bgGroup;
    public static float Spawn_Interval;
    public static float H_Speed;

    //Prefab Slot
    public GameObject prefabObj;
    public float depthLevel;

    //ScreenBoundValues
    public static float Bound_Left;
    public static float Bound_Right;
    public static float Bound_Top;
    public static float Bound_Bottom;

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
            instance.transform.position = new Vector3 (Bound_Right, Random.Range(Bound_Top, Bound_Bottom), depthLevel);

            //make spawned object child of the game object this script is attached to
            instance.transform.parent = this.transform;

            //set spawned object's bound for self-culling based on left of screen
            instance.GetComponent<ScreenCull>().cullBound = Bound_Left;

            //set spawned object's speed
            instance.GetComponent<ConstantTransform>().TranX = H_Speed;

            //do nothing until next interval
            yield return new WaitForSeconds(Spawn_Interval);
        }
    }
}
