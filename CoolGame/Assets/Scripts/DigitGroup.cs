using UnityEngine;
using System.Collections;

public class DigitGroup : MonoBehaviour {

    //GlitchVar
    public bool isGlitching = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (isGlitching)
            DigitSprite.Is_Cycling = true;
        else
            DigitSprite.Is_Cycling = false;
	
	}
}
