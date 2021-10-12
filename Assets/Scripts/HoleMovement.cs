using UnityEngine;

public class HoleMovement : MonoBehaviour
{
  [Header("Hole mesh")]
  [SerializeField] private MeshFilter _meshFilter;
  [SerializeField] private MeshCollider _meshCollider;

  [Header("Hole parameters")]  
  [SerializeField] private float _radius;  
  [SerializeField] private float _moveSpeed;
  [SerializeField] private Transform _holeCenter;
  [SerializeField] private Vector2 _startPosition;
  [SerializeField] private Vector2 _moveLimits;

  [Header("Scriptable Objects")]
  [SerializeField] private GameStateSO _gameState;
  [SerializeField] private SceneLoadChannelSO _sceneLoadEventChannel;

  private Mesh _mesh;
  private HoleVerticesProvider _holeVertices;
  private float x, y;
  private Vector3 touch, targetPos;

  private void Start()
  {
    _mesh = _meshFilter.mesh;
    _holeVertices = new HoleVerticesProvider(_mesh, _holeCenter, _radius);
    MoveToLevelStartPosition();
    _sceneLoadEventChannel.OnNextLevelRequested += MoveToLevelStartPosition;
  }

  private void OnDisable()
  {
    _sceneLoadEventChannel.OnNextLevelRequested -= MoveToLevelStartPosition;
  }

  private void Update()
  {
    if (_gameState.IsGameOver || !_gameState.IsMoving) { return; }

    MoveHole();
  }

  private void MoveHole()
  {
    MoveHoleCenter();
    UpdateHoleVerticesPosition();
  }

  private void MoveHoleCenter()
  {
    x = Input.GetAxis("Mouse X");
    y = Input.GetAxis("Mouse Y");

    touch = Vector3.Lerp(
      _holeCenter.position,
      _holeCenter.position + new Vector3(x, 0f, y),
      _moveSpeed * Time.deltaTime
      );

    targetPos = new Vector3(
      Mathf.Clamp(touch.x, -_moveLimits.x, _moveLimits.x),
      touch.y,
      Mathf.Clamp(touch.z, -_moveLimits.y, _moveLimits.y)
      );

    _holeCenter.position = targetPos;
  }

  private void UpdateHoleVerticesPosition()
  {
    Vector3[] vertices = _mesh.vertices;
    for (int i = 0; i < _holeVertices.Count; i++)
    {
      vertices[_holeVertices.Indexes[i]] = _holeCenter.position + _holeVertices.Offsets[i];
    }
    _mesh.vertices = vertices;
    _meshFilter.mesh = _mesh;
    _meshCollider.sharedMesh = _mesh;
  }

  private void MoveToLevelStartPosition()
  {
    _holeCenter.position = new Vector3(_startPosition.x, _holeCenter.position.y, _startPosition.y);
    UpdateHoleVerticesPosition();
  }

  private void OnDrawGizmos()
  {
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(_holeCenter.position, _radius);
  }
}
