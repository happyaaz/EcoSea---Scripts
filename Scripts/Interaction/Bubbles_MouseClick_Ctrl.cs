using UnityEngine;
using System.Collections;

public class Bubbles_MouseClick_Ctrl : MonoBehaviour {

	public GameObject Bubbles_MouseClick_Par_Pre;  //bubble prefab
	private Vector3 mousePos;
	private Vector3 objectPos;
	private GameObject bubbleParticleInstance;
	public AudioClip mouseClicking;					//bubble sound

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			//set a vector3 pos for the mouse one the screen
			mousePos = Input.mousePosition;
			mousePos.z = 2.0f;       // we want 2m away from the camera position

			//set the bubble position as mousepotions and instantiate
			objectPos = Camera.main.ScreenToWorldPoint(mousePos);
			bubbleParticleInstance = Instantiate(Bubbles_MouseClick_Par_Pre, objectPos, Bubbles_MouseClick_Par_Pre.transform.rotation) as GameObject;

			//play the bubble sound
			audio.PlayOneShot (mouseClicking, 0.1f);
		}
	
	}
}
