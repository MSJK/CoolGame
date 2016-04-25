﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformTracker : MonoBehaviour {
    HashSet<Platform> pfSet = new HashSet<Platform>();
    Platform currentPf;
    Vector2 bounds = new Vector2(20, 20);
	// Use this for initialization
	void Start () {
        var firstPlatform = (GameObject)Instantiate(Resources.Load("Platform"));
        Vector3 prefabScale = firstPlatform.transform.localScale;
        firstPlatform.transform.localScale = new Vector3(30, prefabScale.y, prefabScale.z);
        currentPf = (Platform)firstPlatform.GetComponent(typeof(Platform));
	}
	
	// Update is called once per frame
	void Update () {
	    if(currentPf.PlatformEnd<bounds.x)
        {
            float newYDiff = UnityEngine.Random.value * 1.5f -.75f;
            float newXDiff = UnityEngine.Random.value * 3.75f + 1.5f;
            var newPlatform = (GameObject)Instantiate(Resources.Load("Platform"), new Vector3(currentPf.PlatformEnd + newXDiff, currentPf.transform.position.y + newYDiff, 0), Quaternion.identity);
            currentPf = (Platform)newPlatform.GetComponent(typeof(Platform));
        }
        Camera.main.transform.position += new Vector3(0, (currentPf.transform.position.y+2f - Camera.main.transform.position.y)*.2f*Time.deltaTime, 0);
	}

    public void Report(Platform pf)
    {
        pfSet.Add(pf);
    }
    public void Report(Player pl)
    {
        Vector2 playerPosition = pl.MyTransform.position;
        foreach(Platform pf in pfSet)
        {
            float start = pf.PlatformStart;
            float end = pf.PlatformEnd;
            float height = pf.PlatformHeight;
            if (playerPosition.x >= start && playerPosition.x <= end && playerPosition.y >= height)
                pf.IsActive = true;
            else
                pf.IsActive = false;
        }
    }

    public void ReportOffScreen(Platform pf)
    {
        pfSet.Remove(pf);
        Destroy(pf.gameObject);
    }
}