using UnityEngine;
using System.Collections;

public class Sound_Whale_SFX_Fader : MonoBehaviour {

	// volume
	private float sound_vol = 0.0f;
	private float backgroundSound_vol = 0.0f;
	public float sound_volEnd;
	public AudioSource whaleSound;
	
	
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
	void FixedUpdate(){ 

		
		//Silhouette SFX fader
		if (fadeIn == true)
			if (sound_vol <= sound_volEnd)
		{
			fadeOut = false;
			sound_vol = sound_vol + 0.01f;
			whaleSound.volume = sound_vol;
		}		
		if (fadeOut == true)
			if (sound_vol >= 0)
		{
			fadeIn = false;
			sound_vol = sound_vol - 0.002f;
			whaleSound.volume = sound_vol;
		}
		

		
	}
	
	IEnumerator FadeInDelay ()	{
		
		yield return new WaitForSeconds (10);
		fadeOut = false;
		fadeIn = true;
		StartCoroutine(FadeOutDelay ());
	}
	
	IEnumerator FadeOutDelay ()
	{
		yield return new WaitForSeconds (2);
		fadeIn = false;
		fadeOut = true;
		StartCoroutine(FadeInDelay ());
	}

}
