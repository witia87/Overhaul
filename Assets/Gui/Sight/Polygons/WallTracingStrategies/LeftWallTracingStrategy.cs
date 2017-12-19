using System;

namespace Assets.Gui.Sight.Polygons.WallTracingStrategies
{
    public class LeftWallTracingStrategy : IWallTracingStrategy
    {
        private readonly FieldType[,] _fields;
        private MapVector _fieldPosition;

        public LeftWallTracingStrategy(MapVector fieldPosition, FieldType[,] fields)
        {
            _fieldPosition = fieldPosition;
            _fields = fields;
        }

        public IWallTracingStrategy GoToNextVertex(out MapVector foundVertex)
        {
            do
            {
                _fieldPosition.x -= 1;
                if (_fields[_fieldPosition.z + 1, _fieldPosition.x] == FieldType.Inaccessible
                ) // we've lost touch of the wall (inaccessible is the interior)
                {
                    foundVertex = new MapVector(_fieldPosition.x + 1, _fieldPosition.z + 1);
                    return new UpWallTracingStrategy(new MapVector(_fieldPosition.x, _fieldPosition.z + 1),
                        _fields);
                }
                if (_fields[_fieldPosition.z, _fieldPosition.x] != FieldType.Inaccessible
                ) // we've hit a wall with a face
                {
                    foundVertex = new MapVector(_fieldPosition.x + 1, _fieldPosition.z + 1);
                    return new DownWallTracingStrategy(new MapVector(_fieldPosition.x + 1, _fieldPosition.z),
                        _fields);
                }
            } while (_fieldPosition.x >= 0);

            throw new ApplicationException("Area is not inside a provided space.");
        }
    }
}