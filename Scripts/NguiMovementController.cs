﻿using UnityEngine;
using System.Collections;

public class NguiMovementController : MonoBehaviour {

	public GameObject TutorialFish, tutorialtext; 
	public UILabel TutorialText;
	UISprite TutorialGlowSprite;
	public GameObject TutorialGlow;
	public static bool UseTutorial = true, move1 = false, move2 = false, move3 = false, move4 = false, move5 = false, move6 = false, move7 = false, move8 = false, move9 = false, move10 = false, move11 = false, move12 = false, move13 = false, move14 = false, move15 = false, move16 = false, move17 = false;
	public Vector3 atSpawnBar, atMiniMap, atTheBar, atPlayButton, atCenter, atCoinCounter, destPosition;
	TweenPosition tP;
	float timerForSteps = 0; 
	public float timeForStep1 = 5;
	TweenPosition herbiShowBar;
	TweenPosition plantShowBar;
	TweenPosition MinimapShowBar;
	Quaternion straightRotation;
	UISprite uS;
	UI2DSpriteAnimation U2A;
	Vector3 spriteScale;
	Vector3 spriteOriginalScale;
//	Vector3 PlanktonMinimap;
	public GameObject HideButtonPlant, HideButtonHerbi, HideButtonOmni, HideButtonCarni, HideButtonMiniMap, PlayButton, SeaWeedBalanceBar, SeaWeedBalanceBarIcon, CoralBalanceBar, CoralBalanceBarIcon, SeaUrchinBalanceBar, SeaUrchinBalanceBarIcon;
	public GameObject SeaUrchinBalanceBarNeedle, CoralBalanceBarNeedle, SeaWeedBalanceBarNeedle;

	bool isMoving = false, destReached = false, goingRight = false;
	float speed = 250, step;

	void Start () {
//		U2A = TutorialFish.GetComponent<UI2DSpriteAnimation> ();
//		U2A.;
//		U2A.enabled = true;
//		U2A.Play();
		TutorialText.text = "Hi! I'm your game guide Fishy!";
		spriteScale = TutorialFish.transform.localScale;
		spriteScale.x *= -1;
//		TutorialFish.transform.localScale = spriteScale;
		spriteOriginalScale = TutorialFish.transform.localScale;
		straightRotation = TutorialFish.transform.localRotation;
		atSpawnBar = new Vector3 (-245,-160,0);
		atMiniMap = new Vector3 (410, 35, 0);
		atTheBar = new Vector3 (175, 155, 0);
		atPlayButton = new Vector3 (100, -140, 0);
		atCoinCounter = new Vector3 (-280,175,0);
		atCenter = new Vector3 (75,-120,0);

		tP = TutorialFish.GetComponent<TweenPosition> ();
//		TutorialText.text = "Hi! I'm you game guide Fishy!";
		herbiShowBar = GameObject.Find ("Herbivore_WindowInfo").GetComponent<TweenPosition>();
		plantShowBar = GameObject.Find ("Plant_WindowInfo").GetComponent<TweenPosition> ();
		MinimapShowBar = GameObject.Find ("FoodChainWindow").GetComponent<TweenPosition> ();
		HideButtonPlant = GameObject.Find ("PlantHideButton");
		HideButtonHerbi = GameObject.Find ("HerbieHideButton");
		HideButtonOmni = GameObject.Find ("OmniHideButton");
		HideButtonCarni = GameObject.Find ("CarniHideButton");
		HideButtonMiniMap = GameObject.Find ("FoodChainWindowMoveButton");
		PlayButton = GameObject.Find ("PlayButton");
		uS = TutorialFish.GetComponent<UISprite> ();
		TutorialGlowSprite = TutorialGlow.GetComponent<UISprite> ();
		if (NguiMovementController.UseTutorial) {
			NGUITools.SetActive (HideButtonPlant,false);	
			NGUITools.SetActive (HideButtonHerbi,false);
			NGUITools.SetActive (HideButtonOmni,false);
			NGUITools.SetActive (HideButtonCarni,false);
			NGUITools.SetActive (HideButtonMiniMap, false);
			NGUITools.SetActive (PlayButton, false);
			NGUITools.SetActive (SeaUrchinBalanceBarIcon, false);
			NGUITools.SetActive (SeaUrchinBalanceBar, false);
			NGUITools.SetActive (CoralBalanceBarIcon, false);
			NGUITools.SetActive (CoralBalanceBar, false);
			NGUITools.SetActive (SeaWeedBalanceBarIcon, false);
			NGUITools.SetActive (SeaWeedBalanceBar, false);
//			NGUITools.SetActive (CoralBalanceBarNeedle, false);
//			NGUITools.SetActive (SeaWeedBalanceBarNeedle, false);
//			NGUITools.SetActive (SeaUrchinBalanceBarNeedle, false);
		}

	}

