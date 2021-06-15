using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class swipeMenu : MonoBehaviour
{
    public GameObject scrollbar, labelName,labelSpecimen,labelDescription, inputName;
    private int selection;

    string[] names = new string[ ] { "Yui", "Yumi", "Kyu", "Kaiyu" };
    string[] specimen = new string[] { "Humanoide", "Gato estelar", "Humanoide", "Gato estelar" };
    string[] description = new string[] 
    { 
        "Descripcion extensa #1",
        "Descripcion extensa #2",
        "Descripcion extensa #3",
        "Descripcion extensa #4"
    };
    string[] diet = new string[]
    {
        "Frutal",
        "Carnivoro"
    };

    float scroll_pos = 0;
    float[] pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void preloadName()
    {
        inputName.GetComponent<InputField>().text = names[selection];
    }

    public void saveName()
    {
        PlayerPrefs.SetString("namePet", inputName.GetComponent<InputField>().text);
        PlayerPrefs.SetString("specimenPet", specimen[selection]);
        PlayerPrefs.SetString("dietPet", diet[selection]);
        PlayerPrefs.SetInt("selection", selection);        
        SceneManager.LoadScene("mainScene");
    }

    // Update is called once per frame
    void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for(int i=0; i<pos.Length; i++)
        {
            pos[i] = distance * i;
        }
        if (Input.GetMouseButton(0))
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        } else
        {
            for (int i=0;i<pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance/2) && scroll_pos > pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f,1f), 0.1f);
                    labelName.GetComponent<Text>().text = names[i];
                    labelSpecimen.GetComponent<Text>().text = specimen[i];
                    labelDescription.GetComponent<Text>().text = description[i];
                    selection = i;

                    for (int a = 0; a < pos.Length; a++)
                    {
                        if(a != i)
                        {
                            transform.GetChild(a).localScale = Vector2.Lerp(transform.GetChild(a).localScale, new Vector2(0.5f,0.5f), 0.1f);
                        }
                    }
                }
            }
        }
    }
}
