using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[CreateAssetMenu(fileName = "ManagersDC", menuName = "GameData/Managers")]
public class ManagersDC : DataContainer<SceneManager>
{
    public override IEnumerator Initialize()
    {
       Handle = Addressables.LoadAssetsAsync<GameObject>(assetLabel, asset => Assets.Add(asset.GetComponent<SceneManager>()));
        yield return Handle;
        if (Handle.Status != AsyncOperationStatus.Succeeded) throw Handle.OperationException;
    }
}