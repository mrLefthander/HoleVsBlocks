using UnityEngine;

[CreateAssetMenu(fileName = "Game State", menuName = "Game/Game State")]
public class GameStateSO : ScriptableObject
{
  public bool IsMoving = false;
  public bool IsGameOver = false;
}
