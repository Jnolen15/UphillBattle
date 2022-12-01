using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    public void PlaySound(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
