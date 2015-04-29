using UnityEngine;
using System.Collections;

public class HudManager : BaseManager<HudManager> {

	#region Display booleans
    protected bool _DisplayServiceMsg
	{
		get{
			return GameManager.manager.IsPlaying && !PlayerRacket.manager.PlayerHasServed;
		}
	}

    protected bool _DisplayGameOverNOCongratMsg
	{
		get{
			return GameManager.manager.IsGameOver && !PlayersManager.manager.BestScoreHasJustBeenBeaten;
		}
	}

    protected bool _DisplayGameOverCongratMsg
	{
		get{
			return GameManager.manager.IsGameOver && PlayersManager.manager.BestScoreHasJustBeenBeaten;
		}
	}

    protected bool _DisplayGamePausedMsg
	{
		get{
			return GameManager.manager.IsPaused;
		}
	}
	#endregion

	#region GUI Styles
	public GUIStyle gameOverAreaGUIStyle;
	public GUIStyle smallTextGUIStyle;
	public GUIStyle mediumTextGUIStyle;
	public GUIStyle bigTextGUIStyle;
	#endregion

    protected int _displayedScore = 0;
    protected int _displayedBestScore = 0;

	#region BaseManager Overriden Methods
	protected override IEnumerator CoroutineStart ()
	{
		while(!(LANG.manager.IsReady
		      && PlayersManager.manager.IsReady)) 
			yield return null;

		IsReady = true;
		yield break;
	}
	
	protected override void ScoreHasChanged (params object[] prms)
	{
			RefreshDisplayedScore(PlayersManager.manager.Score,PlayersManager.manager.BestScore);
	}
	#endregion

    protected virtual void RefreshDisplayedScore(int score, int bestScore)
	{
		//Debug.Log("RefreshDisplayedScore");
		_displayedScore = score;
		_displayedBestScore = bestScore;
	}

	void OnGUI()
	{
		if(!IsReady) return;

		//Player, score et BestScore
		GUILayout.BeginArea(new Rect(Screen.width/2f,10,Screen.width/2f-10,Screen.height-10));
		GUITools.RIGHTLABEL(PlayersManager.manager.Player,mediumTextGUIStyle);
		GUILayout.Space(10);
		GUITools.RIGHTLABEL(LANG.manager.TEXT("SCORE")+": "+_displayedScore.ToString(),mediumTextGUIStyle);
		GUILayout.Space(10);
		GUITools.RIGHTLABEL(LANG.manager.TEXT("BEST_SCORE")+": "+_displayedBestScore.ToString(),mediumTextGUIStyle);
		GUILayout.EndArea();

		//Messages: service, pause, game over
		if(_DisplayServiceMsg)
		{
			GUILayout.BeginArea(new Rect(Screen.width/2f,Screen.height/2f,Screen.width/2f-10,Screen.height/2f-10));
			GUITools.BOTTOM(()=>{
				GUITools.RIGHTLABEL(LANG.manager.TEXT("CLICK_ANYWHERE_TO_SERVE"),mediumTextGUIStyle);
			});
			GUILayout.EndArea();
		}

		if(_DisplayGamePausedMsg)
		{
			GUILayout.BeginArea(new Rect(Screen.width/2f,Screen.height/2f,Screen.width/2f-10,Screen.height/2f-10));
			GUITools.BOTTOM(()=>{
				GUITools.RIGHTLABEL(LANG.manager.TEXT("GAME_PAUSED"),mediumTextGUIStyle);
			});
			GUILayout.EndArea();
		}

		//
		if(_DisplayGameOverCongratMsg)
		{
			GUILayout.BeginArea(new Rect(Screen.width/4f,Screen.height/3f,Screen.width/2f,Screen.height/3f),gameOverAreaGUIStyle);
			GUITools.VCENTER(()=>{
				GUITools.HCENTERLABEL(LANG.manager.TEXT("GAME_OVER"),mediumTextGUIStyle);
				GUILayout.Space(20);
				GUITools.HCENTERLABEL(LANG.manager.TEXT("CONGRATULATIONS")+" "+PlayersManager.manager.Player+" !   "+LANG.manager.TEXT("NEW_HIGH_SCORE")+" : "+_displayedBestScore.ToString(),mediumTextGUIStyle);
			});
			GUILayout.EndArea();
		}

		if(_DisplayGameOverNOCongratMsg)
		{
			GUILayout.BeginArea(new Rect(Screen.width/4f,Screen.height/3f,Screen.width/2f,Screen.height/3f),gameOverAreaGUIStyle);
			GUITools.VCENTER(()=>{
				GUITools.HCENTERLABEL(LANG.manager.TEXT("GAME_OVER"),mediumTextGUIStyle);
			});
			GUILayout.EndArea();
		}
	}

}
