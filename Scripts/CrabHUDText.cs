using UnityEngine;
using System.Collections;

[RequireComponent(typeof(HUDText))]
public class CrabHUDText : MonoBehaviour {

	void OnClick()
		
	{
		HUDText AnchoviesSpawnTekst = GetComponent<HUDText> ();
		if(GUI_Main.AvailablePoints >= 15 && CommonData.countingExistingFish.numberOfOmnivores < GUI_Main.MaxOmni)
			AnchoviesSpawnTekst.Add ("-15 Coins",new Color32 (255, 79, 0,0), 0.0f);
	}
}
