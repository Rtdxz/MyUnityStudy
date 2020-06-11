using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public float speed = 2f;
    public float moveTime = 3f;

    private bool directionRight = true;

    private float timer;

    void Start()
    {
        timer = moveTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (directionRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            directionRight = !directionRight;
            timer = moveTime;
        }
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
