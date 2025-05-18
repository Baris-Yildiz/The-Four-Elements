using System;
using UnityEngine;

public class LookCamera : MonoBehaviour
{

    [SerializeField] private Transform parent;
    [SerializeField] private Vector3 offSet;


    private void Update()
    {
        transform.position = parent.transform.position + offSet;
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
    }
}
