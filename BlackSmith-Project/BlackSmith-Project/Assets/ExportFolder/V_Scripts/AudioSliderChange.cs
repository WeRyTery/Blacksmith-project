using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSliderChange : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource musicAudioSource;
    private float previousVolume;

    private void Start()
    {
        musicAudioSource = GetComponent<AudioSource>();
        volumeSlider.value = musicAudioSource.volume;
        volumeSlider.onValueChanged.AddListener(delegate { ChangeVolume(); });
        previousVolume = musicAudioSource.volume;
    }

    public void ChangeVolume()
    {
        if (musicAudioSource != null)
        {
            musicAudioSource.volume = volumeSlider.value;
        }
    }

    public void MuteSound()
    {
        if (musicAudioSource != null)
        {
            if (musicAudioSource.volume > 0f)
            {
                previousVolume = musicAudioSource.volume;
                musicAudioSource.volume = 0f;
            }
            else
            {
                musicAudioSource.volume = previousVolume;
            }
        }
    }
}