	void Update (){
//		U2A.Play ();
//		Debug.Log (U2A.isPlaying);
		if (!NguiMovementController.move1 && TutorialFish.activeSelf 
		    || NguiMovementController.move2 && !NguiMovementController.move3 && destReached 
		    || NguiMovementController.move3 && !NguiMovementController.move4 && destReached 
		    || NguiMovementController.move5 && !NguiMovementController.move6 && destReached 
		    || NguiMovementController.move7 && !NguiMovementController.move8 && destReached 
		    || NguiMovementController.move9 && !NguiMovementController.move10 && destReached 
		    || NguiMovementController.move10 && !NguiMovementController.move11 && destReached
		    || NguiMovementController.move11 && !NguiMovementController.move12 && destReached 
		    || NguiMovementController.move12 && !NguiMovementController.move13 && destReached 
		    || NguiMovementController.move13 && !NguiMovementController.move14 && destReached 
		    || NguiMovementController.move14 && !NguiMovementController.move15 && destReached 
		    || NguiMovementController.move15 && !NguiMovementController.move16 && destReached 
		    || NguiMovementController.move16 && !NguiMovementController.move17 && destReached
		    || NguiMovementController.move17 && UseTutorial) {
			timerForSteps += Time.deltaTime;	
			if (timerForSteps >= timeForStep1) {
				FindNextStep();
				timerForSteps = 0;
			}
		}
		if (isMoving) {
			step = speed * Time.deltaTime;	
//			TutorialFish.transform.LookAt(destPosition);

			TutorialFish.transform.localPosition = Vector3.MoveTowards (TutorialFish.transform.localPosition, destPosition, step);

//			TutorialFish.transform.position += Vector3.forward * Time.deltaTime;
			float DistanceBetweenTheTwo = Vector3.Distance (TutorialFish.transform.localPosition, destPosition);
			if (DistanceBetweenTheTwo <= 20f) {
				isMoving = false;
				destReached = true;
				AdjustRotation();
				tutorialtext.SetActive (true);
				uS.spriteName = "Fishy_Talking_Anim_0021";
			}
		}
//		if (NguiMovementController.move3 && !NguiMovementController.move4) {
//			Vector3 PlanktonMiniMap = GameObject.Find ("Plankton").transform.position;
//			TutorialGlow.transform.position = PlanktonMiniMap;
//		}

	}
//	void UI2DSpriteAnimation.Play()	{
//	}

	void AdjustRotation (){
		TutorialFish.transform.localRotation = straightRotation;
//		spriteScale.x *= -1;
		TutorialFish.transform.localScale = spriteOriginalScale;

	}
	public void SkipTutorial (){
		UseTutorial = false;
		NGUITools.SetActive (HideButtonPlant,true);	
		NGUITools.SetActive (HideButtonHerbi,true);
		NGUITools.SetActive (HideButtonOmni,true);
		NGUITools.SetActive (HideButtonCarni,true);
		NGUITools.SetActive (HideButtonMiniMap, true);
		NGUITools.SetActive (PlayButton, true);
		TutorialFish.SetActive (false);
		TutorialGlow.SetActive (false);
		NGUITools.SetActive (SeaUrchinBalanceBarIcon, true);
		NGUITools.SetActive (SeaUrchinBalanceBar, true);
		NGUITools.SetActive (CoralBalanceBarIcon, true);
		NGUITools.SetActive (CoralBalanceBar, true);
		NGUITools.SetActive (SeaWeedBalanceBarIcon, true);
		NGUITools.SetActive (SeaWeedBalanceBar, true);
//		NGUITools.SetActive (CoralBalanceBarNeedle, true);
//		NGUITools.SetActive (SeaWeedBalanceBarNeedle, true);
//		NGUITools.SetActive (SeaUrchinBalanceBarNeedle, true);

	}

