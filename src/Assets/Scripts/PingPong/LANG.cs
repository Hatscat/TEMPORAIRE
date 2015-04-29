using UnityEngine;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
//using TinyXmlParser;

public class LANG : MonoBehaviour
{
	public static LANG manager;
	public bool IsReady{get;private set;}
	public bool dontDestroyGameObjectOnLoad=false;

	public enum BEHAVIOR { langDefInInspector, langTakenFromDeviceSettings };
	public BEHAVIOR behavior;
	
	public enum LANGUAGE { FR, EN };

	[HideInInspector]
	public CultureInfo cultureInfo;
	
	public LANGUAGE Lang
	{
		get
		{
			return _currentLang;
		}
	}
	
	public LANGUAGE userDefinedLang;
	LANGUAGE _currentLang;
  

	Dictionary<string, string> dico = new Dictionary<string, string>();

	
	public string TEXT(string ID)
	{
		string result;
		dico.TryGetValue(ID, out result);
		
		if (string.IsNullOrEmpty(result))
		{
			//Debug.LogWarning( "No Text for TextID " + ID );
			return ( ID == null ? "" : ID.ToString() );
		}
		else return result;
	}
	
	IEnumerator LoadLocalizedFile(LANGUAGE lang)
	{
		IsReady =false;

		ResourceRequest request = Resources.LoadAsync("Localization/"+lang.ToString(),typeof(TextAsset));
		yield return request;

		dico = new Dictionary<string, string>();

		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.LoadXml((request.asset as TextAsset).text);
		
		foreach (XmlNode node in xmlDoc.FirstChild.ChildNodes)
		{
			dico.Add(node.Name, node.InnerText.Trim());
			//Debug.Log(node.Name+" "+dico[node.Name]);
		}

		IsReady = true;
	}

	public void SetLanguage(LANGUAGE lang)
	{
		_currentLang = lang;
		StartCoroutine(LoadLocalizedFile(_currentLang));
	}
	
	void Awake()
	{
		IsReady = false;

		if (manager) Destroy(gameObject);
		else
		{
			manager = this;
			if(dontDestroyGameObjectOnLoad) DontDestroyOnLoad(gameObject);
			
			switch (behavior)
			{
			case BEHAVIOR.langDefInInspector:
				SetLanguage(userDefinedLang);
				break;
			case BEHAVIOR.langTakenFromDeviceSettings:
				SystemLanguage language = Application.systemLanguage;
				
				switch (language)
				{
				case SystemLanguage.French:
					SetLanguage(LANGUAGE.FR);
					break;
				default:
					SetLanguage(LANGUAGE.EN);
					break;
				}
				break;
			}
			
			//cultureInfo
			switch (_currentLang)
			{
			case LANGUAGE.EN:
				cultureInfo = CultureInfo.CreateSpecificCulture("en-US");
				break;
			case LANGUAGE.FR:
				cultureInfo = CultureInfo.CreateSpecificCulture("fr-FR");
				break;
			}
		}
	}
}
