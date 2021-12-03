using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class SceneLoader: MonoBehaviour
{
  [Header("Scene references")]
  [SerializeField] private AssetReference _mainMenuScene;
  [SerializeField] private AssetReference _endMenuScene;
  [SerializeField] private AssetReference _tableScene;
  [SerializeField] private AssetReference _uiScene;


  [Header("Scriptable Objects")]
  [SerializeField] private SceneLoadChannelSO _sceneLoadEventChannel;
  [SerializeField] private LevelStateSO _levels;

  private SceneInstance _currentlyActiveScene = new SceneInstance();
  private SceneInstance _uiSceneRef = new SceneInstance();
  private SceneInstance _tableSceneRef = new SceneInstance();

  private void Start()
  {
    Addressables.LoadSceneAsync(_mainMenuScene, LoadSceneMode.Additive).Completed += SceneLoadCompleted;
  }

  private void OnEnable()
  {
    _sceneLoadEventChannel.OnGameStartRequested += LoadGameplay;
    _sceneLoadEventChannel.OnNextLevelRequested += LoadNextLevel;
    _sceneLoadEventChannel.OnGameEndRequested += LoadGameEnd;
  }

  private void OnDisable()
  {
    _sceneLoadEventChannel.OnGameStartRequested -= LoadGameplay;
    _sceneLoadEventChannel.OnNextLevelRequested -= LoadNextLevel;
    _sceneLoadEventChannel.OnGameEndRequested -= LoadGameEnd;
  }

  private void LoadGameplay()
  {
    Addressables.UnloadSceneAsync(_currentlyActiveScene, true);
    Addressables.LoadSceneAsync(_uiScene, LoadSceneMode.Additive).Completed += (h) => _uiSceneRef = h.Result;
    Addressables.LoadSceneAsync(_tableScene, LoadSceneMode.Additive).Completed += (h) => _tableSceneRef = h.Result;
    Addressables.LoadSceneAsync(_levels.LevelLocation, LoadSceneMode.Additive).Completed += SceneLoadCompleted;
  }
  private void LoadGameEnd()
  {
    Addressables.UnloadSceneAsync(_currentlyActiveScene, true);
    Addressables.UnloadSceneAsync(_uiSceneRef, true);
    Addressables.UnloadSceneAsync(_tableSceneRef, true);

    Addressables.LoadSceneAsync(_endMenuScene, LoadSceneMode.Additive).Completed += SceneLoadCompleted;
  }

  private void SceneLoadCompleted(AsyncOperationHandle<SceneInstance> handle)
  {
    _currentlyActiveScene = handle.Result;
  }

  private void LoadNextLevel()
  {
    Addressables.UnloadSceneAsync(_currentlyActiveScene, true);
    Addressables.LoadSceneAsync(_levels.LevelLocation, LoadSceneMode.Additive).Completed += SceneLoadCompleted;
  }
}
