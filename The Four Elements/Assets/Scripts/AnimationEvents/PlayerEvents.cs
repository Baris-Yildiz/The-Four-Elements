using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
   private bool attackEnd;

   public void AttackChange()
   {
      attackEnd = !attackEnd;
   }

   public bool AttackEnded()
   {
      return attackEnd;
   }

}
