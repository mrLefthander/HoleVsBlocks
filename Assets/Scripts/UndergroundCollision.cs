using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class UndergroundCollision : MonoBehaviour
{
  [SerializeField] private GameStateSO _gameState;
  [SerializeField] private SceneLoadChannelSO _sceneLoadEventChannel;

  [Header("Game Lose Shake parameters")]
  [SerializeField] private float _shakeDuration = 1f;
  [SerializeField] private float _shakeStrenght = 0.2f;

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
      //_gameState.IsGameOver = true;
      Camera.main.transform
        .DOShakePosition(_shakeDuration, _shakeStrenght)
        .OnComplete(() => 
        _sceneLoadEventChannel.RaiseNextLevelEvent());
    }
  }
}
