using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodScript : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public GameObject[] bloodImages;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void checkHealth()
    {
        float precentHealth = health/maxHealth * 100;
        if(precentHealth >= 100)
        {
            for(int i = 0; i < bloodImages.Length; i++)
            {
                bloodImages[i].SetActive(false);
            }
        } else if (precentHealth >= 80)
        {
            for(int i = 0; i < bloodImages.Length; i++)
            {
                if(i<1)
                {
                    bloodImages[i].SetActive(true);
                } else 
                {
                    bloodImages[i].SetActive(false);
                }
            }
        } else if (precentHealth >= 60)
        {
            for(int i = 0; i < bloodImages.Length; i++)
            {
                if(i<2)
                {
                    bloodImages[i].SetActive(true);
                } else 
                {
                    bloodImages[i].SetActive(false);
                }
            }
        } else if (precentHealth >= 40)
        {
            for(int i = 0; i < bloodImages.Length; i++)
            {
                if(i<3)
                {
                    bloodImages[i].SetActive(true);
                } else 
                {
                    bloodImages[i].SetActive(false);
                }
            }
        } else if (precentHealth >= 20)
        {
            for(int i = 0; i < bloodImages.Length; i++)
            {
                if(i<4)
                {
                    bloodImages[i].SetActive(true);
                } else 
                {
                    bloodImages[i].SetActive(false);
                }
            }
        }
        else if (precentHealth >= 0)
        {
            for(int i = 0; i < bloodImages.Length; i++)
            {
                bloodImages[i].SetActive(true);
            }
        }
    }
}
