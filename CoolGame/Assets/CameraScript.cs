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
    float flickerStrength;
    DateTime flickerStart;

    // Camera Shaders
    [SerializeField]
    Material noiseMaterial;

    [SerializeField]
    Material crtMaterial;

    [SerializeField]
    Material verticalFlipMaterial;

    [SerializeField]
    Material horizontalFlipMaterial;

    private CameraGlitchEffect cameraEffects;
    
	// Use this for initialization
	void Start () {
        pl = (Player)(GameObject.Find("Player").GetComponent(typeof(Player)));
        myTransform = transform;
        currentNonGlitchPosition = transform.position;
        glitchOffset = Vector3.zero;
	    cameraEffects = GetComponent<CameraGlitchEffect>();
        ((GameManager)(GameObject.Find("GameManager").GetComponent(typeof(GameManager)))).ItemBought += (id) =>
        {
            switch (id)
            {
                case "screen-shake":
                    StartCoroutine(CameraShake());
                    break;

                case "noise":
                    CameraNoise();
                    break;

                case "flicker":
                    StartCoroutine(Flicker());
                    break;

                case "vertical-flip":
                    CameraVerticalFlip();
                    break;

                case "horizontal-flip":
                    CameraHorizontalFlip();
                    break;
            }
        };
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C))
            StartCoroutine(Flicker());
        if (shaking && (DateTime.Now - shakeStart).TotalMilliseconds > shakeDuration)
            shaking = false;
        if (flickering && (DateTime.Now - flickerStart).TotalMilliseconds > flickerDuration)
            flickering = false;
        if (shaking || flickering)
            DigitGroup.isGlitching = true;
        else
            DigitGroup.isGlitching = false;
        currentNonGlitchPosition = Vector3.Lerp(myTransform.position, target, lerpSpeed*Time.deltaTime);
        currentNonGlitchPosition.x = pl.MyTransform.position.x;
        Debug.DrawRay(currentNonGlitchPosition, Vector3.up, Color.red, Time.deltaTime);
        myTransform.position = Vector3.Lerp(myTransform.position, (shaking) ? (currentNonGlitchPosition + glitchOffset) : currentNonGlitchPosition, (shaking) ? (shakeLerpSpeed * Time.deltaTime) : (resetLerpSpeed * Time.deltaTime));
	}

    IEnumerator Flicker()
    {
        flickering = true;
        flickerStart = DateTime.Now;
        bool on = false;
        Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("Gameplay objects"));
        while (flickering)
        {
            if(UnityEngine.Random.value>1-Time.deltaTime*flickerStrength)
            {
                if (on)
                {
                    Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("Gameplay objects"));
                    on = false;
                }
                else
                {
                    Camera.main.cullingMask |= (1 << LayerMask.NameToLayer("Gameplay objects"));
                    on = true;
                }
            }
            yield return null;
        }
        Camera.main.cullingMask |= (1 << LayerMask.NameToLayer("Gameplay objects"));
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

    void CameraNoise()
    {
        cameraEffects.StartEffect(3, noiseMaterial);
    }

    void CameraVerticalFlip()
    {
        cameraEffects.StartEffect(3, verticalFlipMaterial);
    }

    void CameraHorizontalFlip()
    {
        cameraEffects.StartEffect(3, horizontalFlipMaterial);
    }
}
