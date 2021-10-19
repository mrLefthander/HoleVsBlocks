using UnityEngine;

[CreateAssetMenu(fileName = "Game State", menuName = "Game/Game State")]
public class GameStateSO : ScriptableObject
{
  [SerializeField] private SceneLoadChannelSO _sceneLoadEventChannel;

#if UNITY_EDITOR
  public bool IsMoving => Input.GetMouseButton(0);
#else
  public bool IsMoving => Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved;
#endif

  public bool IsGameOver = false;

  private void OnEnable()
  {
    ResetGameOverState();
    _sceneLoadEventChannel.OnNextLevelRequested += ResetGameOverState;
  }

  private void OnDestroy()
  {
    _sceneLoadEventChannel.OnNextLevelRequested -= ResetGameOverState;
  }

  private void ResetGameOverState()
  {
    IsGameOver = false;
  }
}
