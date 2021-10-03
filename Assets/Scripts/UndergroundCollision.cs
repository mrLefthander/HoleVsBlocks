using UnityEngine;

public class UndergroundCollision : MonoBehaviour
{
  private void OnTriggerEnter(Collider other)
  {
    string tag = other.tag;

    if (tag.Equals("Objects"))
    {
      Debug.Log("Object");
    }
    if (tag.Equals("Obstacles"))
    {
      Debug.Log("Obstacle");
    }
  }
}
