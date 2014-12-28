using UnityEngine;
using System.Collections;

public class GUI_Main : MonoBehaviour {
	
	public UILabel HerbInfo;
	public UILabel PlantInfo;
	public UILabel CarniInfo;
	public UILabel OmniInfo;
	public UILabel VersionName;
	public UILabel AfterTurn;
	public UILabel Points;
	public UILabel Pointsget;

	public UILabel FirstGoal;
	public UILabel SecGoal;
	public UILabel ThridGoal;
	public UILabel ForthGoal;
	public UILabel FiftGoal;
	public UILabel SixthGoal;

	public UILabel Goal7;
	public UILabel Goal8;
	public UILabel Goal9;
	public UILabel Goal10;
	public UILabel Goal11;
	public UILabel Goal12;

	public UILabel Goal13;
	public UILabel Goal14;
	public UILabel Goal15;

	public UILabel SpecieInfoTitle;
	public UILabel SpecieInfoValue;
	public static int AvailablePoints = 30;
	public GameObject PlanktonActive;
	public GameObject SeaWeedActive;
	public GameObject SeaUrchinActive;
	public GameObject CoralActive;
	public GameObject AnchoviesActive;
	public GameObject CrabActive;
	public GameObject BarracudaActive;
	public GameObject PointsGlow;
	//	public UILabel CtrlInfo;
	public UISlider HerbivoreBar;
	public UISlider PlantBar;
	public UISlider CarnIBar;
	public UISlider OmniBar;
	public static int MaxHerbi;
	public static int MaxPlants;
	public static int MaxCarni;
	public static int MaxOmni;
	
	public GameObject plantSlider;
	private float plantSliderValue = 0.0f;
	
	public string prototypeName;
	CountingSpecies cS;
	public string AfterTurnReport = "";
	public bool displayAfterTurn = false;
	MiniGoals mg_scr;
	

	void Start ()
	{

//		Debug.Log (gameObject.name);

		AvailablePoints = 50;
		Camera.main.GetComponent<CountingSpecies> ();
		//	plantSliderValue = plantSlider.GetComponent<UIProgressBar> ();
		mg_scr = GameObject.Find ("MiniGoals").GetComponent<MiniGoals> ();

		MaxPlants = CommonData.controllingNumberOfSpecies ["Plant"];
		MaxHerbi = CommonData.controllingNumberOfSpecies ["Herbivore"];
		MaxOmni = CommonData.controllingNumberOfSpecies ["Omnivore"];
		MaxCarni = CommonData.controllingNumberOfSpecies ["Carnivore"];


		FirstGoal.text = mg_scr.miniGoalsGuiText_list [0];
		SecGoal.text = mg_scr.miniGoalsGuiText_list [1];
		ThridGoal.text = mg_scr.miniGoalsGuiText_list [2];
		ForthGoal.text = mg_scr.miniGoalsGuiText_list [3];
		FiftGoal.text = mg_scr.miniGoalsGuiText_list [4];
		SixthGoal.text = mg_scr.miniGoalsGuiText_list [5];
		Goal7.text = mg_scr.miniGoalsGuiText_list [6];
		Goal8.text = mg_scr.miniGoalsGuiText_list [7];
		Goal9.text = mg_scr.miniGoalsGuiText_list [8];
		Goal10.text = mg_scr.miniGoalsGuiText_list [9];
		Goal11.text = mg_scr.miniGoalsGuiText_list [10];
		Goal12.text = mg_scr.miniGoalsGuiText_list [11];
		Goal13.text = mg_scr.miniGoalsGuiText_list [12];
		Goal14.text = mg_scr.miniGoalsGuiText_list [13];
		Goal15.text = mg_scr.miniGoalsGuiText_list [14];

		// add stupid stuff


	}
	
	void Update () {
		
		//if ( CommonData.countingExistingFish.numberOfHerbivores > 0 && CommonData.countingExistingFish.numberOfPlants > 0)
		//{
		HerbInfo.text = "" + CommonData.countingExistingFish.numberOfHerbivores;
		PlantInfo.text =  "" + CommonData.countingExistingFish.numberOfPlants;
		CarniInfo.text =  "" + CommonData.countingExistingFish.numberofCarnivores;
		OmniInfo.text =  "" + CommonData.countingExistingFish.numberOfOmnivores;
		VersionName.text = VersionName.ToString();
		Points.text ="Coins: \n"+ AvailablePoints.ToString();
		//CtrlInfo.text = "A for Anchovies, P for Planktons, S for SeaHorse, T for Tune";
		HerbivoreBar.value = CommonData.countingExistingFish.numberOfHerbivores / (float)MaxHerbi;
		PlantBar.value = CommonData.countingExistingFish.numberOfPlants / (float)MaxPlants;
		CarnIBar.value = CommonData.countingExistingFish.numberofCarnivores / (float)MaxCarni;
		OmniBar.value = CommonData.countingExistingFish.numberOfOmnivores / (float)MaxOmni;



		VersionName.text = prototypeName;

		if (GUI_Main.AvailablePoints < 5) 
		{
			NGUITools.SetActive(PlanktonActive,false);
			NGUITools.SetActive(CoralActive,false);
			NGUITools.SetActive(SeaWeedActive,false);
		}

		if (GUI_Main.AvailablePoints >= 5) 
		{
			NGUITools.SetActive(PlanktonActive,true);
			NGUITools.SetActive(CoralActive,true);
			NGUITools.SetActive(SeaWeedActive,true);

		}
		if (GUI_Main.AvailablePoints < 10)
		{
			NGUITools.SetActive (AnchoviesActive, false);
			NGUITools.SetActive(SeaUrchinActive,false);
		}

		if (GUI_Main.AvailablePoints >= 10)
		{
			NGUITools.SetActive (AnchoviesActive, true);
			NGUITools.SetActive(SeaUrchinActive,true);
		}

		if (GUI_Main.AvailablePoints < 15)
		{
			NGUITools.SetActive (CrabActive, false);
		}

		if (GUI_Main.AvailablePoints >= 15)
		{
			NGUITools.SetActive (CrabActive, true);
		}
		if (GUI_Main.AvailablePoints < 20)
		{
			NGUITools.SetActive (BarracudaActive, false);
		}

		if (GUI_Main.AvailablePoints >= 20)
		{
			NGUITools.SetActive (BarracudaActive, true);
		}
		if (GUI_Main.AvailablePoints <= 0)
		{
			NGUITools.SetActive (PointsGlow, true);
		}
		if (GUI_Main.AvailablePoints > 0)
		{
			NGUITools.SetActive (PointsGlow, false);
		}








		if (displayAfterTurn) 
		{
			
			AfterTurn.text = AfterTurnReport.ToString ();
			Pointsget.text = "You received: " + ReprAndEat.totalPoints_int.ToString() + " Coins";
		}
		
		
	}
	
}