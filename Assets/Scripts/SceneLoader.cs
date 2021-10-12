using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class SceneLoader : MonoBehaviour
{
  [Header("Scene references")]
  [SerializeField] private AssetReference _mainMenuScene;
  [SerializeField] private AssetReference _tableScene;
  [SerializeField] private AssetReference _uiScene;

  [Header("Scriptable Objects")]
  [SerializeField] private SceneLoadChannelSO _sceneLoadEventChannel;
  [SerializeField] private LevelStateSO _levels;

  private SceneInstance _currentlyActiveScene = new SceneInstance();

  private void Start()
  {
    Addressables.LoadSceneAsync(_mainMenuScene, LoadSceneMode.Additive).Completed += SceneLoadCompleted;
  }

  private void OnEnable()
  {
    _sceneLoadEventChannel.OnGameStartRequested += LoadGameplay;
    _sceneLoadEventChannel.OnNextLevelRequested += LoadNextLevel;
  }

  private void OnDisable()
  {
    _sceneLoadEventChannel.OnGameStartRequested -= LoadGameplay;
    _sceneLoadEventChannel.OnNextLevelRequested -= LoadNextLevel;
  }

  private void LoadGameplay()
  {
    Addressables.UnloadSceneAsync(_currentlyActiveScene, true);
    Addressables.LoadSceneAsync(_tableScene, LoadSceneMode.Additive);
    Addressables.LoadSceneAsync(_levels.LevelLocation, LoadSceneMode.Additive).Completed += SceneLoadCompleted;
    Addressables.LoadSceneAsync(_uiScene, LoadSceneMode.Additive);
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
