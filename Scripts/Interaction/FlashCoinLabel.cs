using UnityEngine;
using System.Collections;

public class FlashCoinLabel : MonoBehaviour {

	public GameObject flashing_glow;
	public float interval;
	private static bool startFlashing;
	private int points;
//	TweenColor tC;

	// Use this for initialization
	void Start () {
		InvokeRepeating("FlashLabel", 0.5f, interval);

//		tC = flashing_label.GetComponent<TweenColor>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FlashLabel(){
		if(startFlashing){
//			tC.Toggle();
			if(flashing_glow.activeSelf){


				NGUITools.SetActive(flashing_glow, false);
			}else{

				NGUITools.SetActive(flashing_glow, true);
			}

		}
	else{
			flashing_glow.SetActive(false);

		}
	}

	public static void startToFlash(){
		startFlashing = true;
	}
	public static void stopFlashing(){
		startFlashing = false;
	}
}
