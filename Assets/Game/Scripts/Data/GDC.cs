using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[CreateAssetMenu(fileName = "GlobalData", menuName = "GameData/GlobalData")]
public class GDC : ScriptableObject
{
    [SerializeField] private AssetLabelReference dataLabel;
    private Dictionary<Type, DataContainer> _dataContainers = new();

    public IEnumerator Initialize()
    {
        var handle = Addressables.LoadAssetsAsync<ScriptableObject>(dataLabel, so =>
        {
            if (so is not DataContainer dataContainer)
                throw new Exception($"Only DataContainers are allowed under label {dataLabel.labelString}");
            _dataContainers.Add(dataContainer.GetAssetType(), dataContainer);
        });
        yield return handle;
        if (handle.Status != AsyncOperationStatus.Succeeded) throw handle.OperationException;
    }

    public T GetData<T>() where T : DataContainer
    {
        foreach (var dataContainer in _dataContainers.Values)
        { if (dataContainer is T rightContainer) return rightContainer; }
        throw new KeyNotFoundException($"Data {typeof(T)} not found");
    }
}

public abstract class DataContainer : ScriptableObject { public abstract Type GetAssetType(); }