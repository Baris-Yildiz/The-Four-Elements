using System;
using UnityEngine;
[Serializable]
public abstract class BaseElement
{
    public ElementalType elementalType1
    {
        get => elementalType;
        set => elementalType = value;
    }

    public float attackMultip1
    {
        get => attackMultip;
        set => attackMultip = value;
    }

    public float defenseMultip1
    {
        get => defenseMultip;
        set => defenseMultip = value;
    }

    public float speedMultip1
    {
        get => speedMultip;
        set => speedMultip = value;
    }

    public float healthMultip1
    {
        get => healthMultip;
        set => healthMultip = value;
    }

    protected ElementalType elementalType = ElementalType.BASE;
    protected float attackMultip = 1f;
    protected float defenseMultip = 1f;
    protected float speedMultip = 1f;
    protected float healthMultip = 1f;



}
