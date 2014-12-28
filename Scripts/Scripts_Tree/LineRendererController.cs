using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class LineRendererController : MonoBehaviour {


	public List <GameObject> existingSpeciesInTheTree_list = new List <GameObject> ();

	public GameObject lineRendererCarnivorePrefab;
	public GameObject lineRendererOmnivorePrefab;
	public GameObject lineRendererHerbivorePrefab;
	public GameObject lineRendererPlantPrefab;

	public GameObject rendererHolder;


	public void DrawingRendererRunner () {

		//Debug.Log ("I am called");

		if (CommonData.tc_scr.existingSpeciesInTheScene.Count > 1)
		{
			//  we check every fish in the list
			foreach (string fishToLookAt in CommonData.tc_scr.existingSpeciesInTheScene) 
			{
				GameObject checkedObject = null;
				//  find target object in the list, since those objects represent that fish
				foreach (GameObject go in existingSpeciesInTheTree_list)
				{
					SpeciesNames sn = go.GetComponent <SpeciesNames> ();
				//	Debug.Log ("sn.nameOfSpecies = " + sn.nameOfSpecies);
					if (sn.nameOfSpecies == fishToLookAt)
					{
						checkedObject = sn.gameObject;
						FindTargetObjects (checkedObject, fishToLookAt);
						break;
					}
				}
			}
		}
	}


	void FindTargetObjects (GameObject startObject, string keyOfFish) {

		List <string> targetObjectsRepresentedAsStrings = CommonData.tc_scr.GetEatableFish (keyOfFish); 


		if (targetObjectsRepresentedAsStrings.Count > 0)
		{
			//Debug.Log (startObject.name + " eats that amount " + targetObjectsRepresentedAsStrings.Count);
			//Debug.Log ("StartObj = " + startObject.name);

			//  we take every existing object and check if the Start Object can eat it
			foreach (GameObject fishGo in existingSpeciesInTheTree_list) 
			{
				List <GameObject> targetObjectsRepresentedAsGameObject = new List <GameObject> ();
				//  get name
				string nameOfExistingFish = fishGo.GetComponent <SpeciesNames> ().nameOfSpecies;
				//  if this fish exists in the list
				//  then we add it!
				foreach (string eatableFish in targetObjectsRepresentedAsStrings)
				{
					if (eatableFish == nameOfExistingFish)
					{
						//Debug.Log ("Target = " + eatableFish);
						targetObjectsRepresentedAsGameObject.Add (fishGo);
					}
				}
				if (targetObjectsRepresentedAsGameObject.Count > 0)
				{
					//Debug.Log ("We call it! Number = " + targetObjectsRepresentedAsGameObject.Count);
					DrawLines (startObject, targetObjectsRepresentedAsGameObject);
					//targetObjectsRepresentedAsGameObject.Clear ();
				}
			}
		}

	}


	void DrawLines (GameObject startObj, List <GameObject> targetObjects) {

		// 1. no line renderer!
		// 2. Only NGUI textures
		// 3. Position them correctly later
		// 4. TreeObjects and Connections have to have the same parent!

		//  so. we know the object that eats something. in order to draw rays, we have to know the destination object. We go through the list of all the existing objects,
		//  then we add them to the list of objects, and then we draw rays
		foreach (GameObject target in targetObjects)
		{
			List <GameObject> existingLineRenderers = GameObject.FindGameObjectsWithTag ("LineRendererContainer").ToList ();
			//  we don't need to create the same link if it already exists
			if (! existingLineRenderers.Any (go => go.name == (startObj.name + "_" + target.name))) 
			{
				GameObject instantiatedLineRenderer = null;
				if (startObj.tag == "CarnivoreMinimap") 
				{
					instantiatedLineRenderer = NGUITools.AddChild (rendererHolder, lineRendererCarnivorePrefab) as GameObject;
				}
				else if (startObj.tag == "OmnivoreMinimap") 
				{
					instantiatedLineRenderer = NGUITools.AddChild (rendererHolder, lineRendererOmnivorePrefab) as GameObject;
				}
				else if (startObj.tag == "HerbivoreMinimap") 
				{
					instantiatedLineRenderer = NGUITools.AddChild (rendererHolder, lineRendererHerbivorePrefab) as GameObject;
				}
				else if (startObj.tag == "PlantMinimap") 
				{
					instantiatedLineRenderer = NGUITools.AddChild (rendererHolder, lineRendererPlantPrefab) as GameObject;
				}

				string nameOfLink = startObj.name + "_" + target.name;
				instantiatedLineRenderer.name = nameOfLink;

				//  position the vector exactly in the middle! Applies for both X and Y axises.
				Vector3 midpoint = (startObj.transform.localPosition - target.transform.localPosition) * 0.5f + target.transform.localPosition;
				instantiatedLineRenderer.transform.localPosition = midpoint;
				//  the line has to be rotated!

				//  don't know why, but this way we figure out the angle between two NGUI sprites, which are, in essence, just labels
				//  and then we get the negative value of it in some cases
				float angleBetweenPositions =  Mathf.Atan2
					(startObj.transform.localPosition.x - target.transform.localPosition.x, 
					 startObj.transform.localPosition.y - target.transform.localPosition.y) * Mathf.Rad2Deg;

//				Debug.Log ("angleBetweenPositions = " + angleBetweenPositions);

				//instantiatedLineRenderer.transform.LookAt (target.transform.position);
				instantiatedLineRenderer.transform.eulerAngles = new Vector3 (
					instantiatedLineRenderer.transform.rotation.x,
					instantiatedLineRenderer.transform.rotation.y,
					-angleBetweenPositions
					);
				//  and now the length =)
				float halfOfDistance = Vector3.Distance (startObj.transform.localPosition, target.transform.localPosition) / 2;
//				Debug.Log ("halfOfDistance = " + halfOfDistance + ", startObj.tag = " + startObj.tag);
				if (halfOfDistance < 40)
				{
					halfOfDistance += halfOfDistance * 0.1f;
				}
				else if (halfOfDistance < 50)
				{
					halfOfDistance += halfOfDistance * 0.2f;
				}
				else
				{
					halfOfDistance += halfOfDistance * 0.3f;
				}
				UISprite sprite = instantiatedLineRenderer.GetComponent <UISprite> ();
				sprite.height = (int) halfOfDistance;

				if (startObj.tag == "CarnivoreMinimap") 
				{
					instantiatedLineRenderer.transform.localPosition = new Vector3 (
						instantiatedLineRenderer.transform.localPosition.x,
						instantiatedLineRenderer.transform.localPosition.y,
						-15
						);
				}
				else if (startObj.tag == "OmnivoreMinimap") 
				{
					instantiatedLineRenderer.transform.localPosition = new Vector3 (
						instantiatedLineRenderer.transform.localPosition.x,
						instantiatedLineRenderer.transform.localPosition.y,
						-14
						);
				}
				else if (startObj.tag == "HerbivoreMinimap") 
				{
					instantiatedLineRenderer.transform.localPosition = new Vector3 (
						instantiatedLineRenderer.transform.localPosition.x,
						instantiatedLineRenderer.transform.localPosition.y,
						-13
						);
				}
				else if (startObj.tag == "PlantMinimap") 
				{
					instantiatedLineRenderer.transform.localPosition = new Vector3 (
						instantiatedLineRenderer.transform.localPosition.x,
						instantiatedLineRenderer.transform.localPosition.y,
						-12
						);
				}


			}
		}
	}


	public void RemovingObjectFromTree (string removeableObject) {

//		Debug.Log ("I have to destroy " + removeableObject);
		if (existingSpeciesInTheTree_list.Any (go => go.name == removeableObject))
		{
			GameObject go = existingSpeciesInTheTree_list.Find (gogo => gogo.name == removeableObject);
			existingSpeciesInTheTree_list.Remove (go);
			RespectiveSpawner rs_scr = go.GetComponent <RespectiveSpawner> ();
			if (go.tag.Contains ("Carnivore"))
			{
				CommonData.tc_scr.minimapSpawnerCarni.Add (rs_scr.respectiveSpawner);
			}
			else if (go.tag.Contains ("Omnivore"))
			{
				CommonData.tc_scr.minimapSpawnerOmni.Add (rs_scr.respectiveSpawner);
			}
			else if (go.tag.Contains ("Herbivore"))
			{
				CommonData.tc_scr.minimapSpawnerHerbi.Add (rs_scr.respectiveSpawner);
			}
			else if (go.tag.Contains ("Plant"))
			{
				CommonData.tc_scr.minimapSpawnerPlant.Add (rs_scr.respectiveSpawner);
			}
			Destroy (go);
		}
	}


	public void RemovingConnections (string fish) {

		//  remove connection with every fish this one eats.
		List <string> fishThisOneEats = CommonData.tc_scr.GetEatableFish (fish);

		if (fishThisOneEats.Count > 0)
		{
			foreach (string food in fishThisOneEats)
			{
				GameObject connectionToDestroy = GameObject.Find (fish + "_" + food);
				if (connectionToDestroy != null)
				{
					Destroy (connectionToDestroy);
				}
			}
		}

		//  then remove all the connections with the fish that feeds on the removed one

		List <GameObject> lineRenderers = GameObject.FindGameObjectsWithTag ("LineRendererContainer").ToList ();

		foreach (GameObject lr in lineRenderers)
		{
			if (lr.name.Contains ("_" + fish))
			{
				Destroy (lr);
			}
		}
	}
}
