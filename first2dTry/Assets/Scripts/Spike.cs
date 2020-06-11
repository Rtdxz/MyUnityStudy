using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if (collision.gameObject.GetComponent<PlayerController>().isHurt != true)
            {
                collision.gameObject.GetComponent<PlayerController>().isHurt = true;
                SoundManager.instance.PlayHurtAudio();
            }
        }
    }
}
