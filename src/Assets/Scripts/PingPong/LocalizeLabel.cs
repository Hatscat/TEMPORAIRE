using UnityEngine;
using System.Collections;

public class LocalizeLabel : MonoBehaviour
{

    public string code;

    // Use this for initialization
    IEnumerator Start()
    {

        while (!LANG.manager.IsReady)
            yield return null;

        UILabel label = GetComponent<UILabel>();
        if (label) label.text = LANG.manager.TEXT(code);
        Destroy(this);
    }
}
