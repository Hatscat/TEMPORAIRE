using UnityEngine;
using System.Collections;

public class AppLoader : MonoBehaviour {

	public static AppLoader manager;

	public enum LOGO_SIZE_RESPECT_ASPECT_RATIO{doNotRespectAspectRatio,respectAspectRatioBasedOnWidth,respectAspectRatioBasedOnHeight};

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

	IEnumerator ChangeGUIColor(Color initColor,Color targetColor,float duration, AnimationCurve animCurve)
	{
		float elapsedTime = 0;
		
		while(elapsedTime<duration)
		{
			float k = elapsedTime/duration;
			guiColor = Color.Lerp(initColor,targetColor,animCurve.Evaluate(k));
			
			elapsedTime+=Time.deltaTime;
			yield return null;
		}
		
		guiColor = targetColor;
	}

	void Awake()
	{
		manager = this;
	}

	// Use this for initialization
	IEnumerator Start () {

		GUI.enabled = false;

		switch(logoSizeRespectAspectRatio)
		{
		case LOGO_SIZE_RESPECT_ASPECT_RATIO.doNotRespectAspectRatio:
			logoSize = new Vector2(Screen.width*logoRelSize.x,Screen.height*logoRelSize.y);
			break;
		case LOGO_SIZE_RESPECT_ASPECT_RATIO.respectAspectRatioBasedOnWidth:
			logoSize = new Vector2(Screen.width*logoRelSize.x,(logo.height/(float)logo.width)*(Screen.width*logoRelSize.x));
			break;
		case LOGO_SIZE_RESPECT_ASPECT_RATIO.respectAspectRatioBasedOnHeight:
			logoSize = new Vector2((logo.width/(float)logo.height)*(Screen.height*logoRelSize.y),Screen.height*logoRelSize.y);
			break;
		}

		yield return StartCoroutine(ChangeGUIColor(new Color(1,1,1,0),Color.white,animationDuration,popCurve));

		float elapsedTime = 0;
		while(elapsedTime<minWaitDuration
		       || !GameManager.manager.IsReady)
		{
			elapsedTime+=Time.deltaTime;
			yield return null;
		}

		vanishing = true;
		yield return StartCoroutine(ChangeGUIColor(Color.white,new Color(1,1,1,0), animationDuration,vanishCurve));

		GUI.enabled = true;

		Destroy(gameObject);
	}

	void OnGUI()
	{
		//Debug.Log(logoSize.x.ToString()+"    "+logoSize.y.ToString());

		GUI.depth = int.MinValue;

		GUI.color = vanishing ? guiColor:Color.white;
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),background,ScaleMode.StretchToFill);
		GUI.color = guiColor;
		GUI.DrawTexture(new Rect(Screen.width/2f-logoSize.x/2f,Screen.height/2f-logoSize.y/2f,logoSize.x,logoSize.y),logo,ScaleMode.StretchToFill);
	}
}
