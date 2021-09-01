using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Animator anim;
    public bool ready=true;
    private AudioSource mAudioSrc;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        mAudioSrc = GetComponent<AudioSource>();
    } 

    // Update is called once per frame
    void Update()
    {
        if (ready)
        {
           
            if (this.tag == "Hit")
            {
                ready = false;
                anim.SetInteger("enemyState", 5);
                StartCoroutine(delayHit());
            }
            if (this.tag == "Attack")
            {
                mAudioSrc.Play();
                ready = false;
                anim.SetInteger("enemyState", 1);
                StartCoroutine(delay());
            }
            if (this.tag == "Dead")
            {
                ready = false;
                anim.SetInteger("enemyState", 2);
                StartCoroutine(delay());
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            anim.SetInteger("enemyState", 3);
            
        }
        
    }

    IEnumerator delay()
    {

        yield return new WaitForSeconds(2);
        anim.SetInteger("enemyState", 3);
        yield return new WaitForSeconds(2);
        ready = true;

    }

    IEnumerator delayHit()
    {
        yield return new WaitForSeconds(1);
        anim.SetInteger("enemyState", 3);
        this.tag = "Enemy";
        ready = true;
    }

}
