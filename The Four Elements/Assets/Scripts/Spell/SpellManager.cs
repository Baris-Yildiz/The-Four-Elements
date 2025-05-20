using Cinemachine;
using System.Collections.Generic;
using System;
using UnityEngine;
using MagicFX5;

public class SpellManager : MonoBehaviour
{
    public static SpellManager Instance;
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

    public GameObject player;
    public Transform raycastStartTransform;
    

    public CinemachineVirtualCamera playerCamera;
    
    public LayerMask LayerMask;

    private PoolManager[] poolManagers;
    private PoolManager invisibleWallPoolManager;

    private int spellCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        player = GameObject.FindWithTag("Player");
        raycastStartTransform = GameObject.Find("ProjectileL").transform;

    }

    void OnEnable()
    {
        
        
    }

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


        

        if (Input.GetKeyDown(KeyCode.Z))
        {
            ShootForward();
        }
    }

    public void SpawnSpellObject(Transform target)
    {
        
        poolManagers[CurrentSpellIndex].OnSpawnPooledObject = (GameObject spellObj) =>
        {
            if (spellObj.TryGetComponent<MagicFX5_EffectSettings>(out MagicFX5_EffectSettings component))
            {
                Transform[] targetArray = new Transform[1];
                targetArray[0] = target;
                component.Targets = targetArray;
            } else
            {
                spellObj.GetComponentInChildren<WindSpellZone>().HitPlayer = false;
            }
        };
        if (CurrentSpellIndex == 3)
        {
            Vector3 windSpawnPosition = playerCamera.Follow.position + (playerCamera.Follow.position - playerCamera.transform.position).normalized;
            poolManagers[CurrentSpellIndex].SpawnPooledObject(windSpawnPosition, GameObject.FindWithTag("MainCamera").transform.rotation) ;
        } else
        {
            poolManagers[CurrentSpellIndex].SpawnPooledObject(raycastStartTransform.position, raycastStartTransform.rotation);
        }
        

    }

    public void SwitchSpellType()
    {
        CurrentSpellIndex = (CurrentSpellIndex + 1) % spellCount;
    }

    public void ShootForward()
    {
        RaycastHit hit;
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
                GameObject spawnedWall = invisibleWallPoolManager.SpawnPooledObject(hitNoMask.point, Quaternion.identity);
                SpawnSpellObject(spawnedWall.transform);
            }
        }
    }
}
