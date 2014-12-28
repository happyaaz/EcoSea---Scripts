using UnityEngine;
using System.Collections;

public class Projector_2DAni_WaterCTRL : MonoBehaviour {

	public float fps = 16.0f;
	public Texture2D[] frames;
	public Texture2D[] frames2;
	public Texture2D[] frames3;
	public Texture2D[] frames4;
	public Texture2D[] frames5;

	
	private int frameIndex;
	private Projector projector;
	
	void Start()
	{
		projector = GetComponent<Projector>();
		NextFrame();
		InvokeRepeating("NextFrame", 1 / fps, 1 / fps);
	}
	
	void NextFrame()
	{
		projector.material.SetTexture("_ShadowTex", frames5[frameIndex]);
		frameIndex = (frameIndex + 1) % frames5.Length;
	}
}
