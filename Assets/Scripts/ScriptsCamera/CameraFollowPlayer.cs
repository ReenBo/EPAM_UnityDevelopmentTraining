using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class CameraFollowPlayer : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        private Vector3 _distance;
        [SerializeField] private float _speed = 1;

        private void Start()
        {
            _distance = transform.position - _player.position;
        }

        private void FixedUpdate()
        {
            FollowPlayer();
        }

        private void FollowPlayer()
        {
            transform.position = Vector3.Lerp(
                transform.position,
                _player.position + _distance,
                _speed * Time.deltaTime);
        }
    }
}
