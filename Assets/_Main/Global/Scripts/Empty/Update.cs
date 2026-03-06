using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace G
{
    public static class Update
    {
        private static Empty _empty;

        public static void Launch()
        {
            _empty = Object.FindFirstObjectByType<Empty>();
            if (_empty != null) return;
            _empty = new GameObject().AddComponent<Empty>();
            _empty.Initialize();
        }
        
        public static void SetNewUpdate(Action update)
        { _empty.SetNewUpdate(update, Empty.UpdateType.Update); }
        
        public static void SetNewLateUpdate(Action update)
        {_empty.SetNewUpdate(update, Empty.UpdateType.LateUpdate); }
        
        public static void Forget(Action update) { _empty.RemoveUpdate(update); }

        public static void Destroy()
        { if (_empty == null) return; Object.Destroy(_empty.gameObject); _empty = null; }
    }
}