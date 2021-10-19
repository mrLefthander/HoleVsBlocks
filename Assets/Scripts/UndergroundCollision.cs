using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class UndergroundCollision : MonoBehaviour
{
  [Header("Collisions Layer Masks")]
  [SerializeField] private LayerMask _objectivesLayerMask;
  [SerializeField] private LayerMask _obstaclesLayerMask;

  [Header("Game Lose Shake parameters")]
  [SerializeField] private float _shakeDuration = 1f;
  [SerializeField] private float _shakeStrenght = 0.2f;

  [Header("Scriptable Objects Channels")]
  [SerializeField] private GameStateSO _gameState;
  [SerializeField] private SceneLoadChannelSO _sceneLoadEventChannel;

  public static event UnityAction<Rigidbody> ObjectiveDestroyed;

  private void OnTriggerEnter(Collider other)
  {
    if(_gameState.IsGameOver) { return; }

    int layer = other.gameObject.layer;

    if (CompareLayerWithMask(layer, _objectivesLayerMask))
    {
      Destroy(other.gameObject);
      ObjectiveDestroyed?.Invoke(other.attachedRigidbody);
    }
    if (CompareLayerWithMask(layer, _obstaclesLayerMask))
    {
      //_gameState.IsGameOver = true;
      Camera.main.transform
        .DOShakePosition(_shakeDuration, _shakeStrenght)
        .OnComplete(() => 
        _sceneLoadEventChannel.RaiseNextLevelEvent());
    }
  }

  private bool CompareLayerWithMask(int layer, LayerMask mask)
  {
    return mask == (mask | (1 << layer));
  }
}