	void GoToSpawnBar (){
//		tP.from = TutorialFish.transform.position;
//		tP.to = atSpawnBar;
////		tP.Toggle ();
//		tP.PlayForward ();
		destPosition = atSpawnBar;
		float angleBetweenPositions =  Mathf.Atan2
			(TutorialFish.transform.localPosition.x - destPosition.x, 
			 TutorialFish.transform.localPosition.y - destPosition.y) * Mathf.Rad2Deg;
		
		//				Debug.Log ("angleBetweenPositions = " + angleBetweenPositions);
		if (TutorialFish.transform.localPosition.x - destPosition.x >= 0) {
			goingRight = true;
		} else {
			goingRight = false;
		}

		//instantiatedLineRenderer.transform.LookAt (target.transform.position);
		if (goingRight){
			TutorialFish.transform.eulerAngles = new Vector3 (
				TutorialFish.transform.rotation.x,
				TutorialFish.transform.rotation.y,
				-angleBetweenPositions + 90
				);
			TutorialFish.transform.localScale = spriteOriginalScale;
//			TutorialFish.transform.localScale -= TutorialFish.transform.localScale*2;
//			uS.spriteName = "Fishy_Swimming_Anim_0001";
//			TutorialFish.GetComponent<UISprite>().GetAtlasSprite();
//			TutorialFish.GetComponent<UISprite>().GetAtlasSprite(Fishy_Swimming_Anim_0001);
//			TutorialFish.UISprite.Sprite = "ThatSprite";
		} else {
			TutorialFish.transform.eulerAngles = new Vector3 (
				TutorialFish.transform.rotation.x,
				TutorialFish.transform.rotation.y,
				-angleBetweenPositions - 90
				);
//			spriteScale.x *= -1;
			TutorialFish.transform.localScale = spriteScale;
//			TutorialFish.GetComponent<UISprite>().GetAtlasSprite("Fishy_Swimming_Anim_0001");
		}

		destReached = false;
		isMoving = true;
		tutorialtext.SetActive (false);
		uS.spriteName = "Fishy_Swimming_Anim_0001";

	}

	void GoToMiniMap (){
//		tP.from = TutorialFish.transform.position;
//		tP.to = atMiniMap;
////		tP.Toggle ();
//		tP.PlayForward ();
		destPosition = atMiniMap;
		float angleBetweenPositions =  Mathf.Atan2
			(TutorialFish.transform.localPosition.x - destPosition.x, 
			 TutorialFish.transform.localPosition.y - destPosition.y) * Mathf.Rad2Deg;
		
		//				Debug.Log ("angleBetweenPositions = " + angleBetweenPositions);
		if (TutorialFish.transform.localPosition.x - destPosition.x >= 0) {
			goingRight = true;
		} else {
			goingRight = false;
		}
		
		//instantiatedLineRenderer.transform.LookAt (target.transform.position);
		if (goingRight){
			TutorialFish.transform.eulerAngles = new Vector3 (
				TutorialFish.transform.rotation.x,
				TutorialFish.transform.rotation.y,
				-angleBetweenPositions + 90
				);
			TutorialFish.transform.localScale = spriteOriginalScale;
//			uS.spriteName = "Fishy_Swimming_Anim_0001";
		} else {
			TutorialFish.transform.eulerAngles = new Vector3 (
				TutorialFish.transform.rotation.x,
				TutorialFish.transform.rotation.y,
				-angleBetweenPositions - 90
				);
			TutorialFish.transform.localScale = spriteScale;
//			uS.spriteName = "Fishy_Swimming_Anim_0001";
		}
		destReached = false;
		isMoving = true;
		tutorialtext.SetActive (false);
		uS.spriteName = "Fishy_Swimming_Anim_0001";
	}

