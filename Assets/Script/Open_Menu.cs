using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_Menu : MonoBehaviour {

	Animator Anim_Menu, Anim_Btn, Anim_Config;
	public GameObject Menu, Btn, Config_btn;
	// Use this for initialization
	void Start (){
		Anim_Menu = Menu.GetComponent<Animator>();
		Anim_Btn = Btn.GetComponent<Animator>();
		Anim_Config = Config_btn.GetComponent<Animator>();
	}
	public void Switch_menu () {
		Anim_Menu.SetTrigger("Open");
		Anim_Btn.SetTrigger("Close_btn");
	}

	public void Switch_config_menu(){
		Anim_Config.SetTrigger("Open_Config");
		Anim_Menu.SetTrigger("Open");
	}

	public void Switch_Out_Menus(){
		Anim_Btn.SetTrigger("Close_btn");
		Anim_Config.SetTrigger("Open_Config");
	}

	public void Switch_Food_Menu()
    {

    }
}
