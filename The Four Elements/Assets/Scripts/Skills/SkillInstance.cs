using UnityEngine;

public abstract class SkillInstance 
{
  
    private Skill skill;
    private int charge;
    private float remainingCooldown;
    private bool isActive;
    private bool isUsable;

    public SkillInstance(Skill skill, int charge)
    {
        this.skill = skill;
        this.charge = charge;
        remainingCooldown = skill.cooldownTime;
        isActive = false;
        isUsable = true;
    }

    public bool isOnCooldown(float deltaTime)
    {
        remainingCooldown -= deltaTime;
        if (remainingCooldown <= 0)
        {
            isUsable = true;
            return true;
        }

        return false;
    }

    public abstract void Activate();
   

    public void Deactivate()
    {
        isActive = false;
    }

    public void ActivateBasicSkill()
    {
    }

   
    

}
