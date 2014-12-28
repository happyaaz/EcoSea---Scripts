using UnityEngine;
using System.Collections;

public class SpawnSpeciesButtons : MonoBehaviour {

	private Vector3 randomPos;
	public string nameOfSpeciesToThrowIn = string.Empty;
	public UILabel ToolTipText;
	public static int valueAnchovie = 10, valuePlankton = 5, valueBarracouda = 20, valueCrab = 15, valueSeaWeed = 5, valueSeaUrchin = 10, valueCoral = 5;
	public static bool cantClickBecauseOfDraggingIn;
	Tutorial_Ctrl nMC;

	public GameObject anchovieIndiv_go;

	GameObject draggedInObject = null;

	void Start (){
		nMC = Camera.main.GetComponent<Tutorial_Ctrl> ();
		
	}

	void Awake (){
		cantClickBecauseOfDraggingIn = false;
	}

	void OnPress (bool isOver) {
		//  REMEMBER

		if (isOver == true)
		{
		if (nameOfSpeciesToThrowIn == "Anchovie" && GUI_Main.AvailablePoints >= 10 && 
		    CommonData.countingExistingFish.numberOfHerbivores < GUI_Main.MaxHerbi && 
		    !Tutorial_Ctrl.UseTutorial || nameOfSpeciesToThrowIn == "Anchovie" && 
		    GUI_Main.AvailablePoints >= 10 && CommonData.countingExistingFish.numberOfHerbivores < 1 && Tutorial_Ctrl.move3) 
		{
			if (Tutorial_Ctrl.move3){
				CommonData.countingExistingFish.CountNumberOfExistingFish();
			}
			InstantiatingNeededObject ();
			GUI_Main.AvailablePoints -= SpawnSpeciesButtons.valueAnchovie;
			if (!Tutorial_Ctrl.move5){
				nMC.FindNextStep();
			}
		}
		else if (nameOfSpeciesToThrowIn == "Plankton" && GUI_Main.AvailablePoints >= 5 && 
		         CommonData.countingExistingFish.numberOfPlants < GUI_Main.MaxPlants && !Tutorial_Ctrl.UseTutorial || 
		         nameOfSpeciesToThrowIn == "Plankton" && GUI_Main.AvailablePoints >= 5 && 
		         CommonData.countingExistingFish.numberOfPlants < 1 && Tutorial_Ctrl.move1 && !Tutorial_Ctrl.move2 || 
		         nameOfSpeciesToThrowIn == "Plankton" && GUI_Main.AvailablePoints >= 5 && 
		         CommonData.countingExistingFish.numberOfPlants < 2 && Tutorial_Ctrl.move9 && !Tutorial_Ctrl.move10) 
		{
			InstantiatingNeededObject ();
			GUI_Main.AvailablePoints -= SpawnSpeciesButtons.valuePlankton;
			if (!Tutorial_Ctrl.move2){
				nMC.FindNextStep();
			}
			if (Tutorial_Ctrl.move9){
				CommonData.countingExistingFish.CountNumberOfExistingFish();
				if (CommonData.countingExistingFish.numberOfPlankton > 1){
					nMC.FindNextStep();
				}
			}
		}
		else if (nameOfSpeciesToThrowIn == "Barracouda" && GUI_Main.AvailablePoints >= 20 && CommonData.countingExistingFish.numberofCarnivores < GUI_Main.MaxCarni) 
		{
			InstantiatingNeededObject ();
			GUI_Main.AvailablePoints -= SpawnSpeciesButtons.valueBarracouda;
		}
		else if (nameOfSpeciesToThrowIn == "Crab" && GUI_Main.AvailablePoints >= 15 && CommonData.countingExistingFish.numberofCarnivores < GUI_Main.MaxOmni) 
		{
			InstantiatingNeededObject ();
			GUI_Main.AvailablePoints -= SpawnSpeciesButtons.valueCrab;
		}
		else if (nameOfSpeciesToThrowIn == "SeaWeed" && GUI_Main.AvailablePoints >= 5 && CommonData.countingExistingFish.numberOfPlants < GUI_Main.MaxPlants) 
		{
			InstantiatingNeededObject ();
			GUI_Main.AvailablePoints -= SpawnSpeciesButtons.valueSeaWeed;
		}
		else if (nameOfSpeciesToThrowIn == "SeaUrchin" && GUI_Main.AvailablePoints >= 10 && CommonData.countingExistingFish.numberOfOmnivores < GUI_Main.MaxOmni) 
		{
			InstantiatingNeededObject ();
			GUI_Main.AvailablePoints -= SpawnSpeciesButtons.valueSeaUrchin;
		}
		else if (nameOfSpeciesToThrowIn == "Coral" && GUI_Main.AvailablePoints >= 5 && CommonData.countingExistingFish.numberOfPlants < GUI_Main.MaxPlants) 
		{
			InstantiatingNeededObject ();
			GUI_Main.AvailablePoints -= SpawnSpeciesButtons.valueCoral;
		}
		}
	}

