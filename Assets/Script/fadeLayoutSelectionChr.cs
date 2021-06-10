using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeLayoutSelectionChr : MonoBehaviour
{
    Animator Anim_txtTitle, Anim_txtSpecimen, Anim_txtDescription, Anim_btnBack, Anim_btnAccept, Anim_contentBox, Anim_inputField, Anim_txtlabelChangeName;
    public GameObject txtTitle, txtSpecimen, txtDescription, txtlabelChangeName, btnBack, btnAccept, contentBox, inputField;
    // Start is called before the first frame update
    void Start()
    {
        Anim_txtTitle = txtTitle.GetComponent<Animator>();
        Anim_txtSpecimen = txtSpecimen.GetComponent<Animator>();
        Anim_txtDescription = txtDescription.GetComponent<Animator>();
        Anim_btnBack = btnBack.GetComponent<Animator>();
        Anim_btnAccept = btnAccept.GetComponent<Animator>();
        Anim_contentBox = contentBox.GetComponent<Animator>();
        Anim_inputField = inputField.GetComponent<Animator>();
        Anim_txtlabelChangeName = txtlabelChangeName.GetComponent<Animator>();
    }

    public void switchFades()
    {
        Anim_txtTitle.SetTrigger("triggerFade");
        Anim_txtSpecimen.SetTrigger("triggerFade");
        Anim_txtDescription.SetTrigger("triggerFade");
        Anim_btnBack.SetTrigger("triggerFade");
        Anim_btnAccept.SetTrigger("triggerFade");
        Anim_contentBox.SetTrigger("triggerFixed");
        Anim_inputField.SetTrigger("triggerInput");
        Anim_txtlabelChangeName.SetTrigger("triggerFade");
    }    

    // Update is called once per frame
    void Update()
    {
        
    }
}
