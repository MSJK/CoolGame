using UnityEngine;
using System.Collections;
using System;

public class CameraScript : MonoBehaviour {
    Player pl;
    Transform myTransform;
    public Vector3 target;
    Vector3 currentNonGlitchPosition;
    Vector3 glitchOffset;
    bool glitching = false;
    DateTime glitchStart;
    [SerializeField]
    float glitchDuration = 500;
    [SerializeField]
    float lerpSpeed = .01f;
    [SerializeField]
    float glitchLerpSpeed = .5f;
    [SerializeField]
    float glitchDistance = 1;
    [SerializeField]
    float resetLerpSpeed = 1;
	// Use this for initialization
	void Start () {
        pl = (Player)(GameObject.Find("Player").GetComponent(typeof(Player)));
        myTransform = transform;
        currentNonGlitchPosition = transform.position;
        glitchOffset = Vector3.zero;
        ((GameManager)(GameObject.Find("GameManager").GetComponent(typeof(GameManager)))).ItemBought += (id) =>
        {
            if (id == "screen-shake")
            {
                StartCoroutine(CameraShake());
            }
        };
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C))
            StartCoroutine(CameraShake());
        if (glitching && (DateTime.Now - glitchStart).Milliseconds > glitchDuration)
            glitching = false;
        currentNonGlitchPosition = Vector3.Lerp(myTransform.position, target, lerpSpeed*Time.deltaTime);
        currentNonGlitchPosition.x = pl.MyTransform.position.x;
        Debug.DrawRay(currentNonGlitchPosition, Vector3.up, Color.red, Time.deltaTime);
        myTransform.position = Vector3.Lerp(myTransform.position, (glitching) ? (currentNonGlitchPosition + glitchOffset) : currentNonGlitchPosition, (glitching) ? (glitchLerpSpeed * Time.deltaTime) : (resetLerpSpeed * Time.deltaTime));
	}

    IEnumerator CameraShake()
    {
        glitching = true;
        glitchStart = DateTime.Now;
        glitchOffset = (UnityEngine.Random.insideUnitCircle * glitchDistance);
        while (glitching)
        {
            yield return null;
            if (myTransform.position == currentNonGlitchPosition + glitchOffset)
                glitchOffset = (UnityEngine.Random.insideUnitCircle * glitchDistance);
        }
    }
}
