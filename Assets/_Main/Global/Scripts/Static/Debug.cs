using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace G
{
    public static class Debug
    {
        private static DebugEmpty _empty;
        
        public static void Launch()
        { _empty = new GameObject().AddComponent<DebugEmpty>(); _empty.Initialize(); }

        public static void OnDrawGizmosAdd(Action action) { _empty.GizmosActions.Add(action); }
        public static void OnDrawGizmosRemove(Action action) { _empty.GizmosActions.Remove(action); }
        
        public static void Destroy()
        { if (_empty == null) return; Object.Destroy(_empty.gameObject); _empty = null; }
    }
}