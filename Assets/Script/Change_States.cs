using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Change_States : MonoBehaviour {

	int contComida=5, contBaño=5, contAseo=5, contSalud=100, contSueño=1, contFelicidad=100;
	int tempComida, tempBaño, tempAseo, tempFelicidad, tempSueño;
	//en Minutos
	int promedioComida = 9000, promedioBaño = 10800, promedioAseo = 21600, promedioSueño=20;
	bool AnimationState = false;
	public GameObject ic_Comida, ic_Baño, ic_Aseo, ic_Felicidad, ic_Sueño, ic_Salud, ic_Alerta;
	public Sprite C_5,C_4,C_3,C_2,C_1;
	Animator Alerta;

	void Awake()
	{
		Alerta = ic_Alerta.GetComponent<Animator>();
	}
	void Alertar()
	{		
		if ((contComida == 1 || contBaño == 1 || contAseo == 1 || contSueño == 1) && !AnimationState)
		{			
			Alerta.SetTrigger("Open");	
			Debug.Log("Entro a True");
			AnimationState = true;		
		}
		if (contComida > 1 && contBaño > 1 && contAseo > 1 && contSueño > 1 && AnimationState) {			
			Alerta.SetTrigger("Open");
			Debug.Log("Entro a False");
			AnimationState = false;
		}
	}
	public void calcularComida()
	{
		System.DateTime ultima = new System.DateTime( PlayerPrefs.GetInt("A_rC"), 
													  PlayerPrefs.GetInt("Me_rC"),
													  PlayerPrefs.GetInt("D_rC"),
													  PlayerPrefs.GetInt("H_rC"),
													  PlayerPrefs.GetInt("Mi_rC"),
													  0);
		System.DateTime Now = System.DateTime.Now;
		System.TimeSpan tiempoEntre = Now - ultima;
		int restarContador = Mathf.RoundToInt((float)(tiempoEntre.TotalSeconds) / promedioComida);	
		if(restarContador>4){
			restarContador=4;
		}	
		contComida-= restarContador;
		int restarTemporizador = Mathf.RoundToInt((float)tiempoEntre.TotalSeconds);		
		while(restarTemporizador > 0) {
			restarTemporizador-=promedioComida;
		}
		tempComida = (restarTemporizador + promedioComida) ;		
		ChangeComida();
		StartCoroutine(WaitComida());
	}

	public void calcularBaño()
	{		
		System.DateTime ultima = new System.DateTime( PlayerPrefs.GetInt("A_rB"), 
													  PlayerPrefs.GetInt("Me_rB"),
													  PlayerPrefs.GetInt("D_rB"),
													  PlayerPrefs.GetInt("H_rB"),
													  PlayerPrefs.GetInt("Mi_rB"),
													  0);
		System.DateTime Now = System.DateTime.Now;
		System.TimeSpan tiempoEntre = Now - ultima;
		int restarContador = Mathf.RoundToInt((float)(tiempoEntre.TotalSeconds) / promedioBaño);
		if(restarContador>4){
			restarContador=4;
		}
		contBaño-= restarContador;
		int restarTemporizador = Mathf.RoundToInt((float)tiempoEntre.TotalSeconds);
		while(restarTemporizador > 0) {
			restarTemporizador-=promedioBaño;
		}
		tempBaño = (restarTemporizador + promedioBaño);
		//Debug.Log("restarContador " + restarContador);
		//Debug.Log("restarTemporizador " + restarTemporizador);
		//Debug.Log("tempBaño " + tempBaño);
		ChangeBaño();
		StartCoroutine(WaitBaño());
	}

	public void calcularAseo()
	{
		System.DateTime ultima = new System.DateTime( PlayerPrefs.GetInt("A_rA"), 
													  PlayerPrefs.GetInt("Me_rA"),
													  PlayerPrefs.GetInt("D_rA"),
													  PlayerPrefs.GetInt("H_rA"),
													  PlayerPrefs.GetInt("Mi_rA"),
													  0);
		System.DateTime Now = System.DateTime.Now;
		System.TimeSpan tiempoEntre = Now - ultima;
		int restarContador = Mathf.RoundToInt((float)(tiempoEntre.TotalSeconds) / promedioAseo);
		if(restarContador>4){
			restarContador=4;
		}
		contAseo-= restarContador;
		int restarTemporizador = Mathf.RoundToInt((float)tiempoEntre.TotalSeconds);
		while(restarTemporizador > 0) {
			restarTemporizador-=promedioAseo;
		}
		tempAseo = (restarTemporizador + promedioAseo);
		ChangeAseo();
		StartCoroutine(WaitAseo());
	}

	public void calcularSueño()
	{
		contSueño = PlayerPrefs.GetInt("Sueño");
		Debug.Log("contSueño: " + contSueño);
		Debug.Log("PlayerPrefSueño: " + PlayerPrefs.GetInt("Sueño"));
		System.DateTime ultima = new System.DateTime( PlayerPrefs.GetInt("A_Desc"), 
													  PlayerPrefs.GetInt("Me_Desc"),
													  PlayerPrefs.GetInt("D_Desc"),
													  PlayerPrefs.GetInt("H_Desc"),
													  PlayerPrefs.GetInt("Mi_Desc"),
													  PlayerPrefs.GetInt("S_Desc"));
		System.DateTime Now = System.DateTime.Now;
		System.TimeSpan tiempoEntre = Now - ultima;
		int multiplicadorSueño = 1;
		if (PlayerPrefs.GetInt("dormirEnCama") == 1)
		{
			multiplicadorSueño = 10;
			Debug.Log("Si durmio en la cama");
		}
		int restarContador = Mathf.RoundToInt((float)(tiempoEntre.TotalSeconds) / (promedioSueño/multiplicadorSueño));		
		contSueño+= restarContador;
		Debug.Log("restarContador: " + restarContador);
		if(contSueño>5){
			contSueño=5;
		}
		tempSueño = promedioSueño;
		Debug.Log("Post-contSueño: " + contSueño);
		ChangeSueño();
		StartCoroutine(WaitSueño());
	}

	public void Sueño()
	{
		Debug.Log("Entro a guardar el sueño de" + contSueño);
		PlayerPrefs.SetInt("Sueño",contSueño);
	}

	void reFillComida()
	{
		contComida=5;
		ChangeComida();
		System.DateTime reinicio = System.DateTime.Now;
			PlayerPrefs.SetInt("A_rC",reinicio.Year);
			PlayerPrefs.SetInt("Me_rC",reinicio.Month);
			PlayerPrefs.SetInt("D_rC",reinicio.Day);
			PlayerPrefs.SetInt("H_rC",reinicio.Hour);
			PlayerPrefs.SetInt("Mi_rC",reinicio.Minute);
			ChangeComida();
		ChangeFelicidad();
		Alertar();
	}
	void reFillBaño()
	{
		contBaño=5;
		System.DateTime reinicio = System.DateTime.Now;
			PlayerPrefs.SetInt("A_rB",reinicio.Year);
			PlayerPrefs.SetInt("Me_rB",reinicio.Month);
			PlayerPrefs.SetInt("D_rB",reinicio.Day);
			PlayerPrefs.SetInt("H_rB",reinicio.Hour);
			PlayerPrefs.SetInt("Mi_rB",reinicio.Minute);
			ChangeBaño();
		ChangeFelicidad();
		Alertar();
	}
	void reFillAseo()
	{
		contAseo=5;
		System.DateTime reinicio = System.DateTime.Now;
			PlayerPrefs.SetInt("A_rA",reinicio.Year);
			PlayerPrefs.SetInt("Me_rA",reinicio.Month);
			PlayerPrefs.SetInt("D_rA",reinicio.Day);
			PlayerPrefs.SetInt("H_rA",reinicio.Hour);
			PlayerPrefs.SetInt("Mi_rA",reinicio.Minute);
			ChangeAseo();
		ChangeFelicidad();
		Alertar();
	}

	void ChangeComida()
	{
		switch (contComida)
		{
			case 5:
				ic_Comida.GetComponent<Image>().sprite = C_5;
				contSalud+=25;
				break;
			case 4:
				ic_Comida.GetComponent<Image>().sprite = C_4;
				contFelicidad-=4;
				break;
			case 3:
				ic_Comida.GetComponent<Image>().sprite = C_3;
				contFelicidad-=6;
				break;
			case 2:
				ic_Comida.GetComponent<Image>().sprite = C_2;
				contFelicidad-=9;				
				break;
			case 1:
				ic_Comida.GetComponent<Image>().sprite = C_1;
				contFelicidad-=14;
				contSalud-=4;
				Alertar();
				if(contFelicidad<0)
				{
					contFelicidad=0;
				}
				break;
		}
		ChangeFelicidad();
	}

	void ChangeBaño()
	{
		switch (contBaño)
		{
			case 5:
				ic_Baño.GetComponent<Image>().sprite = C_5;
				contSalud+=25;
				break;
			case 4:
				ic_Baño.GetComponent<Image>().sprite = C_4;
				contFelicidad-=4;
				break;
			case 3:
				ic_Baño.GetComponent<Image>().sprite = C_3;
				contFelicidad-=5;
				break;
			case 2:
				ic_Baño.GetComponent<Image>().sprite = C_2;
				contFelicidad-=9;
				break;
			case 1:
				ic_Baño.GetComponent<Image>().sprite = C_1;
				contFelicidad-=14;
				contSalud-=4;
				Alertar();
				if(contFelicidad<0)
				{
					contFelicidad=0;
				}
				break;
		}
		ChangeFelicidad();
	}

	void ChangeAseo()
	{
		switch (contAseo)
		{
			case 5:
				ic_Aseo.GetComponent<Image>().sprite = C_5;
				contSalud+=25;
				break;
			case 4:
				ic_Aseo.GetComponent<Image>().sprite = C_4;
				contFelicidad-=4;
				break;
			case 3:
				ic_Aseo.GetComponent<Image>().sprite = C_3;
				contFelicidad-=6;
				break;
			case 2:
				ic_Aseo.GetComponent<Image>().sprite = C_2;
				contFelicidad-=9;
				break;
			case 1:
				ic_Aseo.GetComponent<Image>().sprite = C_1;
				contFelicidad-=14;
				contSalud-=4;
				Alertar();
				if(contFelicidad<0)
				{
					contFelicidad=0;
				}
				break;
		}
		ChangeFelicidad();
	}

	void ChangeSueño()
	{
		switch (contSueño)
		{
			case 5:
				ic_Sueño.GetComponent<Image>().sprite = C_5;
				contSalud+=25;
				break;
			case 4:
				ic_Sueño.GetComponent<Image>().sprite = C_4;
				contFelicidad-=4;
				break;
			case 3:
				ic_Sueño.GetComponent<Image>().sprite = C_3;
				contFelicidad-=6;
				break;
			case 2:
				ic_Sueño.GetComponent<Image>().sprite = C_2;
				contFelicidad-=9;
				break;
			case 1:
				ic_Sueño.GetComponent<Image>().sprite = C_1;
				Alertar();
				contFelicidad-=14;
				contSalud-=4;
				if(contFelicidad<0)
				{
					contFelicidad=0;
				}
				break;
		}
		ChangeFelicidad();
	}

	void ChangeFelicidad()
	{
		if (contFelicidad>90)
		{
			ic_Felicidad.GetComponent<Image>().sprite = C_5;
			contSalud+=25;
		}else{
			if (contFelicidad <= 90 && contFelicidad >=65)
			{
				ic_Felicidad.GetComponent<Image>().sprite = C_4;
			}else {
				if (contFelicidad < 65 && contFelicidad >= 35)
				{
				ic_Felicidad.GetComponent<Image>().sprite = C_3;
				}else{
					if (contFelicidad < 35 && contFelicidad >= 10)
					{
						ic_Felicidad.GetComponent<Image>().sprite = C_2;
					}else{
						if (contFelicidad < 10)
						{
							ic_Felicidad.GetComponent<Image>().sprite = C_1;
							contSalud-=10;
						}
					} 
				} 
			}
		} 
		ChangeSalud();			
	}	

	void ChangeSalud()
	{
		if (contSalud>90)
		{
			ic_Salud.GetComponent<Image>().sprite = C_5;
		}else{
			if (contSalud <= 90 && contSalud >=65)
			{
				ic_Salud.GetComponent<Image>().sprite = C_4;
			}else {
				if (contSalud < 65 && contSalud >= 35)
				{
				ic_Salud.GetComponent<Image>().sprite = C_3;
				}else{
					if (contSalud < 35 && contSalud >= 10)
					{
						ic_Salud.GetComponent<Image>().sprite = C_2;
					}else{
						if (contSalud < 10)
						{
							ic_Salud.GetComponent<Image>().sprite = C_1;
						}
					} 
				} 
			}
		} 			
	}	

	IEnumerator WaitComida()
	{
		yield return new WaitForSeconds(tempComida);	
		if (contComida > 1)
		{
			contComida--;
		}
		ChangeComida();
		tempComida = promedioComida;
		Debug.Log("Comida:" + contComida);
		StartCoroutine(WaitComida());		
	}
	IEnumerator WaitBaño()
	{
		yield return new WaitForSeconds(tempBaño);		
		if (contBaño > 1)
		{
			contBaño--;
		}
		ChangeBaño();
		tempBaño = promedioBaño;
		Debug.Log("Baño:" + contBaño);
		StartCoroutine(WaitBaño());		
	}
	IEnumerator WaitAseo()
	{
		yield return new WaitForSeconds(tempAseo);		
		if (contAseo > 1)
		{
			contAseo--;
		}
		ChangeAseo();
		tempAseo = promedioAseo;
		Debug.Log("Aseo:" + contAseo);
		StartCoroutine(WaitAseo());	
	}
	IEnumerator WaitSueño()
	{
		yield return new WaitForSeconds(tempSueño);		
		if (contSueño > 1)
		{
			contSueño--;
		}
		ChangeSueño();
		tempSueño = promedioSueño;
		Debug.Log("Sueño:" + contSueño);
		StartCoroutine(WaitSueño());	
	}

	public void Alimentar()
	{
		if (contComida!=5)
		{
			contComida = 5;
			contFelicidad+=20;
			LimitFelicidad();
		}	
		reFillComida();	
	}

	public void Limpiar()
	{
		if (contBaño!=5)
		{
			contBaño = 5;
			contFelicidad+=20;
			LimitFelicidad();
		}	
		reFillBaño();
	}

	public void Bañar()
	{
		if (contAseo!=5)
		{
			contAseo = 5;
			contFelicidad+=20;
			LimitFelicidad();
		}	
		reFillAseo();
	}

	public void Jugar()
	{		
		contFelicidad+=10;
		contSueño-=1;
		if(contSueño <= 0)
		{
			contSueño=1;
		}
		LimitFelicidad();
		ChangeSueño();
		ChangeFelicidad();
	}

	void LimitFelicidad()
	{
		if(contFelicidad>100)
		{
			contFelicidad=100;
		}
		if(contFelicidad<0)
		{
			contFelicidad=0;
		}
		LimitSalud();
	}

	void LimitSalud()
	{
		if(contSalud>100)
		{
			contSalud=100;
		}
		if(contSalud<0)
		{
			contSalud=0;
		}
	}
}
