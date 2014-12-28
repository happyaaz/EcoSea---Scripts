using UnityEngine;
using System.Collections;

public class SliderButtonController : MonoBehaviour
{
	public GameObject slider;
	private bool activated;
	// Use this for initialization
	void Start ()
	{
		slider.SetActive (false);
		activated = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	void OnClick ()
	{
		if (activated) {
			slider.SetActive (false);
			activated = false;
		}  else {
			slider.SetActive (true);
			activated = true;
		}
	}

	void OnTooltip (bool show)
	{
		UITooltip.ShowText ("Adjust Sea Brightnes");
	}
}