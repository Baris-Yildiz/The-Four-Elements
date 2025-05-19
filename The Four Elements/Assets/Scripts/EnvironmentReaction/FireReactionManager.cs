using MagicFX5;
using System.Collections;
using UnityEngine;

public class FireReactionManager : EnvironmentReactionManager
{
    public Material dissolveMat;
    private void Start()
    {
    }
    public override void ReactToEffect()
    {
        StartCoroutine(AddMaterialAfterSeconds(2.5f));
        Destroy(gameObject, 4.5f);
    }

    IEnumerator AddMaterialAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        dissolveMat.SetFloat("_startTime", Time.time);
        gameObject.GetComponent<MeshRenderer>().materials = new Material[]{dissolveMat};
    }


    private void OnParticleCollision(GameObject other)
    {
        if (!isHit && other.GetComponentInParent<MagicFX5_EffectSettings>().gameObject.CompareTag("Fire"))
        {
            ReactToEffect();
            isHit = true;
        }
    }

}
