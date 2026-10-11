using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AttackParticleScript : MonoBehaviour
{
    public Transform orangePos;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = orangePos.position + new Vector3(0, 1, 0);
        transform.LookAt(player.transform);
    }
}
