using System;
using System.Collections;
using UnityEngine;

public class PlayerVFX : MonoBehaviour
{

    private EntityStats entity;
    [SerializeField]private Transform swordTransform;
    [SerializeField]private GameObject windSlash;

    [SerializeField] private GameObject fireSlash;

    [SerializeField] private GameObject waterSlash;

    [SerializeField] private GameObject soilSlash;
    [SerializeField] private float offSetAngle;
    private GameObject slash;

    private void Awake()
    {
        entity = GetComponent<EntityStats>();
        SetSlashesInactive();
        
    }

    void SetSlashesInactive()
    {
        windSlash.SetActive(false);
        fireSlash.SetActive(false);
        soilSlash.SetActive(false);
        waterSlash.SetActive(false);
    }


    public void PlaySlash()
    {
         slash = fireSlash;
        ElementalType type = entity.attackElement;
        switch (type)
        {
            case ElementalType.FIRE:
                fireSlash.SetActive(true);
                slash = fireSlash;
                break;
            case ElementalType.WATER :
                waterSlash.SetActive(true);
                slash = waterSlash;
                break;
            case ElementalType.WIND:
                windSlash.SetActive(true);
                slash = windSlash;
                break;
            case ElementalType.SOIL:
                soilSlash.SetActive(true);
                slash = soilSlash;
                break;
        }
        Quaternion slashRotation = Quaternion.LookRotation(transform.forward);
        GameObject gObject = Instantiate(slash, swordTransform.position, Quaternion.Euler(swordTransform.rotation.x, slashRotation.eulerAngles.y+offSetAngle , swordTransform.rotation.z));
        ParticleSystem pSystem = gObject.GetComponent<ParticleSystem>();
        pSystem.Play();
        StartCoroutine("SetInactive" , gObject);
    }

    IEnumerator SetInactive(GameObject gObject)
    {
       yield return new WaitForSeconds(1);
       Destroy(gObject);
       
    }

    void Update()
    {
        
    }
}
