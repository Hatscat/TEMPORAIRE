using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using ParticlePlayground;

public class MenuManager : BaseManager<MenuManager>
{
    public GameObject LoadingScreen;
    public GameObject MenuScreen;
    public GameObject PressStart;

    //Particle Playground
    public GameObject pp_titre;
    public GameObject pp_loading;

    public String scene;

    private bool isStart;
    private int cpt = 0;
    private float timerLoadingFake = 60;
    
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
        pp_titre.SetActive(true);
        pp_loading.SetActive(false);
        PressStart.SetActive(true);
        isStart = false;
    }
    #endregion

    private IEnumerator CoroutineLoading()
    {
        
        yield return new WaitForSeconds(5);
        onPlayButtonClicked();
        Application.LoadLevel(scene);
    }

    void Update()
    {

        if (!IsReady) return;
        if (Input.GetAxis("start") == 1 && !isStart)
           OnDownSpace();

    }
    private void OnDownSpace()
    {
        isStart = true;
        PressStart.SetActive(false);
        MenuScreen.SetActive(false);
        pp_loading.SetActive(true);
        //titre.SetActive(false);
        pp_titre.GetComponent<PlaygroundParticlesC>().loop = false;
        //
        Debug.Log("isPlay");

        StartCoroutine(CoroutineLoading());
    }
    public void OnClick(GameObject go)
    {
        Debug.Log("isPlay");
        if (go.name == "ButtonPlay")
        {
            //LoadingScreen.SetActive(true);
           
        }
    }
}
