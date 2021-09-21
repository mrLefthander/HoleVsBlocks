using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleMovement : MonoBehaviour
{
  [Header("Hole mesh")]
  [SerializeField] private MeshFilter _meshFilter;
  [SerializeField] private MeshCollider _meshCollider;

  [Header("Hole vertices radius")]
  [SerializeField] private Vector2 _moveLimits;
  [SerializeField] private float _radius;
  [SerializeField] private Transform _holeCenter;

  [Space]
  [SerializeField] private float _moveSpeed;

  [Space]
  [SerializeField] private GameStateSO _gameState;

  private Mesh _mesh;
  private List<int> _holeVertices = new List<int>();
  private List<Vector3> _offsets = new List<Vector3>();
  private int _holeVerticesCount;
  private float x, y;
  private Vector3 touch, targetPos;

  private void Start()
  {
    _mesh = _meshFilter.mesh;
    FindHoleVertices();
  }

  private void Update()
  {
    _gameState.IsMoving = Input.GetMouseButton(0);

    if(!_gameState.IsGameOver && _gameState.IsMoving)
    {
      MoveHole();
      UpdateHoleVerticesPosition();
    }
  }

  private void UpdateHoleVerticesPosition()
  {
    Vector3[] vertices = _mesh.vertices;
    for(int i = 0; i <_holeVerticesCount; i++)
    {
      vertices[_holeVertices[i]] = _holeCenter.position + _offsets[i];
    }
    _mesh.vertices = vertices;
    _meshFilter.mesh = _mesh;
    _meshCollider.sharedMesh = _mesh;
  }

  private void MoveHole()
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

  private void FindHoleVertices()
  {
    for(int i = 0; i < _mesh.vertices.Length; i++)
    {
      float distance = Vector3.Distance(_holeCenter.position, _mesh.vertices[i]);

      if (distance > _radius)
        continue;
      
      _holeVertices.Add(i);
      _offsets.Add(_mesh.vertices[i] - _holeCenter.position);
    }

    _holeVerticesCount = _holeVertices.Count;
  }

  private void OnDrawGizmos()
  {
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(_holeCenter.position, _radius);
  }
}
