using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

[CreateAssetMenu(fileName = "GlobalData", menuName = "GameData/GlobalData")]
public class GDC : ScriptableObject
{
    [SerializeField] private AssetLabelReference dataLabel;
    private readonly Dictionary<Type, IDataContainer> _dataContainers = new();
    private AsyncOperationHandle _handle;

    private void OnEnable() { G.CacheData(this); }

    public IEnumerator Initialize()
    {
        _dataContainers.Clear();
        _handle = Addressables.LoadAssetsAsync<ScriptableObject>(dataLabel, so =>
        { if (so is not IDataContainer container) throw new Exception($"Only DataContainer is supported under label {dataLabel.labelString}");
            _dataContainers.Add(container.GetAssetType, container); });
        yield return _handle;
        if (_handle.Status != AsyncOperationStatus.Succeeded) throw _handle.OperationException;
        foreach (var container in _dataContainers.Values) yield return container.Initialize();
    }

    public DataContainer<T> GetData<T>() where T : Object => (DataContainer<T>)_dataContainers[typeof(T)];

    public T GetAsset<T>(string assetName) where T : Object 
    { var container = (DataContainer<T>)_dataContainers[typeof(T)]; return container.GetAsset(assetName); }

    public void Dispose() { _dataContainers.Clear(); _handle.Release(); }
}

public abstract class DataContainer<T> : ScriptableObject, IDataContainer where T : Object
{
    public Type GetAssetType => typeof(T);
    [SerializeField] protected AssetLabelReference assetLabel;
    protected readonly List<T> Assets = new();
    protected AsyncOperationHandle Handle;
    
    public virtual IEnumerator Initialize()
    {
        Handle = Addressables.LoadAssetsAsync<T>(assetLabel, asset => Assets.Add(asset));
        yield return Handle;
        if (Handle.Status != AsyncOperationStatus.Succeeded) throw Handle.OperationException;
    }

    public T GetAsset(string key) { return Assets.FirstOrDefault(asset => asset.name == key); }
    public List<T> GetAssets() => Assets;

    public void Dispose() { Assets.Clear(); Handle.Release(); }
}

public interface IDataContainer { public Type GetAssetType { get; } public IEnumerator Initialize(); }