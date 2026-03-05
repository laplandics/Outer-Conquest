using UnityEngine;
using Object = UnityEngine.Object;

namespace G
{
    public static class Ui
    {
        private static UiEmpty _empty;

        public static void Launch()
        { _empty = new GameObject().AddComponent<UiEmpty>(); _empty.Initialize(); }

        public static T Create<T>(IUiUser user) where T : GameUi
        {
            var obj = new GameObject(typeof(T).Name);
            Object.DontDestroyOnLoad(obj);
            obj.transform.SetParent(_empty.transform, false);
            var ui = obj.AddComponent<T>();
            ui.Appear(user);
            return ui;
        }

        public static void Delete(GameUi ui)
        { ui.Disappear(); Object.Destroy(ui.gameObject); }
        
        public static void Destroy()
        { if (_empty == null) return; Object.Destroy(_empty.gameObject); _empty = null; }
    }
}