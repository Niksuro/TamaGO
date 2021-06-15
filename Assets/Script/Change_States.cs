using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Change_States : MonoBehaviour {

	int contComida=5, contAnimo=5, contAseo=5, contSalud=50, contSueño=1, contFelicidad=50;
	int tempComida, tempAnimo, tempAseo, tempFelicidad, tempSueño;
	//en Minutos (LineaBasica: int promedioComida = 9000, promedioAnimo = 10800, promedioAseo = 21600, promedioSueño=900;)
	int promedioComida = 20, promedioAnimo = 10800, promedioAseo = 21600, promedioSueño = 10;
	bool AnimationState = false;
	public GameObject ic_Comida, ic_Animo, ic_Aseo, ic_Felicidad, ic_Sueño, ic_Salud, ic_Alerta, txt_Alerta, alertTaste;
	public Sprite C_5,C_4,C_3,C_2,C_1;
	Animator Alerta, Anim_Alert, Anim_AlertTaste;

	void Awake()
	{
		Alerta = ic_Alerta.GetComponent<Animator>();
		Anim_Alert = txt_Alerta.GetComponent<Animator>();
		Anim_AlertTaste = alertTaste.GetComponent<Animator>();
	}

	void Alertar()
	{		
		if ((contComida == 1 || contAnimo == 1 || contAseo == 1 || contSueño == 1) && !AnimationState)
		{			
			Alerta.SetTrigger("Open");	
			AnimationState = true;		
		}
		if (contComida > 1 && contAnimo > 1 && contAseo > 1 && contSueño > 1 && AnimationState) {			
			Alerta.SetTrigger("Open");
			AnimationState = false;
		}
	}

	public void UpdateBasics(int comida, int animo, int aseo, int sueño, int salud, int felicidad)
    {
		contComida = comida;
		contAnimo = animo;
		contAseo = aseo;
		contSueño = sueño;
		contSalud = salud;
		contFelicidad = felicidad;
    }

	public void SaveBasics()
    {
		PlayerPrefs.SetInt("contComida", contComida);
		PlayerPrefs.SetInt("contAnimo", contAnimo);
		PlayerPrefs.SetInt("contAseo", contAseo);
		PlayerPrefs.SetInt("contSueño", contSueño);
		PlayerPrefs.SetInt("contSalud", contSalud);
		PlayerPrefs.SetInt("contFelicidad", contFelicidad);
	}

	public void calcularComida(System.TimeSpan tiempoEntre)
	{
		int restarContador = Mathf.RoundToInt((float)(tiempoEntre.TotalSeconds) / promedioComida);	
		contComida-= restarContador;
		if(contComida < 1)
        {
			CalcularFelicidad(contComida, "Comida");
			CalcularSalud(contComida, "Comida");
			contComida = 1;
        }
		int restarTemporizador = Mathf.RoundToInt((float)tiempoEntre.TotalSeconds);		
		while(restarTemporizador > 0) {
			restarTemporizador-=promedioComida;
		}
		tempComida = (restarTemporizador + promedioComida) ;		
		ChangeSpriteComida();
		StartCoroutine(WaitComida());
	}

	public void calcularAnimo(System.TimeSpan tiempoEntre)
	{				
		int restarContador = Mathf.RoundToInt((float)(tiempoEntre.TotalSeconds) / promedioAnimo);		
		contAnimo-= restarContador;
		if(contAnimo < 1)
        {
			CalcularFelicidad(contAnimo, "Juego");
			CalcularSalud(contAnimo, "Juego");
			contAnimo = 1;
        }
		int restarTemporizador = Mathf.RoundToInt((float)tiempoEntre.TotalSeconds);
		while(restarTemporizador > 0) {
			restarTemporizador-=promedioAnimo;
		}
		tempAnimo = (restarTemporizador + promedioAnimo);
		ChangeSpriteAnimo();
		StartCoroutine(WaitAnimo());
	}

	public void calcularAseo(System.TimeSpan tiempoEntre)
	{
		int restarContador = Mathf.RoundToInt((float)(tiempoEntre.TotalSeconds) / promedioAseo);		
		contAseo-= restarContador;
        if (contAseo < 1)
        {
			CalcularFelicidad(contAseo, "Aseo");
			CalcularSalud(contAseo, "Aseo");
			contAseo = 1;
        }
		int restarTemporizador = Mathf.RoundToInt((float)tiempoEntre.TotalSeconds);
		while(restarTemporizador > 0) {
			restarTemporizador-=promedioAseo;
		}
		tempAseo = (restarTemporizador + promedioAseo);
		ChangeSpriteAseo();
		StartCoroutine(WaitAseo());
	}

	public void calcularSueño(System.TimeSpan tiempoEntre)
	{
		int multiplicadorSueño = 1;
		if (PlayerPrefs.GetInt("dormirEnCama") == 1)
		{
			multiplicadorSueño = 10;
		}
		int restarContador = Mathf.RoundToInt((float)(tiempoEntre.TotalSeconds) / (promedioSueño/multiplicadorSueño));		
		contSueño+= restarContador;
		if(contSueño>5){
			contSueño=5;
		}
		tempSueño = promedioSueño;
		ChangeSpriteSueño();
		//StartCoroutine(WaitSueño());
	}

	public void CalcularFelicidad(int modifier, string selection)
    {
		Debug.Log("MODIFIER en " + selection +": " + modifier.ToString());
		Debug.Log("FELICIDAD INICIAL: " + contFelicidad);
		switch (selection)
        {
			case "Comida":
				contFelicidad += (modifier * 2);
				break;
			case "Aseo":
				contFelicidad += (modifier * 2);
				break;
			case "Juego":
				contFelicidad += (modifier * 3);
				break;
		}
		LimitFelicidad();
		Debug.Log("FELICIDAD FINAL: " + contFelicidad);
    }

	public void CalcularSalud(int modifier, string selection)
    {
		switch (selection)
		{
			case "Comida":
				contSalud -= (modifier * modifier);
				break;
			case "Aseo":
				contSalud += (modifier * 2);
				break;
			case "Felicidad":
				contSalud += modifier;
				break;
		}
		LimitSalud();
	}

	void HappinessUp(int happiness)
    {
		contFelicidad += happiness;
    }

	void Heal(int heal)
    {
		Debug.Log("SALUDPre: " + contSalud);
		contSalud += heal;
		Debug.Log("SALUDPost: " + contSalud);
	}

	public void SaveMoment()
	{
		System.DateTime reinicio = System.DateTime.Now;
		PlayerPrefs.SetInt("A_rB", reinicio.Year);
		PlayerPrefs.SetInt("Me_rB", reinicio.Month);
		PlayerPrefs.SetInt("D_rB", reinicio.Day);
		PlayerPrefs.SetInt("H_rB", reinicio.Hour);
		PlayerPrefs.SetInt("Mi_rB", reinicio.Minute);
	}	

	void ChangeSpriteComida()
	{
		switch (contComida)
		{
			case 5:
				ic_Comida.GetComponent<Image>().sprite = C_5;
				break;
			case 4:
				ic_Comida.GetComponent<Image>().sprite = C_4;
				break;
			case 3:
				ic_Comida.GetComponent<Image>().sprite = C_3;
				break;
			case 2:
				ic_Comida.GetComponent<Image>().sprite = C_2;
				break;
			case 1:
				ic_Comida.GetComponent<Image>().sprite = C_1;
				Alertar();
				break;
		}
		ChangeSpriteFelicidad();
	}

	void ChangeSpriteAnimo()
	{
		switch (contAnimo)
		{
			case 5:
				ic_Animo.GetComponent<Image>().sprite = C_5;
				break;
			case 4:
				ic_Animo.GetComponent<Image>().sprite = C_4;
				break;
			case 3:
				ic_Animo.GetComponent<Image>().sprite = C_3;
				break;
			case 2:
				ic_Animo.GetComponent<Image>().sprite = C_2;
				break;
			case 1:
				ic_Animo.GetComponent<Image>().sprite = C_1;
				Alertar();
				break;
		}
		ChangeSpriteFelicidad();
	}

	void ChangeSpriteAseo()
	{
		switch (contAseo)
		{
			case 5:
				ic_Aseo.GetComponent<Image>().sprite = C_5;
				break;
			case 4:
				ic_Aseo.GetComponent<Image>().sprite = C_4;
				break;
			case 3:
				ic_Aseo.GetComponent<Image>().sprite = C_3;
				break;
			case 2:
				ic_Aseo.GetComponent<Image>().sprite = C_2;
				break;
			case 1:
				ic_Aseo.GetComponent<Image>().sprite = C_1;
				Alertar();
				break;
		}
		ChangeSpriteFelicidad();
	}

	void ChangeSpriteSueño()
	{
		switch (contSueño)
		{
			case 5:
				ic_Sueño.GetComponent<Image>().sprite = C_5;
				break;
			case 4:
				ic_Sueño.GetComponent<Image>().sprite = C_4;
				break;
			case 3:
				ic_Sueño.GetComponent<Image>().sprite = C_3;
				break;
			case 2:
				ic_Sueño.GetComponent<Image>().sprite = C_2;
				break;
			case 1:
				ic_Sueño.GetComponent<Image>().sprite = C_1;
				Alertar();
				break;
			default:
				ic_Sueño.GetComponent<Image>().sprite = C_1;
				Alertar();
				break;
		}
		ChangeSpriteFelicidad();
	}

	void ChangeSpriteFelicidad()
	{
		LimitFelicidad();
		if (contFelicidad>90)
		{
			ic_Felicidad.GetComponent<Image>().sprite = C_5;
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
						}
					} 
				} 
			}
		} 
		ChangeSpriteSalud();			
	}	

	void ChangeSpriteSalud()
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
		ChangeSpriteComida();
		tempComida = promedioComida;
		StartCoroutine(WaitComida());		
	}
	IEnumerator WaitAnimo()
	{
		yield return new WaitForSeconds(tempAnimo);		
		if (contAnimo > 1)
		{
			contAnimo--;
		}
		ChangeSpriteAnimo();
		tempAnimo = promedioAnimo;
		StartCoroutine(WaitAnimo());		
	}
	IEnumerator WaitAseo()
	{
		yield return new WaitForSeconds(tempAseo);		
		if (contAseo > 1)
		{
			contAseo--;
		}
		ChangeSpriteAseo();
		tempAseo = promedioAseo;
		StartCoroutine(WaitAseo());	
	}

	/*IEnumerator WaitSueño()
	{
		yield return new WaitForSeconds(tempSueño);		
		if (contSueño > 1)
		{
			contSueño--;
		}
		ChangeSpriteSueño();
		tempSueño = promedioSueño;
		StartCoroutine(WaitSueño());	
	}*/

	void AlertaError(string msg)
    {
		Debug.Log("CONTSUEÑO " + contSueño);
		ChangeSpriteSueño();
		txt_Alerta.GetComponent<Text>().text = msg;
		Anim_Alert.SetTrigger("triggerPopUp");
	}

	public void Alimentar()
	{
		if (contSueño < 2)
		{
			AlertaError("¡Tu pettit está agotada!");
			return;
		}
		if(contComida == 5)
        {
			AlertaError("¡Tu pettit ya está llena!");
			return;
		}

		

		if (TasteFood())
		{
			contComida = 5;
			contFelicidad+=20;
			LikeAnimation(true);
			HappinessUp(10);
			Heal(5);			
        }
        else
        {
			LikeAnimation(false);
			Heal(5);
			contComida++;
        }		

		contSueño--;
		
		ChangeSpriteComida();
		ChangeSpriteSueño();
		ChangeSpriteFelicidad();
		SaveMoment();
		Alertar();
	}

	void LikeAnimation(bool liked)
    {
		if (liked)
        {
			alertTaste.GetComponent<Image>().sprite = Resources.Load<Sprite>("CaraGusto");
			Anim_AlertTaste.SetTrigger("triggerPopUp");
        }
        else
        {
			alertTaste.GetComponent<Image>().sprite = Resources.Load<Sprite>("CaraDisgusto");
			Anim_AlertTaste.SetTrigger("triggerPopUp");
		}
    }

	bool TasteFood()
    {
		if (PlayerPrefs.GetString("foodSelected") == PlayerPrefs.GetString("dietPet"))
        {
			return true;
        }
        else { return false; }		
    }

	public void Jugar()
	{
		if (contSueño < 2)
		{
			AlertaError("¡Tu pettit está agotada!");
			return;
		}
		if (contAnimo!=5)
		{
			contAnimo++;
			HappinessUp(10);
		}

		contSueño-= 2;

		ChangeSpriteAnimo();
		ChangeSpriteFelicidad();
		ChangeSpriteSueño();
		SaveMoment();		
		Alertar();
	}

	public void Bañar()
	{
		if (contSueño < 2)
		{
			AlertaError("¡Tu pettit está agotada!");			
			return;
		}

		if (contAseo!=5)
		{
			contAseo = 5;
			HappinessUp(5);
			Heal(5);
		}

		contSueño--;

		ChangeSpriteAseo();
		ChangeSpriteFelicidad();
		ChangeSpriteSueño();
		SaveMoment();		
		Alertar();
	}

	public void AumentarFelicidad()
	{
		HappinessUp(10);
		contSueño -=1;
		if(contSueño <= 0)
		{
			contSueño=1;
		}
		ChangeSpriteSueño();
		ChangeSpriteFelicidad();
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
