using UnityEngine;

public class ProjectileSkill : SkillInstance
{
    public Vector3 castingPoint{ get; set; }
    public Transform targetDirection { get; set; }

    public ProjectileSkill(Skill skill, int charge) : base(skill, charge)
    {
    }
    
    public override void Activate()
    {
        if (targetDirection != null)
        {
            SpellManager.Instance.SpawnSpellObject(targetDirection);    
        }
        else
        {
            SpellManager.Instance.ShootForward();
        }


        DecreaseCharge();
        RenewCooldown();
    }
}
