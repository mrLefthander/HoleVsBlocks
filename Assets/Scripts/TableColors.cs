using UnityEngine;

public class TableColors : MonoBehaviour
{
  [Header("Objects to color")]
  [SerializeField] private Material _groundMaterial;
  [SerializeField] private Material _obstacleMaterial;
  [SerializeField] private SpriteRenderer _borders;
  [SerializeField] private SpriteRenderer _front;

  [Header("Scriptable Objects")]
  [SerializeField] private SceneLoadChannelSO _sceneLoadEventChannel;
  [SerializeField] private ColorSchemePickerSO _schemePicker;

  private void Start()
  {
    ApplyColors();
    _sceneLoadEventChannel.OnNextLevelRequested += ApplyColors;
  }

  private void OnDestroy()
  {
    _sceneLoadEventChannel.OnNextLevelRequested -= ApplyColors;
  }

  private void ApplyColors()
  {
    _groundMaterial.color = _schemePicker.Current.GroundColor;
    _borders.color = _schemePicker.Current.TableBorderColor;
    _front.color = _schemePicker.Current.TableFrontColor;
    _obstacleMaterial.color = _schemePicker.Current.ObstacleColor;
  }
}
