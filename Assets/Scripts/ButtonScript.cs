using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public Animator animator;
    public bool pressed = false;
    public int buttonNum;


    public GameObject pictures;
    public GameObject pressButtonText;
    // Start is called before the first frame update
    void Start()
    {
        animator.Play("ButtonOn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        animator.Play("ButtonTurnOff");
        if(!pressed)
        {
            chooseFunc();
        }
        pressed = true;
    }

    void OnTriggerExit(Collider other)
    {
        animator.Play("ButtonTurnOn");
    }

    void chooseFunc()
    {
        switch (buttonNum)
        {
            case 1:
                showPics();
                break;
        }
    }

    void showPics()
    {
        pictures.SetActive(true);
        pressButtonText.SetActive(false);
    }
}
