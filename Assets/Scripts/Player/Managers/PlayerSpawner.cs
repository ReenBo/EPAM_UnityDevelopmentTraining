using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Player.Spawner
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] GameObject _player;

        public void CreatePlayerInSession(Transform target)
        {
            Instantiate(_player, target.position, Quaternion.identity);
        }

        public void CreatePlayerInSession(Vector3 point)
        {
            Instantiate(_player, point, Quaternion.identity);
        }
    }
}
