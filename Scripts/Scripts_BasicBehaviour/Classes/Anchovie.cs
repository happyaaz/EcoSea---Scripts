using UnityEngine;
using System.Collections;

public class Anchovie : Herbivore_AC {
	
	public float speed;
	
	
	public override void Start () {
		
		Speed_prop = speed;
		SpeedInit_prop = speed;
		base.Start ();
		
		ReprAndEat.checkIfItsTimeToDie += CheckIfItsTimeToDie;
		ReprAndEat.HerbivoresAreGoingToEat += lff_scr.LookingForSpeciesToEat;
		ReprAndEat.HappyReproducing += repr_scr.ChoosingMate;
		InvokeRepeating ("CheckIfOutside", 5, 7);
	}


	
	void OnDestroy () {
		
		ReprAndEat.checkIfItsTimeToDie -= CheckIfItsTimeToDie;ReprAndEat.HappyReproducing -= repr_scr.ChoosingMate;
		ReprAndEat.HerbivoresAreGoingToEat -= lff_scr.LookingForSpeciesToEat;
	}
}
