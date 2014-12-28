using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;


public class IndividualBars : MonoBehaviour {

	public List <GameObject> ratioThumbs_list = new List<GameObject> ();

	void Awake () {

		ratioThumbs_list = GameObject.FindGameObjectsWithTag ("RatioBar").ToList ();
	}


	void Start () {
	}


	public void ControlBars () {

		foreach (GameObject ratioBar in ratioThumbs_list)
		{
			
			ratioBar.transform.localPosition = new Vector3 (0, 0, 0);
			ratioBar.transform.localPosition += Vector3.down * 34;
		}

		foreach (string hunter in CommonData.tc_scr.existingSpeciesInTheScene)
		{
			//  find what this fish eats.
			//  figure out the numbers
			//  add the ratio



			int numberOfHunters = CommonData.fswn_scr.NumberOfExistingFishOfThisType (hunter, CommonData.tc_scr.GetArchetypeOfNeededFish (hunter));
			List <string> prey_list = CommonData.tc_scr.GetEatableFish (hunter);
			List <float> ratioValues_list = new List <float> ();

			//  if a hunter eats smth
			if (prey_list.Count > 0)
			{
			//	Debug.Log ("ib, hunter = " + hunter);
				//  add all the ratios in comparison with the hunter's prey
				foreach (string victim in prey_list)
				{
					int numberOfVictims = CommonData.fswn_scr.NumberOfExistingFishOfThisType (victim, CommonData.tc_scr.GetArchetypeOfNeededFish (victim));
					//Debug.Log ("numberOfVictims = " + numberOfVictims);
					if (numberOfVictims > 0)
					{
						float ratio = (float) numberOfHunters / (float) numberOfVictims;
						ratioValues_list.Add (ratio);
					}
				}

				float ratioForIndiv = 0;
				GameObject neededThumb_go = ratioThumbs_list.FirstOrDefault(go => go.name.Contains (hunter)) as GameObject;
//				Debug.Log ("neededThumb_go.name = " + neededThumb_go.name);
				//  we need to move it exactly to the middle, the to the beginning

				if (ratioValues_list.Count > 0)
				{
					float sumOfRatios = 0;
					foreach (float ratio in ratioValues_list) 
					{
						sumOfRatios += ratio;
					}
					ratioForIndiv = (float) sumOfRatios / (float) ratioValues_list.Count;
					
				//	Debug.Log ("Ratio is = " + ratioForIndiv + ", " + hunter);
					///	Debug.Log ("tempRatio = " + finalRatio + ", sumOfRatios = " + sumOfRatios + ", ratioValues_list.Count = " + ratioValues_list.Count);
					if (ratioForIndiv > 1)
					{
						ratioForIndiv = 1;
						neededThumb_go.transform.localPosition += Vector3.up * 68;
					}
					else
					{
						neededThumb_go.transform.localPosition += Vector3.up * 68 * ratioForIndiv;
					}

				}
				else
				{
				//	Debug.Log ("Nei, hunter");
					ratioForIndiv = 1;
					neededThumb_go.transform.localPosition += Vector3.up * 68;
				}
			}
			else
			{
				//  for plants that don't eat anything
				string plant = hunter;
				int numberOfThisPlant = numberOfHunters;
				List <string> huntersFor = CommonData.tc_scr.GetHuntersFor (plant);
				//  what should we dp abput planktons then? They don't eat anything =(

			//	Debug.Log ("ib, plant = " + plant);

				foreach (string hunterForThisFish in huntersFor)
				{
					int numberOfHuntersForThisPlant = CommonData.fswn_scr.NumberOfExistingFishOfThisType (hunterForThisFish, 
					                                                                                      CommonData.tc_scr.GetArchetypeOfNeededFish (hunterForThisFish));
				//	Debug.Log ("numberOfHuntersForThisPlant = " + numberOfHuntersForThisPlant);
					if (numberOfHuntersForThisPlant > 0)
					{
						float ratio = (float) numberOfHuntersForThisPlant / (float) numberOfThisPlant;
						ratioValues_list.Add (ratio);
					}

				}

				float ratioForIndiv = 0;
				GameObject neededThumb_go = ratioThumbs_list.FirstOrDefault(go => go.name.Contains (hunter)) as GameObject;
//				Debug.Log ("neededThumb_go.name = " + neededThumb_go.name);
				//  we need to move it exactly to the middle, the to the beginning
				
				if (ratioValues_list.Count > 0)
				{
					float sumOfRatios = 0;
					foreach (float ratio in ratioValues_list) 
					{
						sumOfRatios += ratio;
					}

				//	Debug.Log ("sumOfRatios = " + sumOfRatios);

					if (sumOfRatios > 1)
					{
						sumOfRatios = 1;
					}

					ratioForIndiv = 1 - sumOfRatios;
				//	Debug.Log ("Indiv ratio = " + ratioForIndiv);
					///	Debug.Log ("tempRatio = " + finalRatio + ", sumOfRatios = " + sumOfRatios + ", ratioValues_list.Count = " + ratioValues_list.Count);
						
					neededThumb_go.transform.localPosition += Vector3.up * 68 * ratioForIndiv;

				}
				else
				{
			//		Debug.Log ("Nei, plant");
					ratioForIndiv = 1;
					neededThumb_go.transform.localPosition += Vector3.up * 68;
				}
			}
		}
	}


	public void RefreshBars () {

		StartCoroutine (RefreshIt ());
	}


	IEnumerator RefreshIt () {

		yield return new WaitForSeconds (0.5f);
		CommonData.wc_scr.ControllingKnob ();
		ControlBars ();
	}
}