	void GoToTheBar (){
//		tP.from = TutorialFish.transform.position;
//		tP.to = atTheBar;
////		tP.Toggle ();
//		tP.PlayForward ();
		destPosition = atTheBar;
		float angleBetweenPositions =  Mathf.Atan2
			(TutorialFish.transform.localPosition.x - destPosition.x, 
			 TutorialFish.transform.localPosition.y - destPosition.y) * Mathf.Rad2Deg;
		
		//				Debug.Log ("angleBetweenPositions = " + angleBetweenPositions);
		if (TutorialFish.transform.localPosition.x - destPosition.x >= 0) {
			goingRight = true;
		} else {
			goingRight = false;
		}
		
		//instantiatedLineRenderer.transform.LookAt (target.transform.position);
		if (goingRight){
			TutorialFish.transform.eulerAngles = new Vector3 (
				TutorialFish.transform.rotation.x,
				TutorialFish.transform.rotation.y,
				-angleBetweenPositions + 90
				);
//			spriteScale.x *= -1;
			TutorialFish.transform.localScale = spriteOriginalScale;
//			TutorialFish.transform.localScale -= TutorialFish.transform.localScale*2;
//			uS.spriteName = "Fishy_Swimming_Anim_0001";
		} else {
			TutorialFish.transform.eulerAngles = new Vector3 (
				TutorialFish.transform.rotation.x,
				TutorialFish.transform.rotation.y,
				-angleBetweenPositions - 90
				);
			TutorialFish.transform.localScale = spriteScale;
//			uS.spriteName = "Fishy_Swimming_Anim_0001";
		}
		destReached = false;
		isMoving = true;
		tutorialtext.SetActive (false);
		uS.spriteName = "Fishy_Swimming_Anim_0001";
	}

	void GoToPlayButton (){
//		tP.from = TutorialFish.transform.position;
//		tP.to = atPlayButton;
////		tP.Toggle ();
//		tP.PlayForward ();
		destPosition = atPlayButton;
		float angleBetweenPositions =  Mathf.Atan2
			(TutorialFish.transform.localPosition.x - destPosition.x, 
			 TutorialFish.transform.localPosition.y - destPosition.y) * Mathf.Rad2Deg;
		
		//				Debug.Log ("angleBetweenPositions = " + angleBetweenPositions);
		if (TutorialFish.transform.localPosition.x - destPosition.x >= 0) {
			goingRight = true;
		} else {
			goingRight = false;
		}
		
		//instantiatedLineRenderer.transform.LookAt (target.transform.position);
		if (goingRight){
			TutorialFish.transform.eulerAngles = new Vector3 (
				TutorialFish.transform.rotation.x,
				TutorialFish.transform.rotation.y,
				-angleBetweenPositions + 90
				);
			TutorialFish.transform.localScale = spriteOriginalScale;
		} else {
			TutorialFish.transform.eulerAngles = new Vector3 (
				TutorialFish.transform.rotation.x,
				TutorialFish.transform.rotation.y,
				-angleBetweenPositions - 90
				);
			TutorialFish.transform.localScale = spriteScale;
		}
		destReached = false;
		isMoving = true;
		tutorialtext.SetActive (false);
		uS.spriteName = "Fishy_Swimming_Anim_0001";
	}
	void GoToCenter (){
//		tP.from = TutorialFish.transform.position;
//		tP.to = atCenter;
////		tP.Toggle ();
//		tP.PlayForward ();
		destPosition = atCenter;
		float angleBetweenPositions =  Mathf.Atan2
			(TutorialFish.transform.localPosition.x - destPosition.x, 
			 TutorialFish.transform.localPosition.y - destPosition.y) * Mathf.Rad2Deg;
		
		//				Debug.Log ("angleBetweenPositions = " + angleBetweenPositions);
		if (TutorialFish.transform.localPosition.x - destPosition.x >= 0) {
			goingRight = true;
		} else {
			goingRight = false;
		}
		
		//instantiatedLineRenderer.transform.LookAt (target.transform.position);
		if (goingRight){
			TutorialFish.transform.eulerAngles = new Vector3 (
				TutorialFish.transform.rotation.x,
				TutorialFish.transform.rotation.y,
				-angleBetweenPositions + 90
				);
			TutorialFish.transform.localScale = spriteOriginalScale;
		} else {
			TutorialFish.transform.eulerAngles = new Vector3 (
				TutorialFish.transform.rotation.x,
				TutorialFish.transform.rotation.y,
				-angleBetweenPositions - 90
				);
			TutorialFish.transform.localScale = spriteScale;
		}
		destReached = false;
		isMoving = true;
		tutorialtext.SetActive (false);
		uS.spriteName = "Fishy_Swimming_Anim_0001";
	}
	void GoToCoinCounter (){
//		tP.from = TutorialFish.transform.position;
//		tP.to = atCoinCounter;
////		tP.Toggle ();
//		tP.PlayForward ();
		destPosition = atCoinCounter;
		float angleBetweenPositions =  Mathf.Atan2
			(TutorialFish.transform.localPosition.x - destPosition.x, 
			 TutorialFish.transform.localPosition.y - destPosition.y) * Mathf.Rad2Deg;
		
		//				Debug.Log ("angleBetweenPositions = " + angleBetweenPositions);
		if (TutorialFish.transform.localPosition.x - destPosition.x >= 0) {
			goingRight = true;
		} else {
			goingRight = false;
		}
		
		//instantiatedLineRenderer.transform.LookAt (target.transform.position);
		if (goingRight){
			TutorialFish.transform.eulerAngles = new Vector3 (
				TutorialFish.transform.rotation.x,
				TutorialFish.transform.rotation.y,
				-angleBetweenPositions + 90
				);
			TutorialFish.transform.localScale = spriteOriginalScale;
		} else {
			TutorialFish.transform.eulerAngles = new Vector3 (
				TutorialFish.transform.rotation.x,
				TutorialFish.transform.rotation.y,
				-angleBetweenPositions - 90
				);
			TutorialFish.transform.localScale = spriteScale;
		}
		destReached = false;
		isMoving = true;
		tutorialtext.SetActive (false);
		uS.spriteName = "Fishy_Swimming_Anim_0001";
	}

