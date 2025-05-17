using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElementManager : MonoBehaviour
{
    private EntityStats player_stats;
    private Dictionary<KeyCode, ElementData> switches = new Dictionary<KeyCode, ElementData>();
    [field:SerializeField] public ElementData fireElement { get; private set; }
    [field:SerializeField] public ElementData windElement { get; private set; }
    [field:SerializeField] public ElementData soilElement { get; private set; }
    [field:SerializeField] public ElementData waterElement { get; private set; }

    [SerializeField] private float switchCooldown = 1f;
    private float remainingSwitchCooldown;
    

    private void Awake()
    {
        remainingSwitchCooldown = switchCooldown;
        player_stats = GetComponent<EntityStats>();
        switches.Add(KeyCode.Alpha1 , fireElement);
        switches.Add(KeyCode.Alpha2 , waterElement);
        switches.Add(KeyCode.Alpha3 , soilElement);
        switches.Add(KeyCode.Alpha4 , windElement);
    }
    // Update is called once per frame
    void Update()
    {
        remainingSwitchCooldown -= Time.deltaTime;
        ChangeElement();
    }

    void ChangeElement()
    {
        if (remainingSwitchCooldown <= 0)
        {
            int index = -1;
            foreach (var entry in switches)
            {
                index++;
                if (Input.GetKeyDown(entry.Key))
                {
                    player_stats.SwitchElement(entry.Value);
                    player_stats.ApplyElementStats();
                    SpellManager.Instance.CurrentSpellIndex = index;
                    remainingSwitchCooldown = switchCooldown;
                }
            }
            
        }
        
    }
}
