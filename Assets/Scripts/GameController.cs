using UnityEngine;
using System.Collections;


public enum Dir { none, up, down, left, right };
public class GameController : MonoBehaviour {

	GameObject kongregateObject;
	KongregateScript kongregateScript;

	float timeLevelStart;
	float timeLevelEnd;

	GameObject audioController;
	GameObject exit;
	float fadeBack = 1.6f;
	float fadeForward = -1.2f;
	float fadeSpeed = 3.0f;

	GameObject fader;

	// Use this for initialization
	void Start () {
		kongregateObject = GameObject.Find ("_Kongregate");
		kongregateScript = kongregateObject.GetComponent<KongregateScript>();
		audioController = GameObject.Find("_AudioController");
		//audioController.audio.volume = 0;
		fader = GameObject.Find ("Fader");

		timeLevelStart = Time.time;
		if(Application.loadedLevel==1){
			kongregateScript.timeGameStart = timeLevelStart;
		}

		//StartCoroutine("FadeIn");
		//StartCoroutine("FadeInMusic");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LevelComplete(){
		StartCoroutine("FadeOut");
	}

	private IEnumerator FadeInMusic(){
		Debug.Log("Audio Fade");
		
		while(audioController.audio.volume<1){
			Debug.Log ("Fading Audio");
			audioController.audio.volume += 0.02f;
			yield return null;
		}
		audioController.audio.volume = 1;
	}

	private IEnumerator FadeIn(){


		fader.transform.position = new Vector3(fader.transform.position.x,fader.transform.position.y,fadeForward);
		float countdown = 0.7f;
		while(countdown>0){
			countdown-=Time.deltaTime;
			yield return null;
		}
		while(fader.transform.localPosition.z<fadeBack){
			fader.transform.position = Vector3.Lerp(fader.transform.position,new Vector3(0,0,fadeBack),0.05f);
			yield return null;
		}
	}

	private IEnumerator FadeOut(){
		exit = GameObject.Find("Exit");
		exit.GetComponentInChildren<AudioSource>().Play();
		string message = exit.GetComponent<MessageHolder>().message;
		while(audioController.audio.volume>0){
			Debug.Log ("Fading Audio");
			audioController.audio.volume -= 0.25f;
			yield return null;
		}
		while(fader.transform.localPosition.z>fadeForward){
			Debug.Log ("Fading");
			fader.transform.Translate(new Vector3(0,0,-fadeSpeed*Time.deltaTime));
			yield return null;
		}

		int nextLevel;
		string completeText;
		float counter;

		int levelCount = Application.levelCount;
		int loadedLevel = Application.loadedLevel;

		if(levelCount==loadedLevel+1){
			nextLevel=0;
			counter = 5;
		}else{
			nextLevel = Application.loadedLevel+1;
			counter = 0.7f;
		}

		timeLevelEnd = Time.time;

		if(loadedLevel == levelCount-2){
			kongregateScript.timeGameEnd = timeLevelEnd;
			kongregateScript.BeatGame();
		}
		kongregateScript.BeatLevel(loadedLevel,(int)Mathf.Round(timeLevelEnd-timeLevelStart));

		//GameObject levelComplete = GameObject.Find("LevelComplete");
		//levelComplete.GetComponent<TextMesh>().text = message;

		while(counter>0){
			counter-=Time.deltaTime;
			yield return null;
		}

		Application.LoadLevel(nextLevel);

	}
}
