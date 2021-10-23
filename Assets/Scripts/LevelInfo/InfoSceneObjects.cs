using ET.Enemy;
using ET.Enemy.AI;
using UnityEngine;

public class InfoSceneObjects : MonoBehaviour
{
    //[RequireComponent]
    [Header("References to player objects in the scene")]
    [SerializeField] private Transform _playerSpawnTarget;

    [Header("References to subsystem objects in the scene")]

    [Header("References to enemy objects in the scene")]

    [Header("References to static level objects in the scene")]
    [SerializeField] private GameObject _ground;
    [SerializeField] private GameObject _lights;

    public Transform PlayerSpawnTarget { get => _playerSpawnTarget; }
    public GameObject Ground { get => _ground; }
    public GameObject Lights { get => _lights; }
}
