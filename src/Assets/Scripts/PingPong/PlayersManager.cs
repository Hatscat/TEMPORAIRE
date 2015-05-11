using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

public class PlayersManager : BaseManager<PlayersManager> {
    /*
	public bool resetPlayerPrefs;

	public string DefaultPlayerName{ get{ return "Player1";}}

	#region BaseManager Overriden Methods
	protected override IEnumerator CoroutineStart ()
	{
		if(resetPlayerPrefs) PlayerPrefs.DeleteAll();
		IsReady = true;
		yield break;
	}

	protected override void Play (params object[] prms)
	{
		Score=0;
		_previousBestScore = BestScore;
		BestScoreHasJustBeenBeaten = false;
	}
	
	protected override void GameOver (params object[] prms)
	{
		if(BestScore>_previousBestScore)
			BestScoreHasJustBeenBeaten = true;
		
		_shouldRefreshBestScores = true;
	}
	#endregion

	public string Player
	{
		get{
			if(!PlayerPrefs.HasKey(name+"_"+this.GetType()+"_Player"))
				Player = DefaultPlayerName;

			return PlayerPrefs.GetString(name+"_"+this.GetType()+"_Player");
		}
		set{
			PlayerPrefs.SetString(name+"_"+this.GetType()+"_Player",value);
			_shouldRefreshBestScores = true;
		}
	}

	private List<string> _players = null;
	private bool _shouldRefreshPlayers = true;

	public List<string> Players
	{
		get{
			if(_players == null || _shouldRefreshPlayers)
			{
				if(!PlayerPrefs.HasKey(name+"_"+this.GetType()+"_Players"))
					Players = new List<string>(){DefaultPlayerName};

				string[] items = PlayerPrefs.GetString(name+"_"+this.GetType()+"_Players").Split(';');

				_players = new List<string>();
				for (int i = 0; i < items.Length; i++) 
					_players.Add(items[i]);

			}
			return _players;
		}
		set
		{
			string str = "";
			for (int i = 0; i < value.Count; i++)
				str+=value[i]+(i<value.Count-1 ? ";":"");

			PlayerPrefs.SetString(name+"_"+this.GetType()+"_Players",str);
			_shouldRefreshPlayers = true;
		}
	}

	public bool AddPlayer(string playerName)
	{
		List<string> tempPlayers = Players;

		Regex regHexPattern = new Regex(@"^[a-zA-Z0-9_@.-]+$");

		if(string.IsNullOrEmpty(playerName)
		   || !regHexPattern.IsMatch(playerName)
		   || tempPlayers.Contains(playerName)
		   ) return false;
		else{
			tempPlayers.Add(playerName);
			Players = tempPlayers;
			_shouldRefreshPlayers = true;
			_shouldRefreshBestScores = true;
			return true;
		}
	}

	public void DeletePlayer(string playerName)
	{
		List<string> tempPlayers = Players;
		tempPlayers.Remove(playerName);
		Players = tempPlayers;

		_shouldRefreshPlayers = true;
		_shouldRefreshBestScores = true;
	}

	public int Score
	{
		get{
			if(!PlayerPrefs.HasKey(name+"_"+this.GetType()+"_"+Player+"_Score"))
				Score = 0;

			return (PlayerPrefs.GetInt(name+"_"+this.GetType()+"_"+Player+"_Score"));
		}

		set{
			PlayerPrefs.SetInt(name+"_"+this.GetType()+"_"+Player+"_Score",value);
			BestScore = Mathf.Max(Score,BestScore);

		}
	}

	public int BestScore
	{
		get{
			if(!PlayerPrefs.HasKey(name+"_"+this.GetType()+"_"+Player+"_BestScore"))
				BestScore = 0;
			
			return (PlayerPrefs.GetInt(name+"_"+this.GetType()+"_"+Player+"_BestScore"));
		}
		
		set{
			PlayerPrefs.SetInt(name+"_"+this.GetType()+"_"+Player+"_BestScore",value);
			
		}
	}

	private int _previousBestScore
	{
		get{
			if(!PlayerPrefs.HasKey(name+"_"+this.GetType()+"_"+Player+"_previousBestScore"))
				_previousBestScore = 0;
			
			return (PlayerPrefs.GetInt(name+"_"+this.GetType()+"_"+Player+"_previousBestScore"));
		}
		
		set{
			PlayerPrefs.SetInt(name+"_"+this.GetType()+"_"+Player+"_previousBestScore",value);
		}
	}

	public bool BestScoreHasJustBeenBeaten
	{
		get{
			if(!PlayerPrefs.HasKey(name+"_"+this.GetType()+"_"+Player+"_BestScoreHasJustBeenBeaten"))
				BestScoreHasJustBeenBeaten = false;
			
			return (PlayerPrefs.GetInt(name+"_"+this.GetType()+"_"+Player+"_BestScoreHasJustBeenBeaten")==1);
		}
		
		set{
			PlayerPrefs.SetInt(name+"_"+this.GetType()+"_"+Player+"_BestScoreHasJustBeenBeaten",value?1:0);
			
		}
	}

	public int GetScore(string playerName)
	{
		if(!PlayerPrefs.HasKey(name+"_"+this.GetType()+"_"+playerName+"_Score"))
			return 0;
		else return PlayerPrefs.GetInt(name+"_"+this.GetType()+"_"+playerName+"_Score");
	}

	public int GetBestScore(string playerName)
	{
		if(!PlayerPrefs.HasKey(name+"_"+this.GetType()+"_"+playerName+"_BestScore"))
			return 0;
		else return PlayerPrefs.GetInt(name+"_"+this.GetType()+"_"+playerName+"_BestScore");
	}

	public int IncreaseScore(int increment)
	{
		Score+=increment;
		return Score;
	}

	private Dictionary<string,int> _bestScores = null;
	private bool _shouldRefreshBestScores = true;

	public Dictionary<string,int> BestScores
	{
		get{
			if(_bestScores == null || _shouldRefreshBestScores)
			{
				_bestScores = new Dictionary<string, int>();
				List<string> players = Players;
				foreach (string player in players) 
					_bestScores.Add(player,GetBestScore(player));
			}
			return _bestScores;
		}
	}
     */
}
