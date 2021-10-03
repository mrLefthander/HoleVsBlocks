using UnityEngine;

public class Level : MonoBehaviour
{
  [SerializeField] private LevelStateSO _levelState;
  [SerializeField] private Transform _objectivesParent;

  [SerializeField] private int _levelNumber;

  private void Start()
  {
    _levelState.InitializeNewLevel(_objectivesParent.childCount, _levelNumber);
  }
}
