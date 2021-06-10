using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class firstSelection : MonoBehaviour
{
    public void CheckStartGame()
    {
        int Inicio = PlayerPrefs.GetInt("Inicios");
        if(Inicio == 1)
        {
            SceneManager.LoadScene("mainScene");
        }
        else
        {
            SceneManager.LoadScene("firstSelection");
        }
    }
}
