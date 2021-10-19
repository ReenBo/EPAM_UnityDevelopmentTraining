using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Player.Spawner
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnTarget;
        [SerializeField] GameObject _player;

        public void CreatePlayerInSession()
        {
            Instantiate(_player, _spawnTarget.position, Quaternion.identity);
        }

        public void CreatePlayerInSession(Vector3 point)
        {
            Instantiate(_player, point, Quaternion.identity);
        }
    }
}
