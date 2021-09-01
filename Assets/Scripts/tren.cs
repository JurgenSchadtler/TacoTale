using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tren : MonoBehaviour
{
    public AudioSource myAudio;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlaySoundAfterDelay(myAudio, 15));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlaySoundAfterDelay(AudioSource audioSource, float delay)
    {
        if (audioSource == null)
            yield break;
        yield return new WaitForSeconds(delay);
        audioSource.Play();
        StartCoroutine(PlaySoundAfterDelay(myAudio, 15));
    }

}
