using UnityEngine;
using System.Collections;

public class DigitGroup : MonoBehaviour {

    //SpeedVar
    public float digitSpeed = -0.05f;

    //GlitchVar
    public bool isGlitching = false;

    //SpawnVars
    public float spawnInterval = 0.25f;
    public float boundLeft = -12.5f;
    public float boundRight = 12.5f;
    public float boundTop = 15f;
    public float boundBottom = -15f;

    // Use this for initialization
    void Start () {

        //set static prefab spawn vars to editor vars
        PrefabSpawn.Spawn_Interval = spawnInterval;

        PrefabSpawn.Bound_Left = boundLeft;
        PrefabSpawn.Bound_Right = boundRight;
        PrefabSpawn.Bound_Top = boundTop;
        PrefabSpawn.Bound_Bottom = boundBottom;
	}
	
	// Update is called once per frame
	void Update () {

        if (isGlitching)
            DigitSprite.Is_Cycling = true;
        else
            DigitSprite.Is_Cycling = false;
	
	}
}
