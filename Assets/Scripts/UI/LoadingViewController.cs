using ET.Interface.UI;
using ET.Scenes.Preloader;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ET.UI.LoadingView
{
    public class LoadingViewController : MonoBehaviour, IUIScreenable
    {
        [SerializeField] private Image _loadingLine;

        public Image LoadingLine { get => _loadingLine; set => _loadingLine = value; }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
