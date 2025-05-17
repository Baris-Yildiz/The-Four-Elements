using UnityEngine;

public class SoilReactionManager : EnvironmentReactionManager
{
    public override void ReactToEffect()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (!isHit && other.GetComponentInParent<MagicFX5_EffectSettings>().gameObject.CompareTag("Soil"))
        {
            print("?");
            ReactToEffect();
            isHit = true;
        }
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
