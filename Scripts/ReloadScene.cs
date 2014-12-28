using UnityEngine;
using System.Collections;

public class ReloadScene : MonoBehaviour {

	void OnTooltip (bool show)
	{
		UITooltip.ShowText ("Reset The Game");
	}

	void OnClick ()
	{
			Application.LoadLevel (0);
		Tutorial_Ctrl.ResetBools ();
	}
}
