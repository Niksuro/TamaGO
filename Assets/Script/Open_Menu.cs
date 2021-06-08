using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_Menu : MonoBehaviour {

	Animator Anim_Menu, Anim_Btn, Anim_Config, Anim_foodBtn, Anim_foodMenu;
	public GameObject Menu, Btn, Config_btn, food_btn, Food_Menu;
	// Use this for initialization
	void Start (){
		Anim_Menu = Menu.GetComponent<Animator>();
		Anim_Btn = Btn.GetComponent<Animator>();
		Anim_Config = Config_btn.GetComponent<Animator>();
		Anim_foodBtn = food_btn.GetComponent<Animator>();
		Anim_foodMenu = Food_Menu.GetComponent<Animator>();
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
		Anim_foodBtn.SetTrigger("Open");
		Anim_foodMenu.SetTrigger("Open");
    }
}
