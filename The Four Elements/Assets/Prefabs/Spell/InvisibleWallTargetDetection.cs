
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWallTargetDetection : MonoBehaviour
{
    public List<GameObject> CollidedObjects;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CollidedObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CollidedObjects.Count != 0)
        {
            CollidedObjects.Clear();
        }

        if (other.TryGetComponent<EnvironmentReactionManager>(out EnvironmentReactionManager reactionManager))
        {
            CollidedObjects.Add(other.gameObject);
        }
        
    }
}
