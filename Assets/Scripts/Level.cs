using UnityEngine;

public class Level : MonoBehaviour
{
  [SerializeField] private LevelStateSO _levelState;
  [SerializeField] private Transform _objectivesParent;

  private void Awake()
  {
    _levelState.InitializeNewLevel(_objectivesParent.childCount);
  }
}
