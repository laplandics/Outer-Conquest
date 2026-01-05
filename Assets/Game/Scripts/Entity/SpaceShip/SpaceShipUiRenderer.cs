using UnityEngine;

public class SpaceShipUiRenderer : EntityUiRenderer
{
    private SpaceShipData _data;
    private SpaceShipUiAsset _uiAsset;

    public override void Enable()
    {
        _data = owner.GetData<SpaceShipData>();
        var assetName = $"{nameof(SpaceShipUiAsset)}{_data.spaceShipType.ToString()}{_data.spaceShipClass.ToString()}";
        _uiAsset = G.Data().GetAsset<EntityAsset>(assetName) as SpaceShipUiAsset;
    }

    public override GameObject GetSelectionUi() => _uiAsset.selectionUiPrefab;

    public override GameObject GetSelectionMemberUi() => _uiAsset.selectionMemberUiPrefab;
}