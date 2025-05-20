using System.Collections;
using UnityEngine;

public class IceReactionManager : EnvironmentReactionManager
{
    public Material freezeMat;
    public GameObject wallToDisable;
    public override void ReactToEffect()
    {
        
        StartCoroutine(AddMaterialAfterSeconds(5f));
        
    }

    IEnumerator AddMaterialAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Transform child = gameObject.transform.GetChild(0);
        child.gameObject.SetActive(true);

        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        
        if (wallToDisable != null )
        {
            wallToDisable.SetActive(false);
        }

    }

    private void OnParticleCollision(GameObject other)
    {
        if (!isHit && other.GetComponentInParent<MagicFX5_EffectSettings>().gameObject.CompareTag("Ice"))
        {
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
