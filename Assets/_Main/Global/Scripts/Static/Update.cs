using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace G
{
    public static class Update
    {
        private static UpdateEmpty _empty;

        public static void Launch()
        { _empty = new GameObject().AddComponent<UpdateEmpty>(); _empty.Initialize(); }
        
        public static void SetNewUpdate(Action update)
        { _empty.SetNewUpdate(update, UpdateEmpty.UpdateType.Update); }
        
        public static void SetNewLateUpdate(Action update)
        {_empty.SetNewUpdate(update, UpdateEmpty.UpdateType.LateUpdate); }
        
        public static void Forget(Action update) { _empty.RemoveUpdate(update); }

        public static void Destroy()
        { if (_empty == null) return; Object.Destroy(_empty.gameObject); _empty = null; }
    }
}