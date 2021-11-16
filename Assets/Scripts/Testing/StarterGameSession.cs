using ET.Core.LevelInfo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Testing
{
    public class StarterGameSession : MonoBehaviour
    {
        private InfoSceneObjects _infoTestScene;

        protected void Awake()
        {
            _infoTestScene = GetComponent<InfoSceneObjects>();
        }

        protected void Update()
        {
            if (Input.GetKey(KeyCode.P))
            {
                GameManager.Instance.InitGame(_infoTestScene);
                GameManager.Instance.GameSessionStatus(true);
            }
        }
    }
}

