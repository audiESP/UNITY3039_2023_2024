using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footstepsoundmanage : MonoBehaviour
{
    public AudioClip defaultFootstepSound;
    public AudioClip forestFootstepSound;
    

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
       audioSource = GetComponent<AudioSource>();
       audioSource.clip = defaultFootstepSound; 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ForestArea"))
        {
            audioSource.clip = forestFootstepSound;
        }
        

        // 播放走路声音
        audioSource.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
