using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class CountingExistingFish : MonoBehaviour {

	//archetypes
	public int numberOfHerbivores;
	public int numberOfPlants;
	public int numberofCarnivores;
	public int numberOfOmnivores;

	//species
	//plants
	public int numberOfPlankton;
	public int numberOfCorals;
	public int numberOfSeaWeed;
	//herbivores
	public int numberOfAnchovies;
	public int numberOfSeaUrchins;
	//omnivores
	public int numberOfCrabs;
	//carnivores
	public int numberOfBarracudas;







	public void CountNumberOfExistingFish (string tagToBeDecreaseByOne = "") {

		//archetypes
		if (tagToBeDecreaseByOne == string.Empty)
		{
			CountNumberOfPlants ();
			CountNumberOfHerbivores ();
			CountNumberOfOmnivores ();
			CountNumberOfCarnivores ();
		}
		else
		{
			if (tagToBeDecreaseByOne == "Plant")
			{
				CountNumberOfPlants (true);
			}
			else
			{
				CountNumberOfPlants ();
			}

			if (tagToBeDecreaseByOne == "Herbivore")
			{
				CountNumberOfHerbivores (true);
			}
			else
			{
				CountNumberOfHerbivores ();
			}

			if (tagToBeDecreaseByOne == "Omnivore")
			{
				CountNumberOfOmnivores (true);
			}
			else
			{
				CountNumberOfOmnivores ();
			}

			if (tagToBeDecreaseByOne == "Carnivore")
			{
				CountNumberOfCarnivores (true);
			}
			else
			{
				CountNumberOfCarnivores ();
			}
		}
		//species alive

		CountNumberOfPlankton ();
		CountNumberOfCorals ();
		CountNumberOfSeaWeed ();
		CountNumberOfAnchovies ();
		CountNumberOfSeaUrchins ();
		CountNumberOfCrabs ();
		CountNumberOfBarracudas ();



	}

	public void CountNumberOfHerbivores (bool isDecreased = false) {

		numberOfHerbivores = GameObject.FindGameObjectsWithTag ("Herbivore").ToList ().Count;
		if (isDecreased == true)
		{
			numberOfHerbivores --;
		}
	}


	public void CountNumberOfPlants (bool isDecreased = false) {

		numberOfPlants = GameObject.FindGameObjectsWithTag ("Plant").ToList ().Count;
		if (isDecreased == true)
		{
			numberOfPlants --;
		}
	}


	public void CountNumberOfCarnivores (bool isDecreased = false)  {

		numberofCarnivores = GameObject.FindGameObjectsWithTag ("Carnivore").ToList ().Count;
		if (isDecreased == true)
		{
			numberofCarnivores --;
		}
	}


	public void CountNumberOfOmnivores (bool isDecreased = false) {

		numberOfOmnivores = GameObject.FindGameObjectsWithTag ("Omnivore").ToList ().Count;
		if (isDecreased == true)
		{
			numberOfOmnivores --;
		}
	}


	public void CountNumberOfPlankton () {
		numberOfPlankton = 0;
		List <GameObject> Plankton = new List<GameObject> ();
		Plankton = GameObject.FindGameObjectsWithTag ("Plant").ToList ();
		
		foreach (GameObject obj in Plankton) {
			if (obj.name.Contains ("Plankton")) {
				numberOfPlankton ++;
			}
		}
	}

	public void CountNumberOfCorals (){
		numberOfCorals = 0;
		List <GameObject> Coral = new List<GameObject> ();
		Coral = GameObject.FindGameObjectsWithTag ("Plant").ToList ();
		
		foreach (GameObject obj in Coral) {
			if (obj.name.Contains ("Coral")) {
				numberOfCorals ++;
			}
		}
	}

	public void CountNumberOfSeaWeed (){
		numberOfSeaWeed = 0;
		List <GameObject> SeaWeed = new List<GameObject> ();
		SeaWeed = GameObject.FindGameObjectsWithTag ("Plant").ToList ();
		
		foreach (GameObject obj in SeaWeed) {
			if (obj.name.Contains ("SeaWeed")) {
				numberOfSeaWeed ++;
			}
		}
	}

	public void CountNumberOfAnchovies () {
		numberOfAnchovies = 0;
		List <GameObject> Anchovies = new List<GameObject> ();
		Anchovies = GameObject.FindGameObjectsWithTag ("Herbivore").ToList ();
		
		foreach (GameObject obj in Anchovies) {
			if (obj.name.Contains ("Anchovie")) {
				numberOfAnchovies ++;
			}
		}
	}

	public void CountNumberOfSeaUrchins () {
		numberOfSeaUrchins = 0;
		List <GameObject> SeaUrchins = new List<GameObject> ();
		SeaUrchins = GameObject.FindGameObjectsWithTag ("Herbivore").ToList ();
		
		foreach (GameObject obj in SeaUrchins) {
			if (obj.name.Contains ("SeaUrchin")) {
				numberOfSeaUrchins ++;
			}
		}
	}

	public void CountNumberOfCrabs () {
		numberOfCrabs = 0;
		List <GameObject> Crabs = new List<GameObject> ();
		Crabs = GameObject.FindGameObjectsWithTag ("Omnivore").ToList ();
		
		foreach (GameObject obj in Crabs) {
			if (obj.name.Contains ("Crab")) {
				numberOfCrabs ++;
			}
		}
	}

	public void CountNumberOfBarracudas () {
		numberOfBarracudas = 0;
		List <GameObject> Barracudas = new List<GameObject> ();
		Barracudas = GameObject.FindGameObjectsWithTag ("Carnivore").ToList ();
		
		foreach (GameObject obj in Barracudas) {
			if (obj.name.Contains ("Barracuda")) {
				numberOfBarracudas ++;
			}
		}
	}
}
