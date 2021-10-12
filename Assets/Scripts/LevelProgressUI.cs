using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LevelProgressUI : MonoBehaviour
{
  [Header("Level Progress UI elements")]
  [SerializeField] private TMP_Text _currentLevelText;
  [SerializeField] private TMP_Text _nextLevelText;
  [SerializeField] private Image _progressFillImage;  
  [SerializeField] private float _fillSpeed = 0.3f;

  [Header("Scriptable Objects")]
  [SerializeField] private SceneLoadChannelSO _sceneLoadEventChannel;
  [SerializeField] private LevelStateSO _levelState;

  private void Start()
  {
    UndergroundCollision.ObjectiveDestroyed += UpdateProgressFill;
    _sceneLoadEventChannel.OnNextLevelRequested += InitializeNewLevel;

    InitializeNewLevel();
  }
  private void OnDestroy()
  {
    UndergroundCollision.ObjectiveDestroyed -= UpdateProgressFill;
    _sceneLoadEventChannel.OnNextLevelRequested -= InitializeNewLevel;
  }

  private void InitializeNewLevel()
  {
    _progressFillImage.DOFillAmount(0f, _fillSpeed * 2);

    int levelNumber = _levelState.CurrentLevelIndex + 1;
    _currentLevelText.text = (levelNumber).ToString();

    string nextLevelText = _levelState.IsLastLevel ? "!" : (levelNumber + 1).ToString();
    _nextLevelText.text = nextLevelText;
  }

  private void UpdateProgressFill()
  {
    float fillValue = 1f - _levelState.LevelProgress;
    _progressFillImage.DOFillAmount(fillValue, _fillSpeed);
  }
}
