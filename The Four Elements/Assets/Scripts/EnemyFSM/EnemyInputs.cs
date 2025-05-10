using System;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using Random = UnityEngine.Random;

public class EnemyInputs : MonoBehaviour
{
    [field: SerializeField] public float attackRange { get; private set; }
    [field: SerializeField] public float attackSpeed { get; set; }
    [SerializeField] private float rangeOffset;
    public Vector3 velocity { get; set; }
    public bool playerDetected { get; set; }
    public bool chasePlayer { get; set; }
    public bool canAttack { get; set; }
    public Vector3 lastPosition { get; set; }
    public Vector3 hitPosition { get; set; }

    private void Awake()
    {
        canAttack = false;
        playerDetected = false;
        lastPosition = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        
    }

    private void Update()
    {
        //Debug.Log(velocity);
        if (!playerDetected)
        {
            canAttack = false;
        }

        float attackRangeSqr = attackRange * attackRange;

        if (playerDetected && (lastPosition - hitPosition).sqrMagnitude > attackRangeSqr)
        {
            canAttack = false;

            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            float offset = attackRange - rangeOffset;

            hitPosition = lastPosition + new Vector3(offset * Mathf.Cos(angle), 0, offset * Mathf.Sin(angle));
        }

        if ((hitPosition - transform.position).sqrMagnitude <= 0.2f * 0.2f)
        {
            canAttack = true;
        }
    }
}