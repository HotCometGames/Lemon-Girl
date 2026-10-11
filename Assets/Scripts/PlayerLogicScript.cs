using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerLogicScript : MonoBehaviour
{
    //health
    public int maxHealth = 5;
    public int health = 5;
    public float invinciblePeriod = 0.5f;
    public float invincibleTimer = 0f;
    public float healSpeed = 8f;
    float timeToHeal = 0;

    //settings
    public Slider slider;
    public GameObject settings;
    public bool canOpenSettings = true;


    //death
    public bool dead = false;
    public Transform respawnPoint;
    public Transform ogRespawnPoint;
    public GameObject deathScreen;
    public TextMeshProUGUI deathMessageText;
    public string deathMessageString = "You Suck";

    //win
    public GameObject winScreen;

    //hitEffect
    public Material normalMaterial;
    public Material hitMaterial;
    public Renderer lemonRenderer;


    //camera
    public CinemachineFreeLook cinemachineFreeLook;
    public float ySens = 10;
    public float xSens = 600;


    //scripts
    public BloodScript bloodScript;
    public PlayerMovementScript playerMovementScript;
    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && canOpenSettings)
        {
            settings.SetActive(!settings.activeSelf);
            if(settings.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            } else {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        if (health < maxHealth)
        {
            if (timeToHeal >= healSpeed)
            {
                health++;
                bloodScript.health = health;
                bloodScript.checkHealth();
                timeToHeal = 0;
            } else {
                timeToHeal += Time.deltaTime;
            }
        }
        if (invincibleTimer > 0)
        {
            invincibleTimer -= Time.deltaTime;
        }
    }

    public void TakeDamage()
    {
        if(invincibleTimer <= 0)
        {
            health--;
            bloodScript.health = health;
            bloodScript.checkHealth();
            timeToHeal = 0;
            invincibleTimer = invinciblePeriod;
            startFlash();
            if(health <= 0)
            {
                Die();
            }
        }
    }

    public void setHealth(int targetHealth)
    {
        health = 0;
        bloodScript.health = health;
        bloodScript.checkHealth();
        if(health <= 0)
        {
            Die();
        }
    }

    void startFlash()
    {
        StartCoroutine(damageFlash());
    }

    IEnumerator damageFlash()
    {
        lemonRenderer.material = hitMaterial;
        yield return new WaitForSeconds(0.1f);
        lemonRenderer.material = normalMaterial;
    }

    public void Die()
    {
        dead = true;
        playerMovementScript.dead = true;
        cinemachineFreeLook.m_YAxis.m_MaxSpeed = 0;
        cinemachineFreeLook.m_XAxis.m_MaxSpeed = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        deathScreen.SetActive(true);
        deathMessageText.text = deathMessageString;
        settings.SetActive(false);
        canOpenSettings = false;
    }

    public void Win()
    {
        dead = true;
        playerMovementScript.dead = true;
        cinemachineFreeLook.m_YAxis.m_MaxSpeed = 0;
        cinemachineFreeLook.m_XAxis.m_MaxSpeed = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        winScreen.SetActive(true);
        settings.SetActive(false);
        canOpenSettings = false;
    }

    public void Respawn()
    {
        dead = false;
        playerMovementScript.dead = false;
        transform.position = respawnPoint.position + new Vector3(0, 1, 0);
        health = maxHealth;
        bloodScript.maxHealth = maxHealth;
        bloodScript.health = health;
        bloodScript.checkHealth();
        lemonRenderer.material = normalMaterial;
        cinemachineFreeLook.m_YAxis.m_MaxSpeed = ySens;
        cinemachineFreeLook.m_XAxis.m_MaxSpeed = xSens;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        deathScreen.SetActive(false);
        winScreen.SetActive(false);
        settings.SetActive(false);
        canOpenSettings = true;
    }

    public void Reset()
    {
        dead = false;
        playerMovementScript.dead = false;
        respawnPoint = ogRespawnPoint;
        transform.position = respawnPoint.position + new Vector3(0, 1, 0);
        health = maxHealth;
        bloodScript.maxHealth = maxHealth;
        bloodScript.health = health;
        bloodScript.checkHealth();
        lemonRenderer.material = normalMaterial;
        cinemachineFreeLook.m_YAxis.m_MaxSpeed = ySens;
        cinemachineFreeLook.m_XAxis.m_MaxSpeed = xSens;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        deathScreen.SetActive(false);
        winScreen.SetActive(false);
        settings.SetActive(false);
        canOpenSettings = true;
    }
    public void SetSens()
    {
        xSens = slider.value*60;
        ySens = slider.value;
        cinemachineFreeLook.m_YAxis.m_MaxSpeed = ySens;
        cinemachineFreeLook.m_XAxis.m_MaxSpeed = xSens;
    }

    void OnParticleCollision(GameObject other)
    {
        if(other.name == "AttackParticles" && !dead)
        {
            deathMessageString = "You Suck";
            TakeDamage();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        int targetLayer = LayerMask.NameToLayer("Ground");
        if(collision.gameObject.layer == targetLayer && !dead)
        {
            deathMessageString = "You Suck";
            setHealth(0);
        }

        int checkPointLayer = LayerMask.NameToLayer("CheckPoint");
        if(collision.gameObject.layer == checkPointLayer)
        {
            respawnPoint = collision.gameObject.transform;
        }
    }
}
