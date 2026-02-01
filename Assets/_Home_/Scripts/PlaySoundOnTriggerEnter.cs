using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider2D))]
public class PlaySoundOnTriggerEnter : MonoBehaviour
{
    public List<AudioClip> audioClips = new();
    public LayerMask layersToTriggerSound;
    private AudioSource _audioSource;
    private AudioSource audioSource
    {
        get
        {
            if (_audioSource == null) TryGetComponent<AudioSource>(out _audioSource);
            return _audioSource;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (audioSource.isPlaying) return;
        // If not in layermask
        if ((layersToTriggerSound & (1 << collision.gameObject.layer)) == 0) return;
        audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Count)]);
    }

}
