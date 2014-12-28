using UnityEngine;
using System.Collections;

public class GoldMove : MonoBehaviour
{
	
	public static GameObject gold;
	public static Transform target;
	private static Transform originalPos;
	
	
	// Use this for initialization
	void Start ()
	{		
		gold = GameObject.Find ("ShinyGold");
		target = GameObject.Find ("targetGold").transform;
		originalPos = GameObject.Find ("OriginalGoldPosition").transform;
		gold.SetActive (false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		gold.transform.position = Vector3.MoveTowards (gold.transform.position, target.position, Time.deltaTime * 15);
		
	}
	
	public static void moveCoinsToCounter ()
	{
//		Debug.Log (gold.transform.position.ToString ());
		
		gold.SetActive (true);
		
		
	}
	
	public IEnumerator waitSomeTime ()
	{
		
		yield return new WaitForSeconds (5F);
	}
	
	public static void resetPositionCoins ()
	{
		
//		Debug.Log (gold.transform.position.ToString ());		
		gold.SetActive (false);
		gold.transform.position = originalPos.position;
//		Debug.Log (gold.transform.position.ToString ());
	}
}