using System;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    [SerializeField] private Skill fireBall;
    [field: SerializeField] public ProjectileSkill projectileSkill { get; private set; }

    private void Awake()
    {
        projectileSkill = new ProjectileSkill(fireBall, 1);
    }

}
