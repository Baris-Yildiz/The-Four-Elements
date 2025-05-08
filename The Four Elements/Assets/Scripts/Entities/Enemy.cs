using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
    [SerializeField]private NavMeshAgent agent;
    [SerializeField] private float attackRange;
    [field: SerializeField] public Transform player { get; private set; }



}
