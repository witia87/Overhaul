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

        public static int Map = 1 << 8;
        public static int MapTransparent = 1 << 9;
        public static int Environment = 1 << 10;
        public static int EnvironmentTransparent = 2 << 11;
        public static int Structure = 1 << 12;
        public static int StructureTransparent = 1 << 13;
        public static int Organism = 1 << 14;
        public static int OrganismTransparent = 1 << 15;
        public static int Floor = 1 << 16;
        public static int Guns = 1 << 17;
    }
}