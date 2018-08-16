using UnityEngine;

namespace Assets.Cognitions.Maps.MapGraphs.Rooms.Covers
{
    public class Cover : ICover
    {
        private readonly float _length;
        private readonly Vector3 _position;
        private readonly float _width;

        public Cover(Vector3 position, float width, float length)
        {
            _position = position;
            _width = width;
            _length = length;
        }

        public Vector3 Position
        {
            get { return _position; }
        }

        public float Width
        {
            get { return _width; }
        }

        public float Length
        {
            get { return _length; }
        }
    }
}