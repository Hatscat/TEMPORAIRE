using UnityEngine;
using System.Collections;

public class AppLoaderNGUI : AppLoader
{

    public UIPanel panelLoadingLogo;
    public UISprite sprBackground;
    public UISprite sprLogo;

    new void OnGUI()
    {
    }

    void OnDestroy()
    {
        if (panelLoadingLogo != null)
            NGUITools.Destroy(panelLoadingLogo); // .gameObject
    }

    void Update()
    {
        sprBackground.color = vanishing ? guiColor : Color.white;
        sprLogo.color = guiColor;
    }
}
