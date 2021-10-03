using UnityEngine;

[CreateAssetMenu(fileName = "Game State", menuName = "Game/Game State")]
public class GameStateSO : ScriptableObject
{
  public bool IsMoving => Input.GetMouseButton(0);
  public bool IsGameOver = false;
}
