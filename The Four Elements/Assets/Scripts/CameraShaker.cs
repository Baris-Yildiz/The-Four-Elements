using System;
using Cinemachine;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{

    private CinemachineImpulseSource _impulse;

    private EntityAttackManager _attackManager;
    [SerializeField] private Vector3 shakeVelocity = Vector3.one;

    private void Awake()
    {
        _attackManager = GetComponent<EntityAttackManager>();
        _impulse = GetComponent<CinemachineImpulseSource>();
    }

    private void OnEnable()
    {
        _attackManager.onAttackHit += ShakeCamera;

    }

    private void OnDisable()
    {
        _attackManager.onAttackHit -= ShakeCamera;
        
    }

    void ShakeCamera(GameObject obj, Vector3 point)
    {
        _impulse.GenerateImpulseAtPositionWithVelocity(point , shakeVelocity);
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
