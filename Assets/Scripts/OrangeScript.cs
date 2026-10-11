using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeScript : MonoBehaviour
{
    public GameObject player;
    public ParticleSystem attackParticles;

    public float distanceToAttack = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float a = transform.position.x - player.transform.position.x;
        float aSquared = a*a;
        float b = transform.position.z - player.transform.position.z;
        float bSquared = b*b;
        float c = Mathf.Sqrt(aSquared + bSquared);
        float d = transform.position.y - player.transform.position.y;
        float dSquared = d*d;
        float distance = Mathf.Sqrt(c+dSquared);

        if(distance <= distanceToAttack)
        {
            if(!attackParticles.isPlaying)
            {
                attackParticles.Play();
            }
        }
    }
}
