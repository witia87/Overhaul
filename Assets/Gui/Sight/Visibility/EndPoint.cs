using UnityEngine;

namespace Assets.Gui.Sight.Visibility
{
    /// <summary>
    ///     The end-point of a segment
    /// </summary>
    internal class EndPoint
    {
        internal EndPoint()
        {
            Position = Vector2.zero;
            Begin = false;
            Segment = null;
            Angle = 0;
        }

        /// <summary>
        ///     Center of the segment
        /// </summary>
        internal Vector2 Position { get; set; }

        /// <summary>
        ///     If this end-point is a begin or end end-point
        ///     of a segment (each segment has only one begin and one end end-point
        /// </summary>
        internal bool Begin { get; set; }

        /// <summary>
        ///     The segment this end-point belongs to
        /// </summary>
        internal Segment Segment { get; set; }

        /// <summary>
        ///     The angle of the end-point relative to the location of the visibility test
        /// </summary>
        internal float Angle { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is EndPoint)
            {
                var other = (EndPoint) obj;

                return Position.Equals(other.Position) &&
                       Begin.Equals(other.Begin) &&
                       Angle.Equals(other.Angle);

                // We do not care about the segment beeing the same 
                // since that would create a circular reference
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode() +
                   Begin.GetHashCode() +
                   Angle.GetHashCode();
        }

        public override string ToString()
        {
            return "{ p:" + Position + "a: " + Angle + " in " + Segment + "}";
        }
    }
}