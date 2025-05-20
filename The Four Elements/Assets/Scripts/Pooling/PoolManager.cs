using System;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject[] pooledObjects;
    private int currentObjectIndex = 0;
    public Action<GameObject> OnSpawnPooledObject;
    public GameObject PoolObject = null;
    public int PoolAmount = 0;

    void Awake()
    {
        SetPooledObjects();
    }

    // void OnHit(MagicFX5_EffectSettings.EffectCollisionHit hit)
    // {
    // }
    //
    // private void OnEnable()
    // {
    //     for (int i = 0; i < pooledObjects.Length; i++)
    //     {
    //         pooledObjects[i].TryGetComponent<MagicFX5_EffectSettings>(out MagicFX5_EffectSettings component);
    //         component.OnEffectCollisionEnter += OnHit;
    //     }
    // }
    //
    // private void OnDisable()
    // {
    //     for (int i = 0; i < pooledObjects.Length; i++)
    //     {
    //         pooledObjects[i].TryGetComponent<MagicFX5_EffectSettings>(out MagicFX5_EffectSettings component);
    //         component.OnEffectCollisionEnter -= OnHit;
    //     }
    // }

    private void SetPooledObjects()
    {
        pooledObjects = new GameObject[PoolAmount];

        for (int i = 0; i < pooledObjects.Length; i++)
        {
            GameObject pooledObject = Instantiate(PoolObject, transform);
            pooledObject.SetActive(false);
            pooledObjects[i] = pooledObject;
        }
    }

    public GameObject SpawnPooledObject(Vector3 position, Quaternion rotation)
    {
        GameObject currentObject = GetCurrentObject();

        if (currentObject == null)
        {
            return null;
        }

        OnSpawnPooledObject?.Invoke(currentObject);

        currentObject.SetActive(false);
        currentObject.transform.SetPositionAndRotation(position, rotation);
        currentObject.SetActive(true);
        
        SetCurrentObject();
        return currentObject;
    }

    public GameObject GetCurrentObject()
    {
        return pooledObjects[currentObjectIndex];
    }

    private void SetCurrentObject()
    {
        currentObjectIndex = (currentObjectIndex + 1) % pooledObjects.Length;
    }
}
