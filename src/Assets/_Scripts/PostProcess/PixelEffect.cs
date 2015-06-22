using UnityEngine;


public class PixelEffect : ImageEffectBase
{
    // Called by camera to apply image effect
    void OnRenderImage (RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}
