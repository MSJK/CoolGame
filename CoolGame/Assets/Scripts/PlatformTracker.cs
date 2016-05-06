using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformTracker : MonoBehaviour {
    CameraScript cs;
    HashSet<Platform> pfSet = new HashSet<Platform>();
    Platform currentPf;
    [SerializeField]
    float platformSeparationXMin;
    [SerializeField]
    float platformSeparationXMax;
    [SerializeField]
    float platformSeparationYMin;
    [SerializeField]
    float platformSeparationYMax;
    [SerializeField]
    float platformSizeMin;
    [SerializeField]
    float platformSizeMax;
    Vector2 bounds = new Vector2(20, 20);
	// Use this for initialization
	void Start () {
        cs = (CameraScript)Camera.main.GetComponent(typeof(CameraScript));
        var firstPlatform = (GameObject)Instantiate(Resources.Load("Platform"));
        Vector3 prefabScale = firstPlatform.transform.localScale;
        firstPlatform.transform.localScale = new Vector3(30, prefabScale.y, prefabScale.z);
        currentPf = (Platform)firstPlatform.GetComponent(typeof(Platform));
	}
	
	// Update is called once per frame
	void Update () {
	    if(currentPf.PlatformEnd<bounds.x)
        {
            float newYDiff = UnityEngine.Random.value * (platformSeparationYMax - platformSeparationYMin) + platformSeparationYMin;
            float newXDiff = UnityEngine.Random.value * (platformSeparationXMax - platformSeparationXMin) + platformSeparationXMin;
            float newLength = UnityEngine.Random.value * (platformSizeMax - platformSizeMin) + platformSizeMin;
            var newPlatform = (GameObject)Instantiate(Resources.Load("Platform"), new Vector3(currentPf.PlatformEnd + newXDiff, currentPf.transform.position.y + newYDiff, 0), Quaternion.identity);
            currentPf = (Platform)newPlatform.GetComponent(typeof(Platform));
            var baseScale = currentPf.transform.localScale;
            currentPf.transform.localScale = new Vector3(newLength, baseScale.y, baseScale.z);
            currentPf.transform.position += new Vector3(currentPf.PlatformLength / 2, 0, 0);
            Currency newCurrency = (Currency)((GameObject)Instantiate(Resources.Load("Collectible"), currentPf.transform.position, Quaternion.identity)).GetComponent(typeof(Currency));
            newCurrency.AssignPlatform(currentPf);
        }
        //Camera.main.transform.position += new Vector3(0, (currentPf.transform.position.y+2f - Camera.main.transform.position.y)*.2f*Time.deltaTime, 0);
        cs.target = new Vector3(Camera.main.transform.position.x, currentPf.transform.position.y + 2f, Camera.main.transform.position.z);
	}

    public void Report(Platform pf)
    {
        pfSet.Add(pf);
    }
    public void Report(Player pl)
    {
        Vector2 playerPosition = pl.MyTransform.position;
        bool haveActivePf = false;
        foreach(Platform pf in pfSet)
        {
            float start = pf.PlatformStart;
            float end = pf.PlatformEnd;
            float height = pf.PlatformHeight;
            if (playerPosition.x > start && playerPosition.x < end && playerPosition.y > height)
            {
                pf.IsActive = true;
                haveActivePf = true;
            }
            else
            {
                pf.IsActive = false;
            }           
        }
        if (!haveActivePf)
            StartCoroutine(((Mover)pl.gameObject.GetComponent(typeof(Mover))).Unground());
    }

    public void ReportOffScreen(Platform pf)
    {
        pfSet.Remove(pf);
        Destroy(pf.gameObject);
    }
}