	public void FindNextStep (){

		if (!NguiMovementController.move1) {
			TutorialText.text = "This is the spawn bar, try clicking on the plankton.";
			GoToSpawnBar();
			NguiMovementController.move1 = true;
			Vector3 planktonbutton = GameObject.Find ("Plankton_Spawn_Button").transform.position;
			TutorialGlow.transform.position = planktonbutton;
			TutorialGlow.SetActive (true);
//			Vector3 planktonbutton = GameObject.Find ("Plankton_Spawn_Button").transform.position;
//			TutorialGlow.transform.position = planktonbutton;
		} else if (NguiMovementController.move1 && !NguiMovementController.move2){
			TutorialText.text = "This will show you how many of each type of species are in the game.";
			GoToTheBar ();
//			herbiHideButton.Toggle();
			NguiMovementController.move2 = true;
			Vector3 BarIndicators = GameObject.Find ("Plant_Bar_Holder").transform.position;
			TutorialGlowSprite.width = 200;
			TutorialGlowSprite.height = 60;
//			Vector3 BarIndicatorsScale = GameObject.Find ("Plant_Bar_Holder").transform.localScale;
			TutorialGlow.transform.position = BarIndicators;
//			TutorialGlow.transform.localScale = BarIndicatorsScale;

		} else if (NguiMovementController.move2 && !NguiMovementController.move3){
			TutorialText.text = "Here you will see what the spieces eats.";
			GoToMiniMap ();
			NguiMovementController.move3 = true;
			MinimapShowBar.Toggle();
			Vector3 MiniMap = GameObject.Find ("FoodChainWindow").transform.position;
			TutorialGlow.transform.localPosition = new Vector3 (400,200,0);
			TutorialGlowSprite.width = 220;
			TutorialGlowSprite.height = 262;

		} else if (NguiMovementController.move3 && !NguiMovementController.move4){
			TutorialText.text = "Now try to spawn anchovies.";
			GoToSpawnBar();
			Vector3 AnchovySpawnButton = GameObject.Find ("Anchovie_Spawn_Button").transform.position;
			TutorialGlow.transform.position = AnchovySpawnButton;
			TutorialGlowSprite.width = 100;
			TutorialGlowSprite.height = 100;
			NguiMovementController.move4 = true;
		} else if (NguiMovementController.move4 && !NguiMovementController.move5){
			TutorialText.text = "As you can see, anchovies eat plankton.";
			GoToMiniMap();
			NguiMovementController.move5 = true;
			Vector3 MiniMap = GameObject.Find ("FoodChainWindow").transform.position;
			TutorialGlow.transform.position = MiniMap;
			TutorialGlowSprite.width = 220;
			TutorialGlowSprite.height = 262;
		} else if (NguiMovementController.move5 && !NguiMovementController.move6){
			TutorialText.text = "When you hit the play button it will allow you to wath the spieces .";
			GoToPlayButton();
			NguiMovementController.move6 = true;
			NGUITools.SetActive (PlayButton, true);
			Vector3 playButton = PlayButton.transform.position;
			TutorialGlow.transform.position = playButton;
			TutorialGlowSprite.width = 230;
			TutorialGlowSprite.height = 124;
		} else if (NguiMovementController.move6 && !NguiMovementController.move7){
			TutorialText.text = "A treasure! Everytime you play a round you will be rewarded in coins based on species alive.";
			GoToTheBar();
			NguiMovementController.move7 = true;
			TutorialGlow.SetActive(false);
		} else if (NguiMovementController.move7 && !NguiMovementController.move8){
			TutorialText.text = "Try clicking the treasure!";
			GoToTheBar();
			NguiMovementController.move8 = true;
//			herbiShowBar.Toggle();
		} else if (NguiMovementController.move8 && !NguiMovementController.move9){
			TutorialText.text = "Looks like you got 3 coins! Congratulations!";
			GoToCoinCounter();
			NguiMovementController.move9 = true;
		} else if (NguiMovementController.move9 && !NguiMovementController.move10){
			TutorialText.text = "You use coins to buy species into your ecosystem.";
			GoToCoinCounter();
			NguiMovementController.move10 = true;
//			plantShowBar.Toggle();
		} else if (NguiMovementController.move10 && !NguiMovementController.move11){
			TutorialText.text = "But first you have to unlock them.";
			GoToSpawnBar();
			NguiMovementController.move11 = true;
		} else if (NguiMovementController.move11 && !NguiMovementController.move12){
			TutorialText.text = "You unlock them by balancing your active ones.";
			NguiMovementController.move12 = true;
			GoToSpawnBar();
		} else if (NguiMovementController.move12 && !NguiMovementController.move13){
//			TutorialText.text = "Try to achieve balance with plankton and anchovies.";
			TutorialText.text = "Use the balance bars to see when you are close to or have reached balance.";
			NguiMovementController.move13 = true;
			plantShowBar.Toggle ();
			herbiShowBar.Toggle();
			GoToTheBar ();
			TutorialGlow.SetActive (true);
			TutorialGlow.transform.localPosition = new Vector3 (-110,210,0);
			TutorialGlowSprite.width = 26;
			TutorialGlowSprite.height = 96;
		}else if (NguiMovementController.move13 && !NguiMovementController.move14){
//			TutorialText.text = "Use the balance bars to see when you are close to or have reached balance.";
			TutorialText.text = "The red on the bottom of the bar indicates that you have too little of that species.";
			NguiMovementController.move14 = true;
			GoToTheBar ();


		} else if (NguiMovementController.move14 && !NguiMovementController.move15){
//			TutorialText.text = "The red on the bottom of the bar indicates that you have too little of that specie.";
			TutorialText.text = "The red on the top of the bar indicates that you have too many of that species.";
			NguiMovementController.move15 = true;
			GoToTheBar ();

		} else if (NguiMovementController.move15 && !NguiMovementController.move16) {
//			TutorialText.text = "The red on the top of the bar indicates that you have too many of that specie.";
			TutorialText.text = "You'll need at least 2 of each species to achieve balance.";
			NguiMovementController.move16 = true;
			GoToSpawnBar ();
			TutorialGlow.SetActive(false);

		}else if (NguiMovementController.move16 && !NguiMovementController.move17){
//			TutorialText.text = "You'll need at least 2 of each specie to achieve balance.";
			TutorialText.text = "Try to achieve balance with plankton and anchovies.";
			NguiMovementController.move17 = true;
			GoToSpawnBar ();

		} else if (NguiMovementController.move17){
			SkipTutorial ();
		}

	}
}
