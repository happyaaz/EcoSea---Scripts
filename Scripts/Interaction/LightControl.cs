using UnityEngine;
using System.Collections;

public class LightControl : MonoBehaviour {
	
	public UISlider slider;
	public Light light;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		light.intensity = slider.value;
	}
}
