using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepAndQuit : MonoBehaviour {
	// Use this for initialization
	void Start () {
		System.DateTime Now = System.DateTime.Now;
		PlayerPrefs.SetInt("A_Desc", Now.Year);
		PlayerPrefs.SetInt("Me_Desc", Now.Month);
		PlayerPrefs.SetInt("D_Desc", Now.Day);
		PlayerPrefs.SetInt("H_Desc", Now.Hour);
		PlayerPrefs.SetInt("Mi_Desc", Now.Minute);
		PlayerPrefs.SetInt("S_Desc", Now.Second);
		PlayerPrefs.SetInt("dormirEnCama", 1);
		StartCoroutine(Sleep());
	}
	IEnumerator Sleep()
	{
		yield return new WaitForSeconds(2);		
		Application.Quit();
	}
}
