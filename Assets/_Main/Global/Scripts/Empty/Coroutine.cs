using System.Collections;
using UnityEngine;

namespace G
{
    public static class Coroutine
    {
        private static Empty _empty;

        public static void Launch()
        {
            _empty = Object.FindFirstObjectByType<Empty>();
            if (_empty != null) return;
            _empty = new GameObject().AddComponent<Empty>();
            _empty.Initialize();
        }
        
        public static UnityEngine.Coroutine Start(IEnumerator routine)
        { var coroutine = _empty.StartCoroutine(routine); return coroutine; }

        public static void Stop(UnityEngine.Coroutine routine) { _empty.StopCoroutine(routine); }

        public static void Destroy()
        { if (_empty == null) return; Object.Destroy(_empty.gameObject); _empty = null; }
    }
}