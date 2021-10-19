using UnityEngine;

[CreateAssetMenu(fileName = "Game State", menuName = "Game/Game State")]
public class GameStateSO : ScriptableObject
{
#if UNITY_EDITOR
  public bool IsMoving => Input.GetMouseButton(0);
#else
  public bool IsMoving => Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved;
#endif

  public bool IsGameOver = false;

  private void OnEnable()
  {
    IsGameOver = false;
  }
}
