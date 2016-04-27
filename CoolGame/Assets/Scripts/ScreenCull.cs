using UnityEngine;
using System.Collections;

public class ScreenCull : MonoBehaviour {

    //Parent Vars
    private PrefabSpawn parentScript;

    //Cull Vars
    public float cullBound = -12f;

	// Use this for initialization
	void Start () {

        StartCoroutine(CheckLocation());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator CheckLocation()
    {
        for (;;)
        {
            if(this.transform.position.x <= cullBound)
                Destroy(this.gameObject);

            yield return new WaitForSeconds(0.5f);
        }
    }
}
