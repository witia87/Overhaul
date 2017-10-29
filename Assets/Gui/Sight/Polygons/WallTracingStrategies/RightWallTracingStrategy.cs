using System;

namespace Assets.Sight.Polygons.WallTracingStrategies
{
    public class RightWallTracingStrategy : IWallTracingStrategy
    {
        private readonly FieldType[,] _fields;
        private MapVector _vertexPosition;

        public RightWallTracingStrategy(MapVector vertexVertexPosition, FieldType[,] fields)
        {
            _vertexPosition = vertexVertexPosition;
            _fields = fields;
        }

        public IWallTracingStrategy GoToNextVertex(out MapVector foundVertex)
        {
            do
            {
                _vertexPosition.x += 1;
                if (_fields[_vertexPosition.z - 1, _vertexPosition.x] == FieldType.Inaccessible
                ) // we've lost touch of the wall (inaccessible is the interior)
                {
                    foundVertex = _vertexPosition;
                    return new DownWallTracingStrategy(new MapVector(_vertexPosition.x, _vertexPosition.z - 1),
                        _fields);
                }
                if (_fields[_vertexPosition.z, _vertexPosition.x] != FieldType.Inaccessible
                ) // we've hit a wall with a face
                {
                    foundVertex = _vertexPosition;
                    return new UpWallTracingStrategy(new MapVector(_vertexPosition.x - 1, _vertexPosition.z),
                        _fields);
                }
            } while (_vertexPosition.x < 128);

            throw new ApplicationException("Area is not inside a provided space.");
        }
    }
}