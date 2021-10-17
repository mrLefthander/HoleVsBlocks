using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SceneLoadChannel", menuName = "EventChannels/Scene Load Channel")]
public class SceneLoadChannelSO : ScriptableObject
{
	public UnityAction OnGameStartRequested;
	public UnityAction OnNextLevelRequested;

	public void RaiseGameStartEvent()
	{
		OnGameStartRequested?.Invoke();
	}

	public void RaiseNextLevelEvent()
  {
		OnNextLevelRequested?.Invoke();
	}
}
