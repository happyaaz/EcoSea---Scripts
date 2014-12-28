using UnityEngine;
using System.Collections;

public class MouseClickOnBigCollider : MonoBehaviour {

	public AnotherMoveScript aMS;
	string TextToDisplay;
	bool isSelected = false;
	GameObject SpeciesInfoScreen;
	GameObject uiRoot;
	finnaIndex fIW;
	GameObject ObjectHovered;
	bool ObjectWasHovered = false;
	GUI_Main gM;
	CountingSpecies cS;
	public string ObjName;
	int numberOfSpeciesToSend;
	public SeaCreature sC;
	string HuntedBy;
	public static bool guiElementIsOpen = false;
	TreasureChest trsChest;

	
	void Start (){
		gM = GameObject.Find ("NGuiLabelManager").GetComponent<GUI_Main> ();
		cS = Camera.main.GetComponent<CountingSpecies> ();
		fIW = GameObject.Find ("UI Root").GetComponent<finnaIndex> ();
		SpeciesInfoScreen = fIW.SpecieInfoScreen;
		trsChest = GameObject.Find ("PlayButton").GetComponent<TreasureChest> ();


	}
	
	void SendValuesToGUIMain (){

		System.Text.StringBuilder ThingsItEats = new System.Text.StringBuilder ();
		
		foreach (string Specie in sC.possibleFood) 
		{
			//			Debug.Log (specie);
			ThingsItEats.Append (Specie);
			ThingsItEats.Append (", ");
		}
//		HuntedBy = ObjectHovered.GetComponent<EatingHabits> ().EatenBy;

		if (ObjectHovered.name.Contains ("Plankton")) {
			numberOfSpeciesToSend = CommonData.countingExistingFish.numberOfPlankton;
//			numberOfSpeciesToSend = CommonData.countingExistingFish.numberOfPlants;

		} else if (ObjectHovered.name.Contains ("Coral")) {
			numberOfSpeciesToSend = CommonData.countingExistingFish.numberOfCorals;
			//			numberOfSpeciesToSend = CommonData.countingExistingFish.numberOfOmnivores;		}

		} else if (ObjectHovered.name.Contains ("SeaWeed")) {
			numberOfSpeciesToSend = CommonData.countingExistingFish.numberOfSeaWeed;
			//			numberOfSpeciesToSend = CommonData.countingExistingFish.numberOfOmnivores;		}

		} else if (ObjectHovered.name.Contains ("Big")) {
			numberOfSpeciesToSend = CommonData.countingExistingFish.numberOfAnchovies;

		} else if (ObjectHovered.name.Contains ("SeaUrchin")) {
			numberOfSpeciesToSend = CommonData.countingExistingFish.numberOfSeaUrchins;

		} else if (ObjectHovered.name.Contains ("Crab")) {
			numberOfSpeciesToSend = CommonData.countingExistingFish.numberOfCrabs;
			//numberOfSpeciesToSend = CommonData.countingExistingFish.numberOfOmnivores;

		} else if (ObjectHovered.name.Contains ("Barracouda")) {	
			numberOfSpeciesToSend = CommonData.countingExistingFish.numberOfBarracudas;
//			numberOfSpeciesToSend = CommonData.countingExistingFish.numberofCarnivores;		
		}
		gM.SpecieInfoTitle.text = "" + ObjName;
		gM.SpecieInfoValue.text = "Alive:  " + numberOfSpeciesToSend + "\r\n" + "Type:  " +  CommonData.tc_scr.GetArchetypeOfNeededFish (ObjName) + "\r\n" + "Eats:  " +  ThingsItEats + "\r\n" + "Hunted by:  " + HuntedBy;
//			+ Mathf.Abs(cS.numberOfSpeciesAfter[ObjName] - cS.numberOfSpeciesBefore [ObjName]);
	}


	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (!isSelected){
//			if (!ObjectHovered.name.Contains("UI")){
				if (Physics.Raycast (ray, out hit, Mathf.Infinity)) 
			{
//					(ObjectHovered.GetComponent("Halo") as Behaviour).enabled = false;
					ObjectWasHovered = true;
					ObjectHovered = hit.transform.gameObject;
				if (!ObjectHovered.name.Contains("UI") && !ObjectHovered.name.Contains("Ok") && !ObjectHovered.name.Contains ("Mesh")){
//						(ObjectHovered.GetComponent("Halo") as Behaviour).enabled = true;
					}

				} else {
					if (ObjectWasHovered && !isSelected){
//						(ObjectHovered.GetComponent("Halo") as Behaviour).enabled = false;
						ObjectWasHovered = false;
					}
//				}
			}
		}
		if (Input.GetMouseButtonDown (0))
		{
			//Debug.Log ("isSelected "+isSelected);
			if (Physics.Raycast(ray, out hit, Mathf.Infinity))
			{
				try
				{
					if (ObjectHovered.GetComponent <DraggingBarracouda> () != null)
					{
						if (ObjectHovered.GetComponent <DraggingBarracouda> ().enabled == true )
						{
							return;
						}
					}
					else if (ObjectHovered.GetComponent <DraggingIn> () != null)
					{
						if (ObjectHovered.GetComponent <DraggingIn> ().enabled == true )
						{
							return;
						}
					}

					else if (ObjectHovered.GetComponent <DraggingInRandomZ> () != null)
					{
						if (ObjectHovered.GetComponent <DraggingInRandomZ> ().enabled == true )
						{
							return;
						}
					}
					else if (ObjectHovered.GetComponent <DraggingInSUandC> () != null)
					{
						if (ObjectHovered.GetComponent <DraggingInSUandC> ().enabled == true )
						{
							return;
						}
					}
				}
				catch
				{
					
				}

				if (ObjectHovered.GetComponent("Halo") != null)
				{
//				isSelected = false;



					if (!isSelected && !ObjectHovered.name.Contains("UI")&& !ObjectHovered.name.Contains ("Mesh") && !ObjectHovered.name.Contains("Chest") && !MouseClickOnBigCollider.guiElementIsOpen)
					{
						isSelected = true;
						(ObjectHovered.GetComponent("Halo") as Behaviour).enabled = true;
	//					TextToDisplay = "You hit " + hit.transform.gameObject.ToString();
//						Debug.Log ("You hit" + hit.transform.gameObject);
						SpeciesInfoScreen.SetActive (true);
						guiElementIsOpen = true;

						if (ObjectHovered.name.Contains ("BigCollider")){
							aMS = ObjectHovered.GetComponent<AnotherMoveScript> ();
							sC = aMS.reference.GetComponent<SeaCreature>();
							HuntedBy = aMS.reference.GetComponent<EatingHabits> ().EatenBy;
	//						ObjName = sC.nameOfSpecies;
						} 
						else 
						{
							sC = ObjectHovered.GetComponent<SeaCreature>();
							HuntedBy = ObjectHovered.GetComponent<EatingHabits> ().EatenBy;
						}
						ObjName = sC.nameOfSpecies;
						SendValuesToGUIMain ();
					}

					else 
					{
						isSelected = false;
						if (ObjectHovered != null && !ObjectHovered.name.Contains("UI")&& !ObjectHovered.name.Contains("MainGoal") && !ObjectHovered.name.Contains ("Mesh"))
						{
							(ObjectHovered.gameObject.GetComponent("Halo") as Behaviour).enabled = false;
						}
					}
				}//get points and destroy chest
				else if(!isSelected && !ObjectHovered.name.Contains("UI") && !ObjectHovered.name.Contains ("Mesh") && !MouseClickOnBigCollider.guiElementIsOpen && trsChest.chestAutomaticOpens == false)
				{
//					Debug.Log (ObjectHovered.name);
					if (ObjectHovered.name.Contains ("Chest_Rand_Pref"))
						StartCoroutine(trsChest.CollectRandomTreasureChest ());
//					else if(ObjectHovered.name.Contains("Chest_GUI_Pref"))
//					{
//						StartCoroutine(trsChest.CollectGUITreasureChest ());
//						Debug.Log ("GUI chest hovered over");
//					}				
				}
			}
		}
	}
}
