using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;
using DG.Tweening;

[CreateAssetMenu(fileName = "Level State", menuName = "Game/Level State")]
public class LevelStateSO : ScriptableObject
{
  [SerializeField] private AssetLabelReference _levelLabel;
  [SerializeField] private SceneLoadChannelSO _sceneLoadEventChannel;

  [Header("Game End Shake parameters")]
  [SerializeField] private float _shakeDuration = 1f;
  [SerializeField] private float _shakeStrenght = 0.2f;

  public float LevelProgress => (float)_currentObjectivesCount / _totalObjectivesCount;
  public int CurrentLevelIndex { get; private set; }
  public bool IsLastLevel => _levelsProvider.IsLastLevel(CurrentLevelIndex);
  public IResourceLocation LevelLocation => _levelsProvider.GetLevelLocation(CurrentLevelIndex);

  private int _totalObjectivesCount;
  private int _currentObjectivesCount;

  private LevelsLocationProvider _levelsProvider;

  public void InitializeNewLevel(int objectivesCount)
  {
    _totalObjectivesCount = objectivesCount;
    _currentObjectivesCount = objectivesCount;
  }

  private void OnEnable()
  {
    UndergroundCollision.ObjectiveDestroyed += OnObjectiveDestroyed;
    _levelsProvider = new LevelsLocationProvider(_levelLabel);
    _levelsProvider.Init();
    CurrentLevelIndex = 0;
  }

  private void OnDestroy()
  {
    UndergroundCollision.ObjectiveDestroyed -= OnObjectiveDestroyed;
  }

  private void OnObjectiveDestroyed(Rigidbody rb)
  {
    _currentObjectivesCount--;
    CheckLevelWin();
  }

  private void CheckLevelWin()
  {
    if (_currentObjectivesCount > 0) { return; }

    if (IsLastLevel)
    {
      CurrentLevelIndex = 0;
      Camera.main.transform
        .DOShakeRotation(_shakeDuration, _shakeStrenght)
        .OnComplete(() => _sceneLoadEventChannel.RaiseGameEndEvent());
    }
    else
    {
      CurrentLevelIndex++;
      _sceneLoadEventChannel.RaiseNextLevelEvent();
    }    
  }
}
