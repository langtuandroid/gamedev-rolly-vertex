using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LinkManagerrv : MonoBehaviour
    {
        [SerializeField]
        private string _urlForPrivacyPolicyrv;
        [SerializeField] 
        private string _urlForTermsOfUserv;
        [SerializeField]
        private Button _privacyButtonrv;
        [SerializeField] 
        private Button _termsButtonrv;

        private bool _externalOpeningUrlDelayFlag = false;

        private void Awake()
        {
            if (_termsButtonrv != null)
                _termsButtonrv.onClick.AddListener(() => OpenUrlrv(_urlForTermsOfUserv));

            if (_privacyButtonrv != null)
                _privacyButtonrv.onClick.AddListener(() => OpenUrlrv(_urlForPrivacyPolicyrv));
        }

        private void OnDestroy()
        {
            if (_termsButtonrv != null)
                _termsButtonrv.onClick.RemoveListener(() => OpenUrlrv(_urlForTermsOfUserv));

            if (_privacyButtonrv != null)
                _privacyButtonrv.onClick.RemoveListener(() => OpenUrlrv(_urlForPrivacyPolicyrv));
        }

        private async void OpenUrlrv(string url)
        {
            if (_externalOpeningUrlDelayFlag) return;
            _externalOpeningUrlDelayFlag = true;
            await OpenURLAsyncrv(url);
            StartCoroutine(WaitForSecondsrv(1, () => _externalOpeningUrlDelayFlag = false));
        }
    
        private async Task OpenURLAsyncrv(string url)
        {
            await Task.Delay(1); // Ждем один кадр, чтобы избежать блокировки основного потока
            try
            {
                Application.OpenURL(url); // Открываем ссылку
            }
            catch (Exception e)
            {
                Debug.LogError($"Ошибка при открытии ссылки {url}: {e.Message}");
            }
        }

        private IEnumerator WaitForSecondsrv(float seconds, Action callback)
        {
            yield return new WaitForSeconds(seconds);
            callback?.Invoke();
        } 
    
    }
}
