using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Fader: MonoBehaviour
{
  [SerializeField] private float _fadeTime = 1f;

  [SerializeField] private Image _fadePanel;
  [SerializeField] private SceneLoadChannelSO _sceneLoadEventChannel;

  private void Start()
  {
    FadeOnLevelLoadEvent();
  }

  private void OnEnable()
  {
    _sceneLoadEventChannel.OnNextLevelRequested += FadeOnLevelLoadEvent;
  }

  private void OnDisable()
  {
    _sceneLoadEventChannel.OnNextLevelRequested -= FadeOnLevelLoadEvent;
  }

  private void FadeOnLevelLoadEvent()
  {
    _fadePanel.DOFade(0f, _fadeTime).From(1f).SetEase(Ease.InExpo);
  }
}
