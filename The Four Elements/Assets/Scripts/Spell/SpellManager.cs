using Cinemachine;
using System;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    private int currentSpellIndex;
    public int CurrentSpellIndex
    {
        get
        {
            return currentSpellIndex;
        }
        set
        {
            currentSpellIndex = value;
        }
    }

    public Transform dummy;
    public GameObject player;
    public Transform raycastStartTransform;

    public CinemachineVirtualCamera playerCamera;
    public LayerMask LayerMask;
    public GameObject InvisibleSpellWall;

    private PoolManager[] poolManagers;
    private PoolManager invisibleWallPoolManager;

    private int spellCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        poolManagers = gameObject.GetComponentsInChildren<PoolManager>();
        spellCount = poolManagers.Length;

        invisibleWallPoolManager = GameObject.Find("InvisibleWallPool").GetComponent<PoolManager>();
        CurrentSpellIndex = 0;
        LayerMask &= ~LayerMask.GetMask("Building", "Ground");
        print(LayerMask);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnSpellObject(dummy);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SwitchSpellType();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ShootForward();
        }
    }

    private void SpawnSpellObject(Transform target)
    {
        poolManagers[CurrentSpellIndex].OnSpawnPooledObject = (GameObject spellObj) =>
        {
            if (spellObj.TryGetComponent<MagicFX5_EffectSettings>(out MagicFX5_EffectSettings component))
            {
                Transform[] targetArray = new Transform[1];
                targetArray[0] = target;
                component.Targets = targetArray;
            }
        };
        poolManagers[CurrentSpellIndex].SpawnPooledObject(raycastStartTransform.position, Quaternion.identity);
    }

    private void SwitchSpellType()
    {
        CurrentSpellIndex = (CurrentSpellIndex + 1) % spellCount;
    }

    private void ShootForward()
    {
        RaycastHit hit;
        Debug.DrawLine(playerCamera.transform.position, playerCamera.Follow.position, Color.black);
        // check for burnable
        if (Physics.Raycast(raycastStartTransform.position, playerCamera.Follow.position - playerCamera.transform.position, out hit, Mathf.Infinity, LayerMask))
        {
            SpawnSpellObject(hit.transform);
        }
        else
        {
            //check for non burnable
            RaycastHit hitNoMask;
            if (Physics.Raycast(raycastStartTransform.position, playerCamera.Follow.position - playerCamera.transform.position, out hitNoMask, Mathf.Infinity))
            {
                GameObject spawnedWall = invisibleWallPoolManager.SpawnPooledObject(hitNoMask.point, Quaternion.identity);// Instantiate(InvisibleSpellWall, hitNoMask.point, Quaternion.identity);
                SpawnSpellObject(spawnedWall.transform);
            }
        }
    }
}
