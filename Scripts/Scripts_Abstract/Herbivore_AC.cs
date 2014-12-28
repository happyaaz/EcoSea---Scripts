using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public abstract class Herbivore_AC : SeaCreature {


	public override void Start () {

		base.Start ();
		//  should be unique for every fish - not for the archetype
		LongevityData ();
		
		maxAmountOfThisSpecies = CommonData.controllingNumberOfSpecies [this.tag];

	}
}
