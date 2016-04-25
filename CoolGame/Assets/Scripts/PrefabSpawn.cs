using UnityEngine;
using System.Collections;

public class PrefabSpawn : MonoBehaviour {

    //bgAssets Object
    private GameObject bgGroup;

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
        bgGroup = this.transform.parent.gameObject;

        StartCoroutine(InstancePrefab());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator InstancePrefab()
    {
        for(;;)
        {
            GameObject instance = Instantiate(prefabObj);

            instance.transform.position = new Vector3 (boundRight, Random.Range(boundTop, boundBottom), depthLevel);
            instance.transform.parent = this.transform;
            instance.GetComponent<ScreenCull>().cullBound = boundLeft;
            if(bgGroup.GetComponent<DigitGroup>().isGlitching)
            {
                instance.GetComponent<Animator>().enabled = true;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
