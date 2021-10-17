using UnityEngine;

[CreateAssetMenu(fileName = "Color Scheme", menuName = "Game/Color Scheme")]
public class ColorSchemeSO: ScriptableObject
{
  [SerializeField] private Color _mainColor;

  public Color GroundColor => _mainColor;
  public Color TableBorderColor => _mainColor + _shadeStep * 2;
  public Color TableFrontColor => _mainColor - _shadeStep;
  public Color ObstacleColor => CalculateAnalogousColor(_mainColor);

  private Color _shadeStep;

  private void OnEnable()
  {
    _shadeStep = new Color32(16, 16, 16, 0);
  }

  private Color CalculateAnalogousColor(Color mainColor)
  {
    float h;
    float s;
    float v;
    float deg = 60f / 360f;
    Color.RGBToHSV(mainColor, out h, out s, out v);
    h += deg;
    return Color.HSVToRGB(h, s, v);
  }

}
