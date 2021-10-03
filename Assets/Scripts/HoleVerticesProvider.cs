using System.Collections.Generic;
using UnityEngine;

public class HoleVerticesProvider
{
  public List<int> Indexes { get; private set; } = new List<int>();
  public List<Vector3> Offsets { get; private set; } = new List<Vector3>();
  public int Count => Indexes.Count;

  private Mesh _mesh;
  private float _maxDistance;
  private Transform _origin;  

  public HoleVerticesProvider(Mesh mesh, Transform origin, float maxDistance)
  {
    _mesh = mesh;
    _maxDistance = maxDistance;
    _origin = origin;

    FindVertices();
  }

  private void FindVertices()
  {
    for (int i = 0; i < _mesh.vertices.Length; i++)
    {
      float distance = Vector3.Distance(_origin.position, _mesh.vertices[i]);

      if (distance > _maxDistance)
        continue;

      Indexes.Add(i);
      Offsets.Add(_mesh.vertices[i] - _origin.position);
    }
  }
}
