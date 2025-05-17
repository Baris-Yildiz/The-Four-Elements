using UnityEngine;

public class EnemySFX : MonoBehaviour
{
   private AudioSource _source;

   [SerializeField] private AudioClip[] hitSounds;
   [SerializeField] private AudioClip[] attackSounds;
   [SerializeField] private AudioClip[] walkSounds;


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
   public void PlayAttackSoundLoop()
   {
      int index = Random.Range(0, attackSounds.Length);
      _source.PlayOneShot(attackSounds[index] , Random.Range(0.6f , 1));
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
