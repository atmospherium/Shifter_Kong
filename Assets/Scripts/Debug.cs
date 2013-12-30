using UnityEngine;

public class Debug {

	private static bool debugging = false;

	public static new void Log(object message){
		if(!debugging) return;
		if( Application.platform == RuntimePlatform.OSXWebPlayer
		   || Application.platform == RuntimePlatform.WindowsWebPlayer ){
			Application.ExternalCall("console.log",message);
		}else{
			UnityEngine.Debug.Log(message.ToString());
		}
	}
}
