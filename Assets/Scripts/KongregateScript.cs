using UnityEngine;
using System.Collections;

public class KongregateScript : MonoBehaviour {

	bool isKongregate = false;
	int userId = 0;
	static string username = "Guest";
	string gameAuthToken = "";

	public float timeGameStart;
	public float timeGameEnd;

	void OnKongregateAPILoaded(string userInfoString){
		isKongregate = true;

		string[] parameters = userInfoString.Split("|"[0]);
		userId =  int.Parse(parameters[0]);
		username = parameters[1];
		gameAuthToken = parameters[2];
	}

	void Awake(){
		DontDestroyOnLoad(this);
		Application.ExternalEval (
			"if(typeof(kongregateUnitySupport) != 'undefined'){" +
			"kongregateUnitySupport.initAPI('"+gameObject.name+"', 'OnKongregateAPILoaded');" +
			"}"
			);
	}

	public void BeatLevel(int levelNum, int time){
		string category = "Level "+levelNum.ToString()+" Complete";
		Application.ExternalCall("kongregate.stats.submit", category, 1);

		string timeCat = "Level "+levelNum.ToString()+" Complete Time";
		Application.ExternalCall ("kongregate.stats.submit", timeCat, time);
	}

	public void BeatGame(){
		Application.ExternalCall("kongregate.stats.submit", "Game Complete", 1);
		Application.ExternalCall("kongregate.stats.submit", "Game Complete Time", Mathf.Round(timeGameEnd-timeGameStart));
	}

}
