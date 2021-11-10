using ET.Enemy;
using ET.Enemy.AI;
using ET.Scenes;
using UnityEngine;

namespace ET.Core.LevelInfo
{
    public class InfoSceneObjects : MonoBehaviour
    {
        [SerializeField] private SceneIndex _levelIndex;

        [Header("References to player objects in the scene")]
        [SerializeField] private Transform _playerSpawnTarget;

        [Header("References to subsystem objects in the scene")]

        [Header("References to enemy objects in the scene")]

        [Header("References to static level objects in the scene")]
        [SerializeField] private GameObject _ground;
        [SerializeField] private GameObject _lights;

        public SceneIndex LevelIndex { get => _levelIndex; }

        public Transform PlayerSpawnTarget { get => _playerSpawnTarget; }
        public GameObject Ground { get => _ground; }
        public GameObject Lights { get => _lights; }
    }
}
