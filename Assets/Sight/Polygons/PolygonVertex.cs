using System;
using System.Collections.Generic;

namespace Assets.Sight.Polygons
{
    public class PolygonVertex
    {
        public List<PolygonVertex> _connectedVertices = new List<PolygonVertex>();

        public void AddConnectedVertex(PolygonVertex vertex)
        {
            if (_connectedVertices.Count > 2)
            {
                throw new ApplicationException("Vertex has to be connected to exactly two other vertices");
            }

            if (_connectedVertices.Contains(vertex))
            {
                throw new ApplicationException("Vertex already connected");
            }

            _connectedVertices.Add(vertex);
        }
    }
}