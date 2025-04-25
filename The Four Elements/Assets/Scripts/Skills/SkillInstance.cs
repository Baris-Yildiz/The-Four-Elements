using UnityEngine;

public abstract class SkillInstance 
{
    
    
  
    private Skill skill;
    private int charge;
    private int currentCharge;
    private float remainingCooldown;
    public bool isActive { get; private set; }
    private bool isUsable;

    public SkillInstance(Skill skill, int charge)
    {
        this.skill = skill;
        this.charge = charge;
        remainingCooldown = skill.cooldownTime;
        isActive = false;
        isUsable = true;
        currentCharge = charge;
    }

    public bool isOnCooldown(float deltaTime)
    {
        remainingCooldown -= deltaTime;
        if (remainingCooldown <= 0)
        {
            isUsable = true;
            charge = Mathf.Clamp(currentCharge + 1, 1, charge);
            return true;
        }

        return false;
    }

    void Update()
    {
        isUsable = isOnCooldown(Time.deltaTime) || charge>0;
    }

    public abstract void Activate();
   

    public void Deactivate()
    {
        isActive = false;
    }

    public void ActivateBasicSkill()
    {
    }

    public void RenewCooldown()
    {
        remainingCooldown = skill.cooldownTime;
    }

    public void DecreaseCharge()
    {
        currentCharge = Mathf.Max(0, currentCharge - 1);
    }




}
