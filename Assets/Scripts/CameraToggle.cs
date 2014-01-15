using UnityEngine;
using System.Collections;

public class CameraToggle : MonoBehaviour {

	static int quality = 1;
	static Renderer bgRenderer;

	public Material hiRes;
	public Material loRes;

	// Use this for initialization
	void Start () {
		bgRenderer = GameObject.Find("BG").renderer;

		if(quality==1){
			Camera.main.renderingPath = RenderingPath.Forward;
		}else{
			Camera.main.renderingPath = RenderingPath.VertexLit;
		}

	}

	void Awake(){
		DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Q)){
			if(quality == 1){
				bgRenderer.material = loRes;
				quality = 0;
			}else{
				bgRenderer.material = hiRes;
				quality = 1;
			}


			if(Camera.main.renderingPath == RenderingPath.VertexLit){
				quality = 1;
				Camera.main.renderingPath = RenderingPath.Forward;
			}else{
				quality = 2;
				Camera.main.renderingPath = RenderingPath.VertexLit;
			}

		}
	}
}
