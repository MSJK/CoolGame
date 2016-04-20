using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformTracker : MonoBehaviour {
    HashSet<Platform> pfSet = new HashSet<Platform>();
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
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
        Destroy(pf);
    }
}
