using UnityEngine;

public class SpaceShipVisualizer : EntityVisualizer
{
    [SerializeField] private SpaceShipVisualizerAsset visualAsset;
    private SpaceShipData _data;
        
    public override void Initialize(Entity newOwner)
    {
        owner = newOwner;
        _data = owner.GetData<SpaceShipData>();
        var spaceShipName = $"{_data.spaceShipType.ToString()}{_data.spaceShipClass.ToString()}";
        var assetPrefab = G.Data().GetAsset<EntityAsset>(spaceShipName);
        visualAsset = Spawner.Spawn(assetPrefab, Vector3.zero, Quaternion.identity).GetComponent<SpaceShipVisualizerAsset>();
        visualAsset.transform.SetParent(transform, false);
    }

    public Collider GetMeshCollider() => visualAsset.modelCollider;
}