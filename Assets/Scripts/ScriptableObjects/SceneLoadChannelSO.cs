using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SceneLoadChannel", menuName = "EventChannels/Scene Load Channel")]
public class SceneLoadChannelSO : ScriptableObject
{
	public event UnityAction OnGameStartRequested;
	public event UnityAction OnNextLevelRequested;
	public event UnityAction OnGameEndRequested;

  public void RaiseGameStartEvent()
	{
		OnGameStartRequested?.Invoke();
	}

	public void RaiseNextLevelEvent()
  {
		OnNextLevelRequested?.Invoke();
	}

	public void RaiseGameEndEvent()
	{
		OnGameEndRequested?.Invoke();
	}
}
