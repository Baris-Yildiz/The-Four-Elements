using System.Collections;
using UnityEngine;

public class IceReactionManager : EnvironmentReactionManager
{
    public Material freezeMat;
    public override void ReactToEffect()
    {

        StartCoroutine(AddMaterialAfterSeconds(5f));
        
    }

    IEnumerator AddMaterialAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        print("girdim");
        gameObject.GetComponent<MeshRenderer>().materials = new Material[] { freezeMat };
        
    }

    IEnumerator ConvertToSoilReactiveObject(float seconds) { 
        yield return new WaitForSeconds(seconds);
        SoilReactionManager soilReactionManager = gameObject.AddComponent<SoilReactionManager>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (!isHit && other.GetComponentInParent<MagicFX5_EffectSettings>().gameObject.CompareTag("Ice"))
        {
            print("?");
            ReactToEffect();
            isHit = true;
        }
    }

    private void OnParticleTrigger()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
