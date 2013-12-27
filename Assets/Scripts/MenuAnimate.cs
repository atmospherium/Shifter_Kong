using UnityEngine;
using System.Collections;

public class MenuAnimate : MonoBehaviour {

	Color colorTarget;

	GameObject mainCamera;
	int qSamples = 4096;
	private float[] samples;

	Material backgroundMaterial;
	Vector2 backgroundDest = new Vector2(0f,0f);

	GameObject lightObject;

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.Find("Main Camera");
		lightObject = GameObject.Find ("MenuLight");
		backgroundMaterial = GameObject.Find ("BackgroundSquare").renderer.sharedMaterial;
		samples = new float[qSamples];
		colorTarget = renderer.sharedMaterial.GetColor("_TintColor");
	}
	
	// Update is called once per frame
	void Update () {

		if(Vector2.Distance(backgroundMaterial.mainTextureOffset,backgroundDest)<0.01f){
			Debug.Log("Matched");
			backgroundDest = new Vector2(Random.Range(0f,1f),Random.Range(0f,1f));
		}
		Vector2 textureOffset = Vector2.Lerp(backgroundMaterial.mainTextureOffset,backgroundDest,0.001f);
		backgroundMaterial.SetTextureOffset("_BumpMap",textureOffset);
		backgroundMaterial.mainTextureOffset = textureOffset;

		Color currentColor = renderer.material.GetColor("_TintColor");

		float floorRange = 200;
		float ceilRange = 255;

		float vol = GetRMS(0) + GetRMS(1);

		float blur = (vol/2)+0.5f;
		float alpha = (vol*vol*vol)+0.5f;
		colorTarget = new Color(blur,blur,blur,alpha);
		renderer.material.SetColor("_TintColor",colorTarget);

		lightObject.light.intensity = Mathf.Pow((1+Mathf.Pow (blur,5)),5);

	}

	float GetRMS(int channel){
		AudioListener.GetOutputData(samples,channel);
		float sum = 0;
		for(var i=0; i<qSamples; i++){
			sum+= samples[i]*samples[i];
		}
		return Mathf.Sqrt(sum/qSamples);
	}
}
