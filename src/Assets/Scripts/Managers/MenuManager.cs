using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MenuManager : BaseManager<MenuManager>
{
    public GameObject LoadingScreen;
    public GameObject MenuScreen;

    protected enum MENU_STATE
    {
        mainMenu,
        loading
    };
    protected MENU_STATE _state;

    #region Display booleans
    protected bool _DisplayMainMenu
    {
        get
        {
            return GameManager.manager.IsMenu;
        }
    }

    protected bool _DisplayPauseButton
    {
        get
        {
            return GameManager.manager.IsPlaying;
        }
    }

    protected bool _DisplayPausedInGameMenu
    {
        get
        {
            return GameManager.manager.IsPaused;
        }
    }

    protected bool _DisplayGameOverMenu
    {
        get
        {
            return GameManager.manager.IsGameOver;
        }
    }
    #endregion

    #region GUIStyles
    #endregion

    #region Events
    public Action onPlayerButtonClicked;
    public Action onPlayButtonClicked;
    public Action onPauseButtonClicked;
    public Action onResumeButtonClicked;
    public Action onNewGameButtonClicked;
    public Action onMainMenuButtonClicked;
    #endregion

    #region BaseManager Overriden Methods
    protected override IEnumerator CoroutineStart()
    {
        yield return null;

        _state = MENU_STATE.mainMenu;

        IsReady = true;
        Debug.Log("Init MenuManager");
        LoadingScreen.SetActive(false);
        MenuScreen.SetActive(true);
    }
    #endregion

    void Update()
    {

        if (!IsReady) return;

    }

    public void OnClick(GameObject go)
    {
        if (go.name == "ButtonPlay")
        {
            LoadingScreen.SetActive(true);
            MenuScreen.SetActive(false);
            Application.LoadLevel(1);
            Debug.Log("isPlay");
            onPlayButtonClicked();
        }
    }
}
