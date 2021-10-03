using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LevelProgressUI : MonoBehaviour
{
  [SerializeField] private LevelStateSO _levelState;
  [SerializeField] private TMP_Text _currentLevelText;
  [SerializeField] private TMP_Text _nextLevelText;


  [Space]
  [SerializeField] private Image _progressFillImage;  
  [SerializeField] private float _fillSpeed = 0.3f;


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
    float fillValue = 1f - (float)_levelState.CurrentObjectivesCount / _levelState.TotalObjectivesCount;
    _progressFillImage.DOFillAmount(fillValue, _fillSpeed);
  }

  private void OnDestroy()
  {
    UndergroundCollision.ObjectiveDestroyed -= UpdateProgressFill;
  }




}
