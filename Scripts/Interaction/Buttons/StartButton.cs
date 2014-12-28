using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {

	public GameObject goals_go;

	void OnClick () {

		Application.LoadLevel (1);
	}
	

}
