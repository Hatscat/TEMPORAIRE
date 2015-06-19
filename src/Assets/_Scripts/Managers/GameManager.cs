using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager manager;

    public enum GAME_STATE_MISSION { tutorial, briefing,find_rooom_secret, armory, find_captain};

    #region Game States
    public enum GAME_STATE { menu, play, pause, gameover };

    [HideInInspector]
    private GAME_STATE _state;

    public bool IsMenu
    {
        get { return _state == GAME_STATE.menu; }
    }

    public bool IsPlaying
    {
        get { return _state == GAME_STATE.play; }
    }

    public bool IsPaused
    {
        get { return _state == GAME_STATE.pause; }
    }

    public bool IsGameOver
    {
        get { return _state == GAME_STATE.gameover; }
    }
    #endregion

    #region Events
    public delegate void GameStepNotification(params object[] prms);

    public event GameStepNotification onGamePlay;
    public event GameStepNotification onGameResume;
    public event GameStepNotification onGamePause;
    public event GameStepNotification onGameMenu;
    public event GameStepNotification onGameOver;
    public event GameStepNotification onGameScoreHasChanged;
    #endregion

    [HideInInspector]
    public bool IsReady { get; private set; }

    void Awake()
    {
        manager = this;
    }

    void OnDestroy()
    {

        MenuManager.manager.onPlayerButtonClicked -= PlayerButtonClicked;
        MenuManager.manager.onPlayButtonClicked -= PlayButtonClicked;
        MenuManager.manager.onPauseButtonClicked -= PauseButtonClicked;
        MenuManager.manager.onResumeButtonClicked -= ResumeButtonClicked;
        MenuManager.manager.onNewGameButtonClicked -= NewGameButtonClicked;
        MenuManager.manager.onMainMenuButtonClicked -= MainMenuButtonClicked;
        Debug.Log("Destroy GameManager");
    }

    IEnumerator Start()
    {

        MenuManager.manager.onPlayerButtonClicked += PlayerButtonClicked;
        MenuManager.manager.onPlayButtonClicked += PlayButtonClicked;
        MenuManager.manager.onPauseButtonClicked += PauseButtonClicked;
        MenuManager.manager.onResumeButtonClicked += ResumeButtonClicked;
        MenuManager.manager.onNewGameButtonClicked += NewGameButtonClicked;
        MenuManager.manager.onMainMenuButtonClicked += MainMenuButtonClicked;

        _state = GAME_STATE.menu;

        IsReady = true;
        yield break;
    }

    #region Menu Callbacks

    void PlayerButtonClicked()
    {
        if (onGameScoreHasChanged != null)
            onGameScoreHasChanged();
    }

    void PlayButtonClicked()
    {
        _state = GAME_STATE.play;

        //CustomTimer.manager.ResetAndStart();

        if (onGamePlay != null)
            onGamePlay();

        /*
        if (onGameScoreHasChanged != null)
            onGameScoreHasChanged();
         * */

    }

    void PauseButtonClicked()
    {
        _state = GAME_STATE.pause;

        //CustomTimer.manager.StopTimer();

        if (onGamePause != null)
            onGamePause();

    }

    void ResumeButtonClicked()
    {
        _state = GAME_STATE.play;

        //CustomTimer.manager.StartTimer();

        if (onGameResume != null)
            onGameResume();

    }

    void NewGameButtonClicked()
    {
        PlayButtonClicked();

    }

    void MainMenuButtonClicked()
    {
        _state = GAME_STATE.menu;

        //CustomTimer.manager.StopTimer();

        if (onGameMenu != null)
            onGameMenu();
    }
    #endregion
}
