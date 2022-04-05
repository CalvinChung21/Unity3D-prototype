using UnityEngine;
using System.Collections;




public class Flashlight_PRO : MonoBehaviour 
{
	[Space(10)]
	[SerializeField()] GameObject Lights; // all light effects and spotlight
	[SerializeField()] AudioSource switch_sound; // audio of the switcher
	
	private Light spotlight;
	private bool is_enabled = false;

	// Use this for initialization
	void Start () 
	{
	}
	
	/// <summary>
	/// changes the intensivity of lights from 0 to 100.
	/// call this from other scripts.
	/// </summary>
	public void Change_Intensivity(float percentage)
	{
		percentage = Mathf.Clamp (percentage, 0, 100);


		spotlight.intensity = (8 * percentage) / 100;
	}
	
	/// <summary>
	/// switch current state  ON / OFF.
	/// call this from other scripts.
	/// </summary>
	public void Switch()
	{
		is_enabled = !is_enabled; 

		Lights.SetActive (is_enabled);

		if (switch_sound != null)
			switch_sound.Play ();
	}
}
