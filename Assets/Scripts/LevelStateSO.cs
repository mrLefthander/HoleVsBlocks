using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level State", menuName = "Game/Level State")]
public class LevelStateSO : ScriptableObject
{
  public int TotalObjectivesCount;
  public int CurrentObjectivesCount;
  public int CurrentLevelNumber;

  public void InitializeNewLevel(int objectivesCount, int levelNumber)
  {
    TotalObjectivesCount = objectivesCount;
    CurrentObjectivesCount = objectivesCount;
    CurrentLevelNumber = levelNumber;
  }
  private void OnObjectiveDestroyed()
  {
    CurrentObjectivesCount--;
    CheckLevelWin();
  }

  private void CheckLevelWin()
  {
    if(CurrentObjectivesCount <= 0)
    {
      Debug.Log("LevelWin!");
    }
  }

  private void OnEnable()
  {
    UndergroundCollision.ObjectiveDestroyed += OnObjectiveDestroyed;
  }

  private void OnDestroy()
  {
    UndergroundCollision.ObjectiveDestroyed -= OnObjectiveDestroyed;
  }


}
