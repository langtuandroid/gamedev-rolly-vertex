using Audio;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField]
        private Button _playGamerv;
        [SerializeField]
        private Button _settingsrv;
        [SerializeField]
        private GameObject _settingsPopuprv;
        
        [SerializeField]
        private TextMeshProUGUI _currentLevelrv;
        [SerializeField]
        private TextMeshProUGUI _bestScorerv;

        private void Awake()
        {
            _playGamerv.onClick.AddListener(LoadGame);
            _settingsrv.onClick.AddListener(ShowHideSettingsPopup);
        }

        private void Start()
        {
            int level = PlayerPrefs.GetInt("level");
            int best = PlayerPrefs.GetInt("best");
            _currentLevelrv.text = $"Level: {level}";
            _bestScorerv.text = $"Best Score: {best}";
        }

        private void OnDestroy()
        {
            _playGamerv.onClick.RemoveListener(LoadGame);
            _settingsrv.onClick.RemoveListener(ShowHideSettingsPopup);
        }
        
        private void ShowHideSettingsPopup()
        {
            AudioManager.Instance.PlaySFXOneShot(0);
            _settingsPopuprv.SetActive(!_settingsPopuprv.activeSelf);
            AudioManager.Instance.PlaySFXOneShot(1);
        }

        private void LoadGame()
        {
            AudioManager.Instance.PlaySFXOneShot(0);
            SceneManager.LoadScene("Game");
        }
    }
}
