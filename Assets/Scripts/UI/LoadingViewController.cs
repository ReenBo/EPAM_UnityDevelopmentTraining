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

        public void Show()
        {
            gameObject.SetActive(true);
            StartCoroutine(FillLoadingLine(true));
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            StopAllCoroutines();
        }

        private IEnumerator FillLoadingLine(bool isDone)
        {
            while (isDone)
            {
                _loadingLine.fillAmount += Mathf.Clamp01(1e-3f);

                yield return null;
            }
            yield break;
        }
    }
}
