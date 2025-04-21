using System;
using UnityEngine;

public class EntityAttackManager : MonoBehaviour
{
   public event Action onAttackStart;
   public event Action<GameObject ,Vector3> onAttackHit;
   [SerializeField]
   private EffectManager effectManager;
   
   
   
   private void Awake()
   {
      effectManager = GetComponent<EffectManager>();
   }
   
   public void PerformAttack()
   {
      onAttackStart?.Invoke();
   }

   public void PerformHit(GameObject _gameObject, Vector3 hitPoint)
   {
      if (_gameObject != null )
      {
         onAttackHit?.Invoke(_gameObject , hitPoint);
      }
      else
      {
         Debug.Log("Hitted object is null");
      }
   }

}
