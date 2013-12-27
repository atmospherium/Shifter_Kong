using UnityEngine;
using System.Collections;

public class MenuBehavior : MonoBehaviour {
	GameObject menuMain;
	GameObject menuAbout;
	string screenLoaded = "main";

	// Use this for initialization
	void Start () {
		menuMain = GameObject.Find("Main");
		menuAbout = GameObject.Find("About");
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Return)){
			if(screenLoaded=="main"){
				StartCoroutine(LoadGame());
			}else{
				StartCoroutine(GoToMain());
			}
		}else if(Input.GetKeyDown (KeyCode.A)){
			StartCoroutine (GoToAbout());
		}
		Debug.Log(screenLoaded);
	}

	IEnumerator GoToAbout(){
		while(Mathf.Abs(transform.position.x-menuAbout.transform.position.x)>0.001){
			Vector3 targetPos = new Vector3(menuAbout.transform.position.x, menuAbout.transform.position.y, transform.position.z);
			transform.position = Vector3.Lerp(transform.position,targetPos,0.3f);
			yield return null;
		}
		screenLoaded = "about";
		yield return null;
	}

	IEnumerator GoToMain(){
		while(Mathf.Abs(transform.position.x-menuMain.transform.position.x)>0.001){
			Vector3 targetPos = new Vector3(menuMain.transform.position.x, menuMain.transform.position.y, transform.position.z);
			transform.position = Vector3.Lerp(transform.position,targetPos,0.3f);
			yield return null;
		}
		screenLoaded = "main";
		yield return null;
	}

	IEnumerator LoadGame(){
		audio.Play();
		while(audio.volume>0){
			Debug.Log ("Fading Audio");
			audio.volume -= 0.25f;
			yield return null;
		}

		float pauseLength = 0.5f;
		while(pauseLength>0){
			pauseLength -= Time.deltaTime;
			yield return null;
		}
		Debug.Log("BREAK");
		Application.LoadLevel(1);

	}
}
