using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerSFX : MonoBehaviour
{

    private AudioSource _source;
    [SerializeField] private float volume = 0.3f;
    
    [SerializeField]private Transform swordTransform;
    [SerializeField] private AudioClip[] manSlashSounds;
    [SerializeField] private AudioClip[] slashClips;
    [SerializeField] private AudioClip[] stabClips;
    [SerializeField] private AudioClip[] hitClips;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        _source.volume = volume;
        _source.spatialBlend = 1f;
    }

    public void PlaySlashClip()
    {
        int index = Random.Range(0, 1);
        int index1 = Random.Range(0, manSlashSounds.Length);
        AudioSource.PlayClipAtPoint(slashClips[index] ,transform.position );
        _source.PlayOneShot(manSlashSounds[index1]);
    }
    public void PlayStabClip()
    {
        int index = Random.Range(0, 1);
        int index1 = Random.Range(0, manSlashSounds.Length);
        AudioSource.PlayClipAtPoint(stabClips[index] ,transform.position );
        _source.PlayOneShot(manSlashSounds[index1]);
    }

    public void PlayHitClip()
    {
        int index = Random.Range(0, stabClips.Length);
        _source.PlayOneShot(hitClips[index] , Random.Range(0.5f , 1));
    }

}
