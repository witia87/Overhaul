using System;
using System.Collections.Generic;
using Assets.Sight.Polygons.WallTracingStrategies;
using UnityEngine;

namespace Assets.Sight.Polygons
{
    public class PolygonsOptimizer
    {
        private readonly FieldType[,] _fields = new FieldType[128, 128];

        private readonly List<MapVector> _registeredInaccessibleEntryPoints = new List<MapVector>();
        private readonly bool[,] _wasVisited = new bool[128, 128];

        public void RegisterRectangle(Vector2[] rectangle)
        {
            var normalizedRectangle = CheckAndTransformRectangle(rectangle);
            var bottomLeft = GetBottomLeftCorner(normalizedRectangle);
            var topRight = GetTopRightCorner(normalizedRectangle);
            _registeredInaccessibleEntryPoints.Add(bottomLeft);
            for (var z = bottomLeft.z; z < topRight.z; z++)
            {
                for (var x = bottomLeft.x; x < topRight.x; x++)
                {
                    _fields[z, x] = FieldType.Inaccessible;
                }
            }
        }

        /// <returns>A collection of non intersecting polygons.</returns>
        public List<Vector2[]> GetOptimizedPolygons()
        {
            var output = new List<Vector2[]>();
            foreach (var field in _registeredInaccessibleEntryPoints)
            {
                var vertex = GetInitialVertex(field.x, field.z);
                // After finding a field (in this case its coordinates coresponds with a vertex)
                // that lays on the edge of a area, we can chec whether or not it was visited.
                if (_wasVisited[vertex.z, vertex.x])
                {
                    continue;
                }

                output.Add(GetPolygon(vertex));
            }

            return output;
        }

        /// <summary>
        ///     Method finds a vertex by going as far to the left as possible, and the as far to the bottom as possible.
        ///     That way definitely a vertex that lies on the edge of registered field is found.
        /// </summary>
        private MapVector GetInitialVertex(int x, int z)
        {
            while (_fields[z, x - 1] == FieldType.Inaccessible)
            {
                x--;
            }

            while (_fields[z - 1, x] == FieldType.Inaccessible)
            {
                z--;
            }
            return new MapVector(x, z);
        }

        /// <param name="initialVertex">Initial vertex must represent a bottom left corner</param>
        private Vector2[] GetPolygon(MapVector initialVertex)
        {
            // Algorithm can be understood as moving around the area 
            // with a right hand touching the wall allthe time

            // We start with moving right since its a bottom-left corner
            IWallTracingStrategy strategy = new RightWallTracingStrategy(initialVertex, _fields);
            var currentVertex = initialVertex;
            var i = 0;
            var output = new List<Vector2>();
            do
            {
                i++;
                output.Add(new Vector2(currentVertex.x, currentVertex.z));
                _wasVisited[currentVertex.z, currentVertex.x] = true;
                strategy = strategy.GoToNextVertex(out currentVertex);
            } while (currentVertex != initialVertex);
            return output.ToArray();
        }

        private MapVector GetBottomLeftCorner(MapVector[] rectangle)
        {
            var bottomLeft = rectangle[0];
            for (var i = 1; i < 4; i++)
            {
                if (bottomLeft.x > rectangle[i].x)
                {
                    bottomLeft.x = rectangle[i].x;
                }
                if (bottomLeft.z > rectangle[i].z)
                {
                    bottomLeft.z = rectangle[i].z;
                }
            }
            return bottomLeft;
        }

        private MapVector GetTopRightCorner(MapVector[] rectangle)
        {
            var topRight = rectangle[0];
            for (var i = 1; i < 4; i++)
            {
                if (topRight.x < rectangle[i].x)
                {
                    topRight.x = rectangle[i].x;
                }
                if (topRight.z < rectangle[i].z)
                {
                    topRight.z = rectangle[i].z;
                }
            }
            return topRight;
        }

        private MapVector[] CheckAndTransformRectangle(Vector2[] rectangle)
        {
            if (rectangle.Length != 4)
            {
                throw new ApplicationException("Registered polygon is not a rectangle.");
            }
            var output = new MapVector[rectangle.Length];
            for (var i = 0; i < rectangle.Length; i++)
            {
                var x = Mathf.RoundToInt(rectangle[i].x);
                var z = Mathf.RoundToInt(rectangle[i].y);
                if (Mathf.Abs(x - rectangle[i].x) + Mathf.Abs(z - rectangle[i].y) > 0.00001)
                {
                    throw new ApplicationException("Sight blockers vertices are not on the grid");
                }
                output[i] = new MapVector(x, z);
            }
            return output;
        }
    }
}