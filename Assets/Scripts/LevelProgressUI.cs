using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelProgressUI : MonoBehaviour
{
  [SerializeField] private LevelStateSO _levelState;
  [SerializeField] private TMP_Text _currentLevelText;
  [SerializeField] private TMP_Text _nextLevelText;
  [SerializeField] private Image _progressFillImage;


  private void Start()
  {
    UndergroundCollision.ObjectiveDestroyed += UpdateProgressFill;

    InitializeNewLevel();
  }

  private void InitializeNewLevel()
  {
    _progressFillImage.fillAmount = 0f;
    _currentLevelText.text = _levelState.CurrentLevelNumber.ToString();
    _nextLevelText.text = (_levelState.CurrentLevelNumber + 1).ToString();
  }

  private void UpdateProgressFill()
  {
    _progressFillImage.fillAmount = 1f - (float)_levelState.CurrentObjectivesCount / _levelState.TotalObjectivesCount;
  }

  private void OnDestroy()
  {
    UndergroundCollision.ObjectiveDestroyed -= UpdateProgressFill;
  }




}
