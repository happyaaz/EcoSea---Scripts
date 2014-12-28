using UnityEngine;
using System.Collections;

public class SoundCtrl : MonoBehaviour {

	// volume
	private float music_vol = 0.0f;
	private float backgroundSound_vol = 0.0f;
	public float music_volEnd;
	public float backgroundSound_volEnd;
	public AudioSource music;
	public AudioSource backgroundSound;



	private bool firstPlay = true;
	private bool fadeIn = false;
	private bool fadeOut = false;

	void Awake()
	{
		fadeIn = true;

//		StartCoroutine("FadeOutMusic");
	}	

	void Start ()
	{
		StartCoroutine(FadeOutDelay ());
	}

	//for better performance it might be better to do this in an invoke repeating function
	void FixedUpdate(){ // volume out in the game

		//fade in for the ambience BG
		if(backgroundSound_vol <= backgroundSound_volEnd)
			{
				backgroundSound_vol = backgroundSound_vol + 0.01f;
				backgroundSound.volume = backgroundSound_vol; 
			}

		//music fader
		if (fadeIn == true)
			if (music_vol <= music_volEnd)
			{
				fadeOut = false;
				music_vol = music_vol + 0.01f;
				music.volume = music_vol;
			}		
		if (fadeOut == true)
			if (music_vol >= 0)
			{
				fadeIn = false;
				music_vol = music_vol - 0.002f;
				music.volume = music_vol;
			}

		//Silhouette SFX fader


	}

	IEnumerator FadeInDelay ()	{

		yield return new WaitForSeconds (85);
		fadeOut = false;
		fadeIn = true;
		StartCoroutine(FadeOutDelay ());
	}

	IEnumerator FadeOutDelay ()
	{
		yield return new WaitForSeconds (85);
		fadeIn = false;
		fadeOut = true;
		StartCoroutine(FadeInDelay ());
	}



}


