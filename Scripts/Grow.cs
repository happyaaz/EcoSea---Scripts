using UnityEngine;
using System.Collections;

public class Grow : MonoBehaviour {

	public   bool scalingTR = false;
	public  float speed = 1.2f;
	public  float currentValue = 0.1f;
	public float lastValue = 1f;
	
	void Start() {
		scalingTR = true;
	}
	
	void Update() {
		
		if(scalingTR) 
		{
			if(currentValue >= lastValue) 
			{
				scalingTR = false;
			}
			else 
			{
				currentValue = Mathf.MoveTowards(currentValue, lastValue, Time.deltaTime%speed);
				transform.localScale = new Vector3(currentValue, currentValue, currentValue);
			}
		}
	}
}
