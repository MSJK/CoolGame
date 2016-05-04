using UnityEngine;
using System.Collections;

public class PostProcessEffect : MonoBehaviour
{
    public Material material = null;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, material);
    }
}
