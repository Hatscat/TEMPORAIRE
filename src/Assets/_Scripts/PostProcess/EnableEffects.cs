using UnityEngine;
using System.Collections;

public class EnableEffects : MonoBehaviour
{
    public KeyCode inputForAsciiEffect, inputForGreenAsciiEffect, inputForPixelEffect;

    private AsciiEffect asciiEffect;
    private GreenAsciiEffect greenAsciiEffect;
    private PixelEffect pixelEffect;

    void Start()
    {
        asciiEffect = GetComponent<AsciiEffect>();
        greenAsciiEffect = GetComponent<GreenAsciiEffect>();
        pixelEffect = GetComponent<PixelEffect>();
    }

    void Update()
    {
        if (Input.GetKeyUp(inputForAsciiEffect))
        {
            asciiEffect.enabled = !asciiEffect.enabled;
            greenAsciiEffect.enabled = false;
            pixelEffect.enabled = false;
        }
        else if (Input.GetKeyUp(inputForGreenAsciiEffect))
        {
            greenAsciiEffect.enabled = !greenAsciiEffect.enabled;
            asciiEffect.enabled = false;
            pixelEffect.enabled = false;
        }
        else if (Input.GetKeyUp(inputForPixelEffect))
        {
            pixelEffect.enabled = !pixelEffect.enabled;
            asciiEffect.enabled = false;
            greenAsciiEffect.enabled = false;
        }
    }
}