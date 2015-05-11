using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class MenuManager : BaseManager<MenuManager>
{
		enum MENU_STATE
		{
				mainMenu,
				newPlayer}
		;
		MENU_STATE _state;
		public string gameTitleText;
		public Texture2D gameTitleDrawTexture;
		public Vector2 gameTitleTextureSize;
		private Vector2 _scrollPosition = Vector2.zero;
		private string _newPlayerName = "";
    /*

	#region Display booleans
		private bool _DisplayMainMenu {
				get {
						return GameManager.manager.IsMenu;
				}
		}
	
		private bool _DisplayPauseButton {
				get {
						return GameManager.manager.IsPlaying;
				}
		}
	
		private bool _DisplayPausedInGameMenu {
				get {
						return 	GameManager.manager.IsPaused;
				}
		}
	
		private bool _DisplayGameOverMenu {
				get {
						return 	GameManager.manager.IsGameOver;
				}
		}
	#endregion
	
	#region GUIStyles
		public GUIStyle mainMenuAreaGUIStyle;
		public GUIStyle gameTitleGUIStyle;
		public GUIStyle playersListGUIStyle;
		public GUIStyle newPlayerGUIStyle;
		public GUIStyle buttonGUIStyle;
		public GUIStyle buttonChosenPlayerGUIStyle;
		public GUIStyle buttonDeletePlayerGUIStyle;
		public GUIStyle pauseButtonGUIStyle;
		public GUIStyle smallTextGUIStyle;
		public GUIStyle mediumTextGUIStyle;
		public GUIStyle bigTextGUIStyle;
	#endregion

	#region Events
		public Action onPlayerButtonClicked;
		public Action onPlayButtonClicked;
		public Action onPauseButtonClicked;
		public Action onResumeButtonClicked;
		public Action onNewGameButtonClicked;
		public Action onMainMenuButtonClicked;
#endregion

		public bool IsPointInsidePauseIconZone (Vector2 pt)
		{
				//Debug.Log(pt*1000);
				return   (pt.x < 10 + Screen.width * .1f) && (pt.y < 10 + Screen.width * .1f);
		}


	#region BaseManager Overriden Methods
		protected override IEnumerator CoroutineStart ()
		{
				while (!(LANG.manager.IsReady
		       && PlayersManager.manager.IsReady))
						yield return null;
				
				_state = MENU_STATE.mainMenu;
	
				IsReady = true;
		}
	#endregion

		void OnGUI ()
		{
				if (!IsReady)
						return;

				Vector2 areaSize = new Vector2 (3 * Screen.width / 5f, 3 * Screen.height / 5f);

				if (_DisplayMainMenu) {
						//le titre
						GUILayout.BeginArea (new Rect (10, 10, Screen.width / 2f, Screen.height / 2f));
						GUITools.TOP (() => {
								GUILayout.Label (gameTitleDrawTexture, gameTitleGUIStyle, GUILayout.Width (gameTitleTextureSize.x), GUILayout.Height (gameTitleTextureSize.y));
						});
						GUILayout.EndArea ();

						switch (_state) {
						case MENU_STATE.mainMenu:
        //le menu principal
								GUILayout.BeginArea (new Rect (Screen.width / 2f - areaSize.x / 2f, Screen.height / 2f - areaSize.y / 2f, areaSize.x, areaSize.y), mainMenuAreaGUIStyle);
								GUILayout.Space (20);
								GUITools.HCENTERLABEL (LANG.manager.TEXT ("CHOOSE_A_PLAYER"), bigTextGUIStyle);
								GUILayout.Space (20);

								GUITools.HCENTER (() => {
										_scrollPosition = GUILayout.BeginScrollView (_scrollPosition, playersListGUIStyle, GUILayout.Width (areaSize.x * .9f));
        
										foreach (KeyValuePair<string,int> bestScore in PlayersManager.manager.BestScores) {
												GUITools.HGUI (() => {
														if (GUILayout.Button (bestScore.Key + "\t\t\t" + bestScore.Value.ToString (), 
					                      bestScore.Key.Equals (PlayersManager.manager.Player) ? buttonChosenPlayerGUIStyle : buttonGUIStyle)) {
																PlayersManager.manager.Player = bestScore.Key;
																if (onPlayerButtonClicked != null)
																		onPlayerButtonClicked ();
														}
														GUILayout.Space (10);
														
														if (!bestScore.Key.Equals (PlayersManager.manager.DefaultPlayerName) && GUILayout.Button ("", buttonDeletePlayerGUIStyle)) {
																if (bestScore.Key.Equals (PlayersManager.manager.Player))
																		PlayersManager.manager.Player = PlayersManager.manager.DefaultPlayerName;
																PlayersManager.manager.DeletePlayer (bestScore.Key);
																
														}
														
														GUILayout.Space (20);
												});
												GUILayout.Space (5);
											
										}
										GUILayout.EndScrollView ();
								});
								GUILayout.Space (20);
								GUITools.HCENTER (() => {
										if (GUILayout.Button (LANG.manager.TEXT ("NEW_PLAYER"), buttonGUIStyle)) {
												_state = MENU_STATE.newPlayer;
												_newPlayerName = "";
										}

										GUILayout.FlexibleSpace ();
										if (GUILayout.Button (LANG.manager.TEXT ("PLAY"), buttonGUIStyle))
										if (onPlayButtonClicked != null)
												onPlayButtonClicked ();
								});
								GUILayout.Space (20);
								GUILayout.EndArea ();
								break;
						case MENU_STATE.newPlayer:
							
								GUILayout.BeginArea (new Rect (Screen.width / 2f - areaSize.x / 2f, Screen.height / 2f - areaSize.y / 2f, areaSize.x, areaSize.y), mainMenuAreaGUIStyle);
								
								GUITools.VCENTER (() => {        
										GUITools.HCENTER (() => {
												GUILayout.Label (LANG.manager.TEXT ("NEW_PLAYER_NAME"), mediumTextGUIStyle);
												GUILayout.FlexibleSpace ();
												_newPlayerName = GUILayout.TextField (_newPlayerName, newPlayerGUIStyle, GUILayout.Width (areaSize.x * .3f));
										});
										GUILayout.FlexibleSpace ();

										GUITools.HCENTER (() => {
												if (GUILayout.Button (LANG.manager.TEXT ("OK"), buttonGUIStyle)) {
														PlayersManager.manager.AddPlayer (_newPlayerName);
														_state = MENU_STATE.mainMenu;
												}
												GUILayout.FlexibleSpace ();
												if (GUILayout.Button (LANG.manager.TEXT ("CANCEL"), buttonGUIStyle))
														_state = MENU_STATE.mainMenu;
										});
								});
								GUILayout.EndArea ();
								break;
						}



				}

				if (_DisplayPauseButton) {
						GUILayout.BeginArea (new Rect (10, 10, Screen.width * .1f, Screen.width * .1f));
						GUITools.TOP (() => {
								GUITools.LEFT (() => {
										if (GUILayout.Button ("", pauseButtonGUIStyle, GUILayout.Width (Screen.width * .1f), GUILayout.Height (Screen.width * .1f))
												&& onPauseButtonClicked != null)
												onPauseButtonClicked ();
								});
						});
						GUILayout.EndArea ();
				}

				if (_DisplayPausedInGameMenu) {
						GUILayout.BeginArea (new Rect (10, 10, Screen.width / 3f, Screen.height / 2f));
						GUITools.TOP (() => {
								if (GUILayout.Button (LANG.manager.TEXT ("RESUME"), buttonGUIStyle) && onResumeButtonClicked != null)
										onResumeButtonClicked ();
								if (GUILayout.Button (LANG.manager.TEXT ("NEW_GAME"), buttonGUIStyle) && onNewGameButtonClicked != null)
										onNewGameButtonClicked ();
								if (GUILayout.Button (LANG.manager.TEXT ("MAIN_MENU"), buttonGUIStyle) && onMainMenuButtonClicked != null)
										onMainMenuButtonClicked ();
						});
						GUILayout.EndArea ();
				}

				if (_DisplayGameOverMenu) {
						GUITools.TOP (() => {
								GUILayout.BeginArea (new Rect (10, 10, Screen.width / 3f, Screen.height / 2f));
      
								if (GUILayout.Button (LANG.manager.TEXT ("NEW_GAME"), buttonGUIStyle) && onNewGameButtonClicked != null)
										onNewGameButtonClicked ();
								if (GUILayout.Button (LANG.manager.TEXT ("MAIN_MENU"), buttonGUIStyle) && onMainMenuButtonClicked != null)
										onMainMenuButtonClicked ();
						});
						GUILayout.EndArea ();
				}
		}
     */
}
