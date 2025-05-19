using UnityEngine;

public abstract class EnvironmentReactionManager : MonoBehaviour
{
    protected bool isHit = false;
    public abstract void ReactToEffect();
}
