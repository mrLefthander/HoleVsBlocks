using UnityEngine;
using UnityEngine.Events;

public class UndergroundCollision : MonoBehaviour
{
  [SerializeField] private GameStateSO _gameState;

  public static event UnityAction ObjectiveDestroyed;

  private void OnTriggerEnter(Collider other)
  {
    if(_gameState.IsGameOver) { return; }

    string tag = other.tag;

    if (tag.Equals("Objectives"))
    {
      Destroy(other.gameObject);
      ObjectiveDestroyed?.Invoke();
    }
    if (tag.Equals("Obstacles"))
    {
      _gameState.IsGameOver = true;
    }
  }
}
