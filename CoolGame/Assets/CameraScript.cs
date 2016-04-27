using UnityEngine;
using System.Collections;
using System;

public class CameraScript : MonoBehaviour {
    Player pl;
    Transform myTransform;
    public Vector3 target;
    Vector3 currentNonGlitchPosition;
    Vector3 glitchOffset;
    bool shaking = false;
    DateTime shakeStart;
    [SerializeField]
    float shakeDuration = 500;
    [SerializeField]
    float lerpSpeed = .01f;
    [SerializeField]
    float shakeLerpSpeed = .5f;
    [SerializeField]
    float shakeDistance = 1;
    [SerializeField]
    float resetLerpSpeed = 1;
    bool flickering = false;
    [SerializeField]
    float flickerDuration;
    [SerializeField]
    float shakeRampPercentage;
    DateTime flickerStart;
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
            else if (id == "flicker")
            {
                StartCoroutine(Flicker());
            }
        };
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C))
            StartCoroutine(Flicker());
        if (shaking && (DateTime.Now - shakeStart).Milliseconds > shakeDuration)
            shaking = false;
        if (flickering && (DateTime.Now - flickerStart).Milliseconds > flickerDuration)
            flickering = false;
        currentNonGlitchPosition = Vector3.Lerp(myTransform.position, target, lerpSpeed*Time.deltaTime);
        currentNonGlitchPosition.x = pl.MyTransform.position.x;
        Debug.DrawRay(currentNonGlitchPosition, Vector3.up, Color.red, Time.deltaTime);
        myTransform.position = Vector3.Lerp(myTransform.position, (shaking) ? (currentNonGlitchPosition + glitchOffset) : currentNonGlitchPosition, (shaking) ? (shakeLerpSpeed * Time.deltaTime) : (resetLerpSpeed * Time.deltaTime));
	}

    IEnumerator Flicker()
    {
        flickering = true;
        Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("Gameplay objects"));
        while (flickering)
        {
            //if()
            yield return null;
        }
    }

    IEnumerator CameraShake()
    {
        shaking = true;
        shakeStart = DateTime.Now;
        glitchOffset = (UnityEngine.Random.insideUnitCircle * shakeDistance);
        while (shaking)
        {
            yield return null;
            if (myTransform.position == currentNonGlitchPosition + glitchOffset)
                glitchOffset = (UnityEngine.Random.insideUnitCircle * shakeDistance);
        }
    }
}
