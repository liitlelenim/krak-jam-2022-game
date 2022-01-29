using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class SoundsManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] sounds;

    public void PlaySound(int soundIndex)
    {
        HandleSound(sounds[soundIndex]);
    }

    private void HandleSound(AudioClip clip)
    {
        GameObject sound = new GameObject();
        AudioSource source = sound.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = 0.3f;
        source.Play();
        Destroy(sound, source.clip.length + 2f);
    }
}