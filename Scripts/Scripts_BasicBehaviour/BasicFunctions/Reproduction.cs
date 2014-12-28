using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Reproduction : MonoBehaviour {

	SeaCreature sc_Parent;
	GettingSeaCreatureScript getSCScript;
	GameObject closestMate;
	public static Dictionary <string, int> numberOfMatingPairs_dict = new Dictionary <string, int> ();

	void Start () {

		sc_Parent = this.GetComponent <SeaCreature> ();
		getSCScript = this.GetComponent <GettingSeaCreatureScript> ();
	}


	public void ChoosingMate () {

		//  we do not look for tags, we look for objects of the same type, but they have to be the same species
		//  if this object was chosen as a mate (the event is still fired), we don't need to execute this code

		try
		{
		//  if there're some number of children to be born
			if (CommonData.rae_scr.numberOfBornFishPerFish_dict.ContainsKey (sc_Parent.nameOfSpecies))
			{
				//  and if there'are less than 3 mating pairs
				if (numberOfMatingPairs_dict [sc_Parent.nameOfSpecies] < 2)
				{
					if (sc_Parent.currentActionState_enum != SeaCreature.PossibleStatesOfActions.BeingChosenAsMateAndHeadingToItsMate)
					{
						if (this.gameObject != null)
						{
							if (GameObject.FindGameObjectsWithTag (this.tag).ToList ().Count > sc_Parent.maxAmountOfThisSpecies)
							{
								return;
							}
						}
						//  find at first species of the same type. Since mates have to be the same type of species, we specify it.
						//  And because we find all objects with the same tag, we should NOT include in the list THIS game object
						List <GameObject> findPossibleMates = CommonData.fswn_scr.FindMates (this.tag, this.name, this.gameObject);
						//  no point in checking then
						if (this.gameObject != null)
						{
							if (findPossibleMates.Count == 0)
							{
								return;
							}
						}
						//  we can't use fish that is already a mate
						findPossibleMates = FindFreeMates (findPossibleMates);
						//  then stop executing
						if (this.gameObject != null)
						{
							if (findPossibleMates.Count == 0)
							{
								return;
							}
							else
							{
								//  we want to know how many species are gonna get to their mates to reproduce
								//  sort it in adnvance

								//  going to mate? ++ to the number of mating pairs
								numberOfMatingPairs_dict [sc_Parent.nameOfSpecies] ++;
								findPossibleMates = CommonData.sf_scr.SortFish (findPossibleMates);
								//  and then choose the closest one
								closestMate = findPossibleMates [0];
								//  Spread sheet
								CommonData.rae_scr.spreadSheet_parent_list.Add (this.gameObject);
								CommonData.rae_scr.spreadSheet_parent_Checker_list.Add (this.gameObject);

								CommonData.numberOfFishGettingToMate ++;
								if (closestMate != null)
								{
									ChosingMate_ChoosingTheClosestMateAndSayingItToPrepare (closestMate);
								}
							}
						}
					}
				}
			}
		}
		catch
		{
				
		}
	}


	
	void ChosingMate_ChoosingTheClosestMateAndSayingItToPrepare (GameObject closestFullMate) {
		
		//  we still need to get the exact script of the closest mate
		SeaCreature sc_Mate = getSCScript.GetSeaCreatureScript (closestFullMate);
		//  now that object know its mate
		sc_Mate.mate_go = this.gameObject;
		//  just for consistence
		sc_Parent.mate_go = closestFullMate;
		//  change the states
		sc_Mate.currentActionState_enum = SeaCreature.PossibleStatesOfActions.BeingChosenAsMateAndHeadingToItsMate;
		sc_Parent.currentActionState_enum = SeaCreature.PossibleStatesOfActions.GettingToMate;


		
		CommonData.mg_scr.WriteDownEvent (sc_Parent.nameOfSpecies + "_GaveBirth_");
		CommonData.mg_scr.CheckMiniGoals ();
	}



	List <GameObject> FindFreeMates (List <GameObject> foundMates) {
		//  gettingtomate, being chosen

		List <GameObject> freeMates = new List <GameObject> ();

		foreach (GameObject go in foundMates)
		{
			SeaCreature sc_possible = getSCScript.GetSeaCreatureScript (go);
			//  that's the problem, newborn babies will also be here in the list
			if (sc_possible.currentActionState_enum != SeaCreature.PossibleStatesOfActions.GettingToMate &&
			    sc_possible.currentActionState_enum != SeaCreature.PossibleStatesOfActions.BeingChosenAsMateAndHeadingToItsMate &&
			    sc_possible.currentActionState_enum != SeaCreature.PossibleStatesOfActions.WasBorn
			    && sc_possible.currentActionState_enum != SeaCreature.PossibleStatesOfActions.WillDieNextRound)
			{
				freeMates.Add (go);
			}
		}

		return freeMates;
	}


	List <GameObject> ChoosingMate_Sorting (List <GameObject> possibleMates) {
		
		//  sort it by ascending (1 -> 2 -> 5 and etc.)
		for (int i = 0; i < possibleMates.Count - 1; i ++)
		{
			float sqrMag1 = Vector3.Distance (possibleMates [i].transform.position, transform.position);
			float sqrMag2 = Vector3.Distance (possibleMates [i + 1].transform.position, transform.position);
			
			if (sqrMag2 < sqrMag1)
			{
				GameObject temp = possibleMates [i];
				possibleMates [i] = possibleMates [i + 1];
				possibleMates [i + 1] = temp;
				i = 0;
			}
		}
		
		return possibleMates;
	}
}
