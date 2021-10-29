using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyPosWeapon : MonoBehaviour
{
    [SerializeField] private Transform _handPos;
    [SerializeField] private Transform _shootTargetPosition;

    protected void Update()
    {
        gameObject.transform.position = _handPos.position;
        gameObject.transform.rotation = _shootTargetPosition.rotation;
    }
}
