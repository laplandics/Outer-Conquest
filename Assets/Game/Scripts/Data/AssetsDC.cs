using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[CreateAssetMenu(fileName = "AssetsDC", menuName = "GameData/Assets")]
public class AssetsDC : DataContainer<EntityAsset>
{
    public override IEnumerator Initialize()
    {
        Handle = Addressables.LoadAssetsAsync<GameObject>(assetLabel, asset => Assets.Add(asset.GetComponent<EntityAsset>()));
        yield return Handle;
        if (Handle.Status != AsyncOperationStatus.Succeeded) throw Handle.OperationException;
    }
}