	void OnTooltip (bool show){

		if (nameOfSpeciesToThrowIn == "Anchovie") 
		{
			UITooltip.ShowText ("Anchovie");
		}
		else if (nameOfSpeciesToThrowIn == "Plankton") 
		{
			UITooltip.ShowText ("Plankton");
		}
		else if (nameOfSpeciesToThrowIn == "Barracouda") 
		{
			UITooltip.ShowText ("Barracouda");
		}
		else if (nameOfSpeciesToThrowIn == "Crab") 
		{
			UITooltip.ShowText ("Crab");
		}
		else if (nameOfSpeciesToThrowIn == "SeaWeed") 
		{
			UITooltip.ShowText ("SeaWeed");
		}
		else if (nameOfSpeciesToThrowIn == "SeaUrchin") 
		{
			UITooltip.ShowText ("SeaUrchin");
		}
		else if (nameOfSpeciesToThrowIn == "Coral") 
		{
			UITooltip.ShowText ("Coral");
		}
//		Debug.Log ("I am Actually here, but you can't see me :P");
	}
	
	
	public void InstantiatingNeededObject () {

		if (nameOfSpeciesToThrowIn != "Anchovie")
		{
			//
			GameObject species = CommonData.listOfPrefabs_scr.SendInstantiatedObject (nameOfSpeciesToThrowIn);
	//		Debug.Log (species.name);
			//  events
			SeaCreature sc = species.GetComponent <SeaCreature> ();
			
			string eventStr = sc.nameOfSpecies + "_WasSpawned_";
			CommonData.mg_scr.WriteDownEvent (eventStr);

			DraggingIn (species);

			if (!CommonData.tc_scr.existingSpeciesInTheScene.Contains (sc.nameOfSpecies))
			{
				CommonData.tc_scr.existingSpeciesInTheScene.Add (sc.nameOfSpecies);
			}
			//  BECAUSE
			CommonData.cs_scr.CountBeforeTurn ();

			CommonData.ib_scr.ControlBars ();

			CommonData.mg_scr.CheckMiniGoals ();
		}
		else
		{
			GameObject species = anchovieIndiv_go;

			string eventStr = "Anchovie_WasSpawned_";
			CommonData.mg_scr.WriteDownEvent (eventStr);

			DraggingIn (species);
			
			if (!CommonData.tc_scr.existingSpeciesInTheScene.Contains ("Anchovie"))
			{
				CommonData.tc_scr.existingSpeciesInTheScene.Add ("Anchovie");
			}
			//  BECAUSE
			CommonData.cs_scr.CountBeforeTurn ();
			
			CommonData.ib_scr.ControlBars ();
			
			CommonData.mg_scr.CheckMiniGoals ();
		}
	}


	//  not for planktons
	void DraggingIn (GameObject species) {

//		Debug.Log ("Fuck off");

		Vector3 positionOfInstantiation = Vector3.zero;

		RaycastHit[] hits;

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		hits = Physics.RaycastAll(ray, Mathf.Infinity);
		int i = 0;
		while (i < hits.Length) 
		{
			RaycastHit hit = hits[i];
			if (hit.transform.tag == "OceanFloor") 
			{
				positionOfInstantiation = new Vector3 (hit.point.x, 
				                                       hit.point.y, 
				                                       hit.point.z);
//				Debug.Log ("positionOfInstantiation = " + positionOfInstantiation + ", point = " + hit.point);
				break;
			}
			i++;
		}

		draggedInObject = Instantiate (species, positionOfInstantiation, species.transform.rotation) as GameObject;

		if (nameOfSpeciesToThrowIn == "Coral" || nameOfSpeciesToThrowIn == "SeaWeed")
		{
			draggedInObject.AddComponent <DraggingIn> ();
			DraggingIn di_scr = draggedInObject.GetComponent <DraggingIn> ();
			di_scr.timeOfSpawning = Time.time;
		}
		else if (nameOfSpeciesToThrowIn == "Plankton")
		{
			draggedInObject.AddComponent <DraggingInRandomZ> ();
			DraggingInRandomZ driz_scr = draggedInObject.GetComponent <DraggingInRandomZ> ();
			driz_scr.timeOfSpawning = Time.time;
		}
		else if (nameOfSpeciesToThrowIn == "Barracouda")
		{
			SeaCreature scBarr_scr = draggedInObject.GetComponent <SeaCreature> ();
			scBarr_scr.currentActionState_enum = SeaCreature.PossibleStatesOfActions.BeingDragged;
			draggedInObject.AddComponent <DraggingBarracouda> ();
		}
		else if (nameOfSpeciesToThrowIn == "SeaUrchin" || nameOfSpeciesToThrowIn == "Crab")
		{
			SeaCreature scBarr_scr = draggedInObject.GetComponent <SeaCreature> ();
			scBarr_scr.currentActionState_enum = SeaCreature.PossibleStatesOfActions.BeingDragged;

			if (this.name.Contains ("Crab"))
			{
				Crab crab_scr = draggedInObject.gameObject.GetComponent <Crab> ();
				crab_scr.GetCharacterController ();
				crab_scr.DisableChararacterController ();
				
			}
			else
			{
				SeaUrchin su_scr = draggedInObject.gameObject.GetComponent <SeaUrchin> ();
				su_scr.GetCharacterController ();
				su_scr.DisableChararacterController ();
			}

			draggedInObject.AddComponent <DraggingInSUandC> ();
		}
		else if (nameOfSpeciesToThrowIn == "Anchovie")
		{
			draggedInObject.AddComponent <DraggingAnchovie> ();
		}
	}

	
	private void RandomPosition () {
		
		float randomSpot_x_fl = Random.Range (-10, 10);
		float randomSpot_y_fl = Random.Range (0, 10);
		float randomSpot_z_fl = Random.Range (3, 25);
		
		randomPos = new Vector3 (randomSpot_x_fl, randomSpot_y_fl, randomSpot_z_fl);
	}
}
