using UnityEngine;
using System.Collections;

public class Mute_Button : MonoBehaviour {
	public bool muted = false;

	private AudioListener myAudioListener;
	void Start(){
	//	myAudioListener = GameObject.FindObjectOfType<AudioListener> ();
	}
	void OnClick ()
	{
		if (muted == false) 
		{

			AudioListener.pause = true;
			muted = true;
		} else
		{
			AudioListener.pause = false;
			muted = false;
		}

	}
}
