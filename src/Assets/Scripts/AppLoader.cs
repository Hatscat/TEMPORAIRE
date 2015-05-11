using UnityEngine;
using System.Collections;

public class AppLoader : MonoBehaviour {

    /*
     * Class Loading, SplashScreen
     * */
    public static AppLoader manager;

    public enum LOGO_SIZE_RESPECT_ASPECT_RATIO { doNotRespectAspectRatio, respectAspectRatioBasedOnWidth, respectAspectRatioBasedOnHeight };

    public Texture2D background;

    public Texture2D logo;
    public Vector2 logoRelSize;
    public LOGO_SIZE_RESPECT_ASPECT_RATIO logoSizeRespectAspectRatio;
    Vector2 logoSize;

    public AnimationCurve popCurve;
    public AnimationCurve vanishCurve;

    public float animationDuration;
    public float minWaitDuration;

    protected Color guiColor;

    protected bool vanishing = false;

    void Awake()
    {
        manager = this;
    }

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
