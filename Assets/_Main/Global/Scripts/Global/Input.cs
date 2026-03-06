namespace G
{
    public static class Input
    {
        public static GameInputs Get { get; private set; }

        public static void Set() => Get = new GameInputs();
        public static void Enable() => Get.Enable();
        public static void Disable() => Get.Disable();
        public static void Clear() => Get = null;
    }
}