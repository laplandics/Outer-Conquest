using UnityEngine;

public static class ColliderCopier
{
    public static Collider CopyCollider(Collider source, GameObject target)
    {
        var type = source.GetType();
        var copy = target.AddComponent(type) as Collider;
        if (copy == null) return null;
        copy.isTrigger = source.isTrigger;
        copy.material  = source.material;
        switch (source)
        {
            case BoxCollider b when copy is BoxCollider cb:
                cb.center = b.center;
                cb.size   = b.size;
                break;
            case SphereCollider s when copy is SphereCollider cs:
                cs.center = s.center;
                cs.radius = s.radius;
                break;
            case CapsuleCollider c when copy is CapsuleCollider cc:
                cc.center    = c.center;
                cc.radius    = c.radius;
                cc.height    = c.height;
                cc.direction = c.direction;
                break;
            case MeshCollider m when copy is MeshCollider cm:
                cm.sharedMesh = m.sharedMesh;
                cm.convex     = m.convex;
                break;
        }
        return copy;
    }
}