using System;

namespace Assets.Gui.Sight.Polygons
{
    public struct MapVector : IEquatable<MapVector>
    {
        public bool Equals(MapVector other)
        {
            return x == other.x && z == other.z;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is MapVector && Equals((MapVector) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (x * 397) ^ z;
            }
        }

        public int x;
        public int z;

        public MapVector(int x, int z)
        {
            this.x = x;
            this.z = z;
        }

        public static MapVector operator +(MapVector b, MapVector c)
        {
            return new MapVector(b.x + c.x, b.z + c.z);
        }

        public static bool operator ==(MapVector b, MapVector c)
        {
            return b.x == c.x && b.z == c.z;
        }

        public static bool operator !=(MapVector b, MapVector c)
        {
            return !(b == c);
        }
    }
}