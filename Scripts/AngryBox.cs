using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryBox : MonoBehaviour
{
    [SerializeField] AudioClip angryBoxAudio;

    private AudioSource myAudioSource;
    
    void Awake()
    {
        myAudioSource = this.GetComponent<AudioSource>();
        this.GetComponent<Animator>().enabled = false;
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if(collisionInfo.gameObject.tag == "Player" && this.gameObject.tag == "AngryBox") {
            this.gameObject.tag = "Untagged";
            GameManagerScript.angryBoxCount++;
            FindObjectOfType<SceneManagerScript>().IncrementAngryBox();
            this.GetComponent<Animator>().enabled = true;
            myAudioSource.PlayOneShot(angryBoxAudio);
            if(GameManagerScript.angryBoxCount == 5) {
                SceneManagerScript.gameOver = true;
            }
        }
    }
}
