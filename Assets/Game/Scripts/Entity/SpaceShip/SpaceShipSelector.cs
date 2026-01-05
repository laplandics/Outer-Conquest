using UnityEngine;

public class SpaceShipSelector : EntitySelector
{
    private Collider _selectionCollider;

    public override void Enable()
    {
        var visualizer = owner.GetEntityComponent<SpaceShipVisualizer>();
        var meshCollider = visualizer.GetMeshCollider();
        var newCollider = ColliderCopier.CopyCollider(meshCollider, gameObject);
        _selectionCollider = newCollider;
        _selectionCollider.enabled = true;
    }

    public override Bounds GetBounds() => _selectionCollider.bounds;
}