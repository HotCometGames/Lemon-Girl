using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GramofonScript : MonoBehaviour
{
    public AudioSource audioSource;
    public bool countinuePlaying = true;

    public PlayerLogicScript playerLogicScript;
    public Slider slider;

    // Update is called once per frame

    void Start()
    {
        audioSource.volume = 2;
    }
    void Update()
    {
        if(!audioSource.isPlaying && countinuePlaying)
        {
            audioSource.Play();
        }
    }
    public void setVolume()
    {
        audioSource.volume = slider.value;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
            countinuePlaying = false;
            audioSource.Stop();
            playerLogicScript.Win();
        }
    }
}
