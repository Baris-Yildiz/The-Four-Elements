using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   private EntityAttackManager _attackManager;
   [SerializeField] private float time;
   [SerializeField] private float scale;
   private bool _isHitStopping = false;

   private void Awake()
   {
      _attackManager = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityAttackManager>();
   }

   private void OnEnable()
   {
      _attackManager.onAttackHit += StartTimeChange;
   }

   private void OnDisable()
   {
      _attackManager.onAttackHit -= StartTimeChange;
   }

   void StartTimeChange(GameObject obj , Vector3 p)
   {
      Stop(time , scale);
   }
   
   

   public void Stop(float duration, float scale = 0f)
   {
      if (_isHitStopping) return;
      StartCoroutine(HitStopCoroutine(duration, scale));
   }

   private IEnumerator HitStopCoroutine(float duration, float scale)
   {
      _isHitStopping = true;

      float originalTimeScale = Time.timeScale;
      Time.timeScale = scale;
      Time.fixedDeltaTime = 0.02f * Time.timeScale;

      yield return new WaitForSecondsRealtime(duration);

      Time.timeScale = originalTimeScale;
      Time.fixedDeltaTime = 0.02f * originalTimeScale;
      _isHitStopping = false;
   }
   
}
