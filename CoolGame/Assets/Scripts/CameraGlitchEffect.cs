using UnityEngine;
using System.Collections;

public class CameraGlitchEffect : MonoBehaviour
{
    private Material currentEffect = null;
    private int effectTimeID = 0;
    private int maxTimeID = 0;
    private float effectTime = 0;
    private float maxTime = 0;

    public void StartEffect(float time, Material material)
    {
        currentEffect = material;
        maxTime = time;
        effectTime = 0;
    }

    void Start()
    {
        effectTimeID = Shader.PropertyToID("_EffectTime");
        maxTimeID = Shader.PropertyToID("_MaxTime");
    }

    void Update()
    {
        effectTime += Time.deltaTime;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (effectTime < maxTime)
        {
            currentEffect.SetFloat(effectTimeID, effectTime);
            currentEffect.SetFloat(maxTimeID, maxTime);
            Graphics.Blit(src, dest, currentEffect);
        }
    }
}
