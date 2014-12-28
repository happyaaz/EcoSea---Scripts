using UnityEngine;
using System.Collections;

public class TreasureChest : MonoBehaviour {
	//variables for the random chest
	public GameObject chest_Master;
	private GameObject chest_Pref;
	private GameObject chestClone;
	private GameObject coins_Par;
	private GameObject coins_Flare;

	//variables for the GUI chest
	public GameObject Chest_GUI_Pref;
	private GameObject coinsGUI_Flare;

	//the different ways of spawning the chest
//	public bool chestEveryPlay = false;
	public bool chestRandomSpawn = true;

	//bool to toggle automatic opening and having to click the chest
	public bool chestAutomaticOpens = false;

	private bool chestDestroyed = true;


	//timer variables to spawn chest at random intervals
	private int nextActionTime;
	private int period;
	public int minTime = 30;
	public int maxTime = 60;

	//the coins that our randomly spawning chest gives 
	private int coinsForRandomChest = 20;

	private bool chestEffectsOn = false;

	public AudioClip coinsSound;

	Tutorial_Ctrl nMC;



	// Use this for initialization
	void Start () {
		coinsGUI_Flare = GameObject.FindGameObjectWithTag ("CoinsGUI_Flare");
		coinsGUI_Flare.SetActive (false);
		Chest_GUI_Pref.SetActive (false);

		nextActionTime = Random.Range ( minTime, maxTime);
		period = Random.Range ( minTime, maxTime);
		nMC = Camera.main.GetComponent<Tutorial_Ctrl> ();
	}

	void Update ()
	{

		if (chestRandomSpawn == true)
		{
			if (Time.time > nextActionTime) 
			{

				nextActionTime += period;
				if (chestDestroyed == true)
				{
					InstantiateTreasureChest();
					chestDestroyed = false;
				}
				period = Random.Range ( minTime, maxTime);
			}
		}
	}
	
	void OnClick()
	{
		//if not collected, destroy the chest
		if(chestDestroyed == false)
		{
			Destroy (chestClone);
			chestDestroyed = true;
		}
	}
	//To place the chest on the ocean floor
	void Placement () {
		
		RaycastHit[] hits;
		hits = Physics.RaycastAll(transform.position, Vector3.down, 100.0F);
		int i = 0;
		while (i < hits.Length) 
		{
			RaycastHit hit = hits[i];
			if (hit.transform.tag == "OceanFloor") 
			{
				chestClone.transform.position = new Vector3 (chestClone.transform.position.x, 
				                                       hit.point.y, 
				                                             chestClone.transform.position.z);
				break;
			}
			i++;
		}
	}

	//instantiate the treasure chest and play animation
	public void InstantiateTreasureChest()
	{
		if (chestDestroyed == true)
		{
			if (chestRandomSpawn == true)
			{
				chestClone = Instantiate (chest_Master, CommonFunctions.RandomPositionWhenThrowingIn (), chest_Master.transform.rotation) as GameObject;
				Placement ();
				StartCoroutine (TreasureChestAppearingEffects ());
			} else {
				chestClone = Instantiate (chest_Master, chest_Master.transform.position, chest_Master.transform.rotation) as GameObject;
				StartCoroutine (TreasureChestAppearingEffects ());
			}
		}
	}

	public IEnumerator TreasureChestAppearingEffects ()
	{
		chest_Pref = GameObject.FindGameObjectWithTag ("Chest_Pref");
		chest_Pref.SetActive (false);
		
		yield return new WaitForSeconds (0.5f);
		
		chest_Pref.SetActive (true);
		chestDestroyed = false;
		coins_Par = GameObject.FindGameObjectWithTag ("Coins_Par");
		coins_Flare = GameObject.FindGameObjectWithTag ("Coins_Flare");
		coins_Par.SetActive (false);
		coins_Flare.SetActive (false);

		if (chestAutomaticOpens == true)
		{
			StartCoroutine(CollectRandomTreasureChest());
		}
	}

	//Destroy random treasure chest when clicked on and getting points
	public IEnumerator CollectRandomTreasureChest()
	{
		if (chestEffectsOn == false)
		{
			chestEffectsOn = true;
			//the coins
			if (chestRandomSpawn == true)
			{
				GUI_Main.AvailablePoints += coinsForRandomChest;
				HUDText RecievedPoints= GetComponent<HUDText> ();
				RecievedPoints.Add (coinsForRandomChest + " Coins!" ,new Color32 (22,214,86,0), 2.0f);
			}

			chest_Pref.animation.Play ("Chest_Open");
			coins_Par.SetActive (true);
			coins_Flare.SetActive (true);
			audio.PlayOneShot (coinsSound, 0.4f);

			yield return new WaitForSeconds (3.0f);

			coins_Flare.SetActive (false);
			chest_Pref.animation.Play ("Chest_Close");
			yield return new WaitForSeconds (1.0f);
			chest_Pref.animation.Stop ();

			Destroy (chestClone);
			chestDestroyed = true;
			chestEffectsOn = false;
		}
	}
	//Destroy random treasure chest when clicked on and getting points
	public IEnumerator CollectGUITreasureChest()
	{
//		Debug.Log ("collect GUI is run");
		if (chestEffectsOn == false)
		{
			if (!Tutorial_Ctrl.UseTutorial || Tutorial_Ctrl.move8){
				chestEffectsOn = true;

				//the coins		
				GUI_Main.AvailablePoints += ReprAndEat.totalPoints_int;
				HUDText RecievedPoints= GetComponent<HUDText> ();
				RecievedPoints.Add ("You Received " + ReprAndEat.totalPoints_int + " for all the living Creatures",new Color32 (22,214,86,0), 2.0f);
		
				Chest_GUI_Pref.animation.Play ("Chest_Open");
				coinsGUI_Flare.SetActive (true);
				audio.PlayOneShot (coinsSound, 0.7f);
				GoldMove.moveCoinsToCounter();
				FlashCoinLabel.startToFlash();
				yield return new WaitForSeconds (3.0f);
				
				coinsGUI_Flare.SetActive (false);
				Chest_GUI_Pref.animation.Play ("Chest_Close");
				yield return new WaitForSeconds (1.0f);
				Chest_GUI_Pref.animation.Stop ();

				Chest_GUI_Pref.SetActive (false);

				chestEffectsOn = false;
				GoldMove.resetPositionCoins();
				FlashCoinLabel.stopFlashing();
				if (Tutorial_Ctrl.UseTutorial && !Tutorial_Ctrl.move9){
					nMC.FindNextStep();
				}
			}
		}
	}

}
