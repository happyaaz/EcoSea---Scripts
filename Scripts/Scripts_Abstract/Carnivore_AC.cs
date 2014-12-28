using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public abstract class Carnivore_AC : SeaCreature {


	public override void Start () {

		base.Start ();
		LongevityData ();
		
		maxAmountOfThisSpecies = CommonData.controllingNumberOfSpecies [this.tag];


	}
}
