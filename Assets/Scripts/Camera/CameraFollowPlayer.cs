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
        protected void FixedUpdate()
        {
            if (_playerTransform)
            {
                FollowPlayer();
            }
        }

        public void GetPlayerPosition(Transform target)
        {
            _playerTransform = target;
            _distance = CalculateDistance();
        }

        private Vector3 CalculateDistance()
        {
            _distance = transform.position - _playerTransform.position;

            return _distance;
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
