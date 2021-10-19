using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(SphereCollider))]
public class HoleMagnet : MonoBehaviour
{
  [SerializeField] private float _magnetForce;
  [SerializeField] private SceneLoadChannelSO _sceneLoadEventChannel;
  [SerializeField] private GameStateSO _gameState;

  private List<Rigidbody> _affectedRigidbodies = new List<Rigidbody>();
  private Transform _holeTransform;

  private void Start()
  {
    _holeTransform = transform;
    RemoveAllFromMagnetField();
    UndergroundCollision.ObjectiveDestroyed += RemoveFromMagnetField;
    _sceneLoadEventChannel.OnNextLevelRequested += RemoveAllFromMagnetField;
  }

  private void FixedUpdate()
  {
    if (_affectedRigidbodies.Count <= 0 || _gameState.IsGameOver) { return; }

    foreach (Rigidbody rb in _affectedRigidbodies)
    {
      rb.AddForce((_holeTransform.position - rb.position) * _magnetForce * Time.fixedDeltaTime);
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    AddToMagnetField(other.attachedRigidbody);
  }

  private void OnTriggerExit(Collider other)
  {
    RemoveFromMagnetField(other.attachedRigidbody);
  }

  private void OnDestroy()
  {
    UndergroundCollision.ObjectiveDestroyed -= RemoveFromMagnetField;
    _sceneLoadEventChannel.OnNextLevelRequested -= RemoveAllFromMagnetField;
  }

  private void AddToMagnetField(Rigidbody rb)
  {
    _affectedRigidbodies.Add(rb);
  }

  private void RemoveFromMagnetField(Rigidbody rb)
  {
    _affectedRigidbodies.Remove(rb);
  }

  private void RemoveAllFromMagnetField()
  {
    _affectedRigidbodies.Clear();
  }
}
