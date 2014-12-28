using UnityEngine;
using System.Collections;

public class MouseClickSoundCtrl : MonoBehaviour {

	public AudioClip mouseClicking;
	private bool playMouseClickSound;

	void Start ()
	{
		playMouseClickSound = true;
	}

	void OnClick()
	{
		if (playMouseClickSound) 
		{
			playMouseClickSound = false;
			playMouseClickSound = true;
			audio.PlayOneShot (mouseClicking, 0.1f);
		}
	}
}
