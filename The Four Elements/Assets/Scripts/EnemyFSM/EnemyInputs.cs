using System;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using Random = UnityEngine.Random;

public class EnemyInputs : MonoBehaviour
{
    private Enemy enemy;
    [field:SerializeField] public float attackRange { get; private set; }
    [SerializeField] private float rangeOffset;
    public bool playerDetected { get; set; }
    public bool chasePlayer { get; set; }
    public bool canAttack { get; set; }
    public Vector3 lastPosition { get; set; }
    public Vector3 hitPosition { get; set; }

    private void Awake()
    {
        canAttack = false;
        playerDetected = false;
        lastPosition = new Vector3(float.MaxValue , float.MaxValue , float.MaxValue);
        enemy = GetComponent<Enemy>();
    }
    
    void Update()
    {
        if (!playerDetected)
        {
            canAttack = false;
        }

        if (playerDetected && Vector3.Distance(lastPosition , hitPosition) > attackRange)
        {
            canAttack = false;
            Debug.Log(Vector3.Distance(lastPosition , transform.position) + " zazzzzsadasdas");
            float angle = Random.Range(0f, 360f) * (float)Math.PI / 180;
            hitPosition = lastPosition + new Vector3((attackRange-rangeOffset) * (float)Math.Cos(angle), 0,
                (attackRange-rangeOffset) * (float)Math.Sin(360 - angle));
        }

        if (Vector3.Distance(hitPosition, transform.position) <= 0.2f)
        {
            canAttack = true;
        }
    }
    
    
}
