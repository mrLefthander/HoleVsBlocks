using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

[CreateAssetMenu(fileName = "Level State", menuName = "Game/Level State")]
public class LevelStateSO : ScriptableObject
{
  [SerializeField] private AssetLabelReference _levelLabel;
  [SerializeField] private SceneLoadChannelSO _sceneLoadEventChannel;

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

  private void OnObjectiveDestroyed()
  {
    _currentObjectivesCount--;
    CheckLevelWin();
  }

  private void CheckLevelWin()
  {
    if (_currentObjectivesCount > 0) { return; }

    UpdateCurrentLevelIndex();
    _sceneLoadEventChannel.RaiseNextLevelEvent();
  }

  private void UpdateCurrentLevelIndex()
  {
    if (IsLastLevel) { return;  }
    
    CurrentLevelIndex++;
  }
}
