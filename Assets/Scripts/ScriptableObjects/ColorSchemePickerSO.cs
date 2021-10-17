using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Color Scheme Picker", menuName = "Game/Color Scheme Picker")]
public class ColorSchemePickerSO : ScriptableObject
{
  [SerializeField] private List<ColorSchemeSO> _colorSchemes;
  [SerializeField] private SceneLoadChannelSO _sceneLoadEventChannel;

  public ColorSchemeSO Current { get; private set; }

  private void OnEnable()
  {
    UpdateColorScheme();
    _sceneLoadEventChannel.OnNextLevelRequested += UpdateColorScheme;
  }

  private void OnDisable()
  {
    _sceneLoadEventChannel.OnNextLevelRequested -= UpdateColorScheme;
  }

  private void UpdateColorScheme()
  {
    Current = _colorSchemes[Random.Range(0, _colorSchemes.Count)];
  }
}
