using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "UnitFunctions/PlaySound")]
public class PlaySound : UnitFunction
{
    [SerializeField] private AudioClip clip;

    public override void Activate(TokenUnit tUnit)
    {
        var audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        audioManager.PlaySound(clip);
    }
}
