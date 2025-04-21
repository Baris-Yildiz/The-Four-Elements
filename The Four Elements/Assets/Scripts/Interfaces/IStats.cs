using UnityEngine;



public interface IStats
{
   
 
   float maxHealth { get; }
   float baseAttack { get; }
   float baseDefense { get; }
   float baseSpeed { get; }
   ElementalType _elementalType { get; }
   float fireRes { get; }
   float waterRes { get; }
   float soilRes { get; }
   float windRes { get; }
}
