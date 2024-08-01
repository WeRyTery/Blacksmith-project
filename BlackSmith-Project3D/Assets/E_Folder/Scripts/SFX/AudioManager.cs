using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    AudioSource audio;
    AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (audio != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                audio.volume = 1; //Changes volume of our audio

                audio.Play(); //Plays audio, after playing it 2 time it stops.
            }

            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                audio.PlayOneShot(audioClip); //Plays audio once. Can play few times without stopping other audio clips
            }

            //audio.PlayDelayed(10f); //Plays audio with delay of float number (in sec). In this example its 10sec

            if(Input.GetKeyDown(KeyCode.S))
            {
                audio.Stop(); //Stops audio clip
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                audio.Pause(); //Pauses our audio, gives us ability to resume it in future
                if (Input.GetKeyDown(KeyCode.P))
                {
                    audio.UnPause(); //Unpauses our audio, and resumes from the stopped point
                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (audio.volume != 0)
                {
                    audio.volume -= 0.1f;
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (audio.volume != 1)
                {
                    audio.volume += 0.1f;
                }
            }

        }
    }
}
