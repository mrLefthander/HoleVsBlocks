using UnityEngine;
using DG.Tweening;

public class HoleMagnetAnimation : MonoBehaviour
{
  [SerializeField] private Transform _magnetCircle;
  [SerializeField] private float _rotationSlowness = 0.3f;

  private void Start()
  {
    Rotate();
  }

  private void Rotate()
  {
    _magnetCircle
      .DORotate(new Vector3(90f, -90f, 0f), _rotationSlowness)
      .SetEase(Ease.Linear)
      .From(new Vector3(90f, 0f, 0f))
      .SetLoops(-1, LoopType.Incremental);
  }
}
