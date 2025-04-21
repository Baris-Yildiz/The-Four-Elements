using UnityEngine;

public class Fire : BaseElement
{
    public float speedMultipF => base.speedMultip;

    public float defenseMultipF => base.defenseMultip*0.5f;

    public float attackMultipF=> base.attackMultip*2f;
    public float healthMultipF => base.healthMultip * 0.8f;
    public ElementalType elementalTypeF => ElementalType.FIRE;

    
    
    
}
