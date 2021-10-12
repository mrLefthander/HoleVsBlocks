using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

public class LevelsLocationProvider
{
  private AssetLabelReference _levelsLabel;
  private IList<IResourceLocation> _levelsLocations;

  public LevelsLocationProvider(AssetLabelReference levelsLabel)
  {
    _levelsLabel = levelsLabel;
    _levelsLocations = new List<IResourceLocation>();
  }

  public async void Init()
  {
    await GetLocations(_levelsLabel, _levelsLocations);
  }

  public IResourceLocation GetLevelLocation(int levelIndex)
  {
    return _levelsLocations[levelIndex];
  }

  public bool IsLastLevel(int levelIndex)
  {
    return levelIndex == (_levelsLocations.Count - 1);
  }

  private async Task GetLocations(AssetLabelReference label, IList<IResourceLocation> locations)
  {
    var locationsHandle = await Addressables.LoadResourceLocationsAsync(label).Task;

    foreach (var location in locationsHandle)
      locations.Add(location);

    Addressables.Release(locationsHandle);
  }
}
