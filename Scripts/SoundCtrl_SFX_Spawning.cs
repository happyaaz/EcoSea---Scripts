using UnityEngine;
using System.Collections;

public class SoundCtrl_SFX_Spawning : MonoBehaviour {

	public AudioClip plankton_SpawnSound;
	public AudioClip anchovy_SpawnSound;
	public AudioClip coral_SpawnSound;
	public AudioClip seaUrchin_SpawnSound;
	public AudioClip seaweed_SpawnSound;
	public AudioClip crab_SpawnSound;
	public AudioClip barracuda_SpawnSound;





	public void Plankton_SpawnSound ()
	{
		audio.PlayOneShot (plankton_SpawnSound, 2f);
	}

	public void Anchovy_SpawnSound ()
	{
		audio.PlayOneShot (anchovy_SpawnSound, 1f);
	}

	public void Coral_SpawnSound ()
	{
		audio.PlayOneShot (coral_SpawnSound, 0.5f);
	}

	public void SeaUrchin_SpawnSound ()
	{
		audio.PlayOneShot (seaUrchin_SpawnSound, 0.05f);
	}

	public void Seaweed_SpawnSound ()
	{
		audio.PlayOneShot (seaweed_SpawnSound, 0.6f);
	}

	public void Crab_SpawnSound ()
	{
		audio.PlayOneShot (crab_SpawnSound, 0.1f);
	}

	public void Barracuda_SpawnSound ()
	{
		audio.PlayOneShot (barracuda_SpawnSound, 0.6f);
	}
}
