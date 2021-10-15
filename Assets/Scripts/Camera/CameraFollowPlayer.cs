using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class CameraFollowPlayer : MonoBehaviour
    {
        #region Variables
        private Transform _playerTransform;
        private Vector3 _distance;

        [Header("Camera parameters")]
        [Range(0, 10)]
        [SerializeField] private float _speed = 1;
        #endregion

        #region Methods
        protected void Start()
        {
            if (!_playerTransform)
            {
                _playerTransform = GameObject.FindGameObjectWithTag("PlayerPosition").transform;
                _distance = transform.position - _playerTransform.position;
            }
        }

        protected void FixedUpdate()
        {
            FollowPlayer();
        }

        private void FollowPlayer()
        {
            transform.position = Vector3.Lerp(
                transform.position,
                _playerTransform.position + _distance,
                _speed * Time.deltaTime);
        }
        #endregion
    }
}
