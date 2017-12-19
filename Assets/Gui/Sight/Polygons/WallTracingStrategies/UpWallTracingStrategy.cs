using System;

namespace Assets.Gui.Sight.Polygons.WallTracingStrategies
{
    public class UpWallTracingStrategy : IWallTracingStrategy
    {
        private MapVector _fieldPosition;
        private readonly FieldType[,] _fields;

        public UpWallTracingStrategy(MapVector fieldPosition, FieldType[,] fields)
        {
            _fieldPosition = fieldPosition;
            _fields = fields;
        }

        public IWallTracingStrategy GoToNextVertex(out MapVector foundVertex)
        {
            do
            {
                _fieldPosition.z += 1;
                if (_fields[_fieldPosition.z, _fieldPosition.x + 1] == FieldType.Inaccessible
                ) // we've lost touch of the wall (inaccessible is the interior)
                {
                    foundVertex = new MapVector(_fieldPosition.x + 1, _fieldPosition.z);
                    return new RightWallTracingStrategy(new MapVector(_fieldPosition.x + 1, _fieldPosition.z),
                        _fields);
                }
                if (_fields[_fieldPosition.z, _fieldPosition.x] != FieldType.Inaccessible
                ) // we've hit a wall with a face
                {
                    foundVertex = new MapVector(_fieldPosition.x + 1, _fieldPosition.z);
                    return new LeftWallTracingStrategy(new MapVector(_fieldPosition.x, _fieldPosition.z - 1), _fields);
                }
            } while (_fieldPosition.z < 128);

            throw new ApplicationException("Area is not inside a provided space.");
        }
    }
}