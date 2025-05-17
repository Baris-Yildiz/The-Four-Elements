using UnityEngine;

public class PlayerAttackEvents : MonoBehaviour
{
    public bool canHit { get; private set; } = false;


    public void Attack1()
    {
    }

    public void Attack2()
    {
    }

    public void Attack3()
    {
    }

    public void StartCollision()
    {
        Debug.Log("can hit");
        canHit = true;
    }

    public void CloseCollision()
    {
        Debug.Log("cannot hit");
        canHit = false;
    }
    
    

}
