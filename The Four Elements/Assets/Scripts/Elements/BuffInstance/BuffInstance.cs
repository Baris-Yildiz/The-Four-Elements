using UnityEngine;

[System.Serializable]
public class BuffInstance
{

    public Buff buff { get; private set; }
    private float remainingDuration;
    public int stackAmount;
    public bool isRefreshable;
    public bool isDot;
    public float remainingTickDuration;
    public BuffInstance(Buff buff)
    {
        this.buff = buff;
        remainingDuration = this.buff.duration;
        isRefreshable = this.buff.isRefreshable;
        isDot = this.buff.DamageOverTimeAmount != 0;
        
    }

    public bool IsBuffOver(float deltaTime)
    {
        remainingDuration -= deltaTime;
        return remainingDuration <= 0f;
    }

    public void Refresh()
    {
        if (isRefreshable)
        {
            remainingDuration = buff.duration;
        }
    }

    public bool IsTickTime(float deltaTime)
    {
        remainingTickDuration -= deltaTime;
        if (remainingTickDuration <= 0)
        {
            remainingTickDuration = buff.DamageOverTimeTickRate;
            return true;
        }
        return false;
    }



}
