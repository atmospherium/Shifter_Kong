using UnityEngine;
using System.Collections;

public class MenuBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKeyDown){
			StartCoroutine(LoadGame());
		}
	}

	IEnumerator LoadGame(){
		GameObject mainCamera = GameObject.Find("Main Camera");
		audio.Play();
		while(mainCamera.audio.volume>0){
			Debug.Log ("Fading Audio");
			mainCamera.audio.volume -= 0.25f;
			yield return null;
		}

		float pauseLength = 0.5f;
		while(pauseLength>0){
			pauseLength-=Time.deltaTime;
			yield return null;
		}
		Debug.Log("BREAK");
		Application.LoadLevel(1);

	}
}
