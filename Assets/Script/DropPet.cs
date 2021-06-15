using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropPet : MonoBehaviour, IDropHandler    
{
    Change_States CS;
    public void OnDrop(PointerEventData eventData)
    {
        CS = GameObject.Find("Scripts").GetComponent(typeof(Change_States)) as Change_States;
        CS.Alimentar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
