using ET.Scenes.Preloader;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ET.UI.LoadingView
{
    public class LoadingViewController : MonoBehaviour
    {
        [SerializeField] private Image _loadingLine;

        public void ShowLoadingScreen(bool processing)
        {
            gameObject.SetActive(processing);
            
            StartCoroutine(FillLoadingLine(processing));
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
