using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerVolume : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void setVolume (float vol)
    {
        audioMixer.SetFloat("Volume",vol);
    }
}
