using UnityEngine;

public class MovementSkill : SkillInstance
{
    public Vector3 targetPoint { get; set; }
    public MovementSkill(Skill skill, int charge) : base(skill, charge)
    {
    }

    public override void Activate()
    {
       
    }
}
