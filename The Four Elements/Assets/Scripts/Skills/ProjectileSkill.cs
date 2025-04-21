using UnityEngine;

public class ProjectileSkill : SkillInstance
{
    public Vector3 castingPoint{ get; set; }
    public Vector3 targetDirection { get; set; }

    public ProjectileSkill(Skill skill, int charge) : base(skill, charge)
    {
    }
    
    public override void Activate()
    {
        
    }
}
