using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class StartupLoader : MonoBehaviour
{
  [SerializeField] private AssetReference _coreScene;

  private void Awake()
  {
    Addressables.LoadSceneAsync(_coreScene, LoadSceneMode.Single);
  }
}
