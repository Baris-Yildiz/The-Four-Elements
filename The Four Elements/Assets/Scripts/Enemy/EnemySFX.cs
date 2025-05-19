using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySFX : MonoBehaviour
{
   private AudioSource _source;
   [SerializeField] private float volume = 0.3f;
   [SerializeField] private AudioClip[] hitSounds;
   [SerializeField] private AudioClip[] attackSounds;
   [SerializeField] private AudioClip[] walkSounds;

   private void Awake()
   {
      _source = GetComponent<AudioSource>();
      _source.volume = volume;
   }

   public void PlayHitSound()
   {
      int index = Random.Range(0, hitSounds.Length);
      _source.PlayOneShot(hitSounds[index] , Random.Range(0.6f , 1));
   }

   public void PlayAttackSoundOnce()
   {
      int index = Random.Range(0, attackSounds.Length);
      _source.PlayOneShot(attackSounds[index] , Random.Range(0.6f , 1));
   }
   public void PlayAttackSoundLoop(float attackSpeed)
   {
      int index = Random.Range(0, attackSounds.Length);
      _source.clip = attackSounds[index];
      _source.loop = true;
      _source.pitch = attackSounds[index].length / attackSpeed;
      _source.Play( );
   }

   public void ResetLoop()
   {
      _source.clip = null;
      _source.pitch = 1;
      _source.loop = false;
   }

   public void OnFootstep(AnimationEvent animationEvent)
   {
      if (animationEvent.animatorClipInfo.weight > 0.5f)
      {
         if (walkSounds.Length > 0)
         {
            var index = Random.Range(0, walkSounds.Length);
            AudioSource.PlayClipAtPoint(walkSounds[index], transform.TransformPoint(transform.position),
               Random.Range(0.4f, 0.8f));
         }
      }
   }

}
