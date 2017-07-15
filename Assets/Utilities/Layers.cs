namespace Assets.Utilities
{
    public static class Layers
    {
        public static int BuiltinLayer0 = 1 << 0;
        public static int BuiltinLayer1 = 1 << 1;
        public static int BuiltinLayer2 = 1 << 2;
        public static int BuiltinLayer3 = 1 << 3;
        public static int BuiltinLayer4 = 1 << 4;
        public static int BuiltinLayer5 = 1 << 5;
        public static int BuiltinLayer6 = 1 << 6;
        public static int BuiltinLayer7 = 1 << 7;

        public static int Floor = 1 << 8;
        public static int Wall = 1 << 9;
        public static int WallTransparent = 1 << 10;
        public static int MovementModule = 2 << 11;
        public static int TargetingModule = 1 << 12;
        public static int GunModule = 1 << 13;
        public static int MovementModuleDetector = 1 << 14;
        public static int TargetingModuleDetector = 1 << 15;
        public static int GunModuleDetector = 1 << 16;
        public static int Bullet = 1 << 17;
    }
}