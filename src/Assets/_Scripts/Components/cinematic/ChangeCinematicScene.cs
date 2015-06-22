using UnityEngine;
using System.Collections;

public class ChangeCinematicScene : MonoBehaviour {

    public string scene;
    public int timeAnimation;

	// Use this for initialization
	void Start () 
    {
        StartCoroutine(CoroutineAnimationWait());
	}
	
	// Update is called once per frame
	void Update () 
    {
       
	}

    private IEnumerator CoroutineAnimationWait()
    {
        yield return new WaitForSeconds(timeAnimation);
        Application.LoadLevel(scene);
    }
}
