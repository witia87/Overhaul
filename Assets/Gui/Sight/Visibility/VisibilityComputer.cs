using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Gui.Sight.Visibility
{
    /// <summary>
    ///     Class which computes a mesh that represents which regions are
    ///     visibile from the origin point given a set of occluders
    /// </summary>
    public class VisibilityComputer
    {
        // These represent the map and light location:        
        private readonly List<EndPoint> _endpoints;

        private readonly EndPointComparer _radialComparer;

        private readonly List<Segment> _segments;

        public VisibilityComputer()
        {
            _segments = new List<Segment>();
            _endpoints = new List<EndPoint>();
            _radialComparer = new EndPointComparer();
        }

        /// <summary>
        ///     The origin, or position of the observer
        /// </summary>
        public Vector2 Origin { get; set; }

        /// <summary>
        ///     The maxiumum view distance
        /// </summary>
        public float Radius { get; set; }

        // Add a segment, where the first point shows up in the
        // visualization but the second one does not. (Every endpoint is
        // part of two _segments, but we want to only show them once.)
        public void AddSegment(Vector2 p1, Vector2 p2)
        {
            var segment = new Segment();
            var endPoint1 = new EndPoint();
            var endPoint2 = new EndPoint();

            endPoint1.Position = p1;
            endPoint1.Segment = segment;

            endPoint2.Position = p2;
            endPoint2.Segment = segment;

            segment.P1 = endPoint1;
            segment.P2 = endPoint2;

            _segments.Add(segment);
            _endpoints.Add(endPoint1);
            _endpoints.Add(endPoint2);
        }

        // Processess _segments so that we can sort them later
        private void UpdateSegments()
        {
            foreach (var segment in _segments)
            {
                // NOTE: future optimization: we could record the quadrant
                // and the y/x or x/y ratio, and sort by (quadrant,
                // ratio), instead of calling atan2. See
                // <https://github.com/mikolalysenko/compare-slope> for a
                // library that does this.

                segment.P1.Angle = (float) Math.Atan2(segment.P1.Position.y - Origin.y,
                    segment.P1.Position.x - Origin.x);
                segment.P2.Angle = (float) Math.Atan2(segment.P2.Position.y - Origin.y,
                    segment.P2.Position.x - Origin.x);

                // Map angle between -Pi and Pi
                var dAngle = segment.P2.Angle - segment.P1.Angle;
                if (dAngle <= -Mathf.PI)
                {
                    dAngle += 2 * Mathf.PI;
                }
                if (dAngle > Mathf.PI)
                {
                    dAngle -= 2 * Mathf.PI;
                }

                segment.P1.Begin = dAngle > 0.0f;
                segment.P2.Begin = !segment.P1.Begin;
            }
        }

        // Helper: do we know that segment a is in front of b?
        // Implementation not anti-symmetric (that is to say,
        // _segment_in_front_of(a, b) != (!_segment_in_front_of(b, a)).
        // Also note that it only has to work in a restricted set of cases
        // in the visibility algorithm; I don't think it handles all
        // cases. See http://www.redblobgames.com/articles/visibility/segment-sorting.html
        private bool SegmentInFrontOf(Segment a, Segment b, Vector2 relativeTo)
        {
            // NOTE: we slightly shorten the _segments so that
            // intersections of the _endpoints (common) don't count as
            // intersections in this algorithm                        

            var a1 = VectorMath.LeftOf(a.P2.Position, a.P1.Position,
                VectorMath.Interpolate(b.P1.Position, b.P2.Position, 0.01f));
            var a2 = VectorMath.LeftOf(a.P2.Position, a.P1.Position,
                VectorMath.Interpolate(b.P2.Position, b.P1.Position, 0.01f));
            var a3 = VectorMath.LeftOf(a.P2.Position, a.P1.Position, relativeTo);

            var b1 = VectorMath.LeftOf(b.P2.Position, b.P1.Position,
                VectorMath.Interpolate(a.P1.Position, a.P2.Position, 0.01f));
            var b2 = VectorMath.LeftOf(b.P2.Position, b.P1.Position,
                VectorMath.Interpolate(a.P2.Position, a.P1.Position, 0.01f));
            var b3 = VectorMath.LeftOf(b.P2.Position, b.P1.Position, relativeTo);

            // NOTE: this algorithm is probably worthy of a short article
            // but for now, draw it on paper to see how it works. Consider
            // the line A1-A2. If both B1 and B2 are on one side and
            // relativeTo is on the other side, then A is in between the
            // viewer and B. We can do the same with B1-B2: if A1 and A2
            // are on one side, and relativeTo is on the other side, then
            // B is in between the viewer and A.
            if (b1 == b2 && b2 != b3)
            {
                return true;
            }
            if (a1 == a2 && a2 == a3)
            {
                return true;
            }
            if (a1 == a2 && a2 != a3)
            {
                return false;
            }
            if (b1 == b2 && b2 == b3)
            {
                return false;
            }

            // If A1 != A2 and B1 != B2 then we have an intersection.
            // Expose it for the GUI to show a message. A more robust
            // implementation would split _segments at intersections so
            // that part of the segment is in front and part is behind.

            //demo_intersectionsDetected.push([a.p1, a.p2, b.p1, b.p2]);
            return false;

            // NOTE: previous implementation was a.d < b.d. That's simpler
            // but trouble when the _segments are of dissimilar sizes. If
            // you're on a grid and the _segments are similarly sized, then
            // using distance will be a simpler and faster implementation.
        }

        /// <summary>
        ///     Computes the visibility polygon and returns the vertices
        ///     of the triangle fan (minus the center vertex)
        /// </summary>
        public List<Vector2> Compute()
        {
            var output = new List<Vector2>();
            var open = new LinkedList<Segment>();

            UpdateSegments();

            _endpoints.Sort(_radialComparer);

            float currentAngle = 0;

            // At the beginning of the sweep we want to know which
            // _segments are active. The simplest way to do this is to make
            // a pass collecting the _segments, and make another pass to
            // both collect and process them. However it would be more
            // efficient to go through all the _segments, figure out which
            // ones intersect the initial sweep line, and then sort them.
            for (var pass = 0; pass < 2; pass++)
            {
                foreach (var p in _endpoints)
                {
                    var currentOld = open.Count == 0 ? null : open.First.Value;

                    if (p.Begin)
                    {
                        // Insert into the right place in the list
                        var node = open.First;
                        while (node != null && SegmentInFrontOf(p.Segment, node.Value, Origin))
                        {
                            node = node.Next;
                        }

                        if (node == null)
                        {
                            open.AddLast(p.Segment);
                        }
                        else
                        {
                            open.AddBefore(node, p.Segment);
                        }
                    }
                    else
                    {
                        open.Remove(p.Segment);
                    }


                    Segment currentNew = null;
                    if (open.Count != 0)
                    {
                        currentNew = open.First.Value;
                    }

                    if (currentOld != currentNew)
                    {
                        if (pass == 1)
                        {
                            AddTriangle(output, currentAngle, p.Angle, currentOld);
                        }
                        currentAngle = p.Angle;
                    }
                }
            }

            return output;
        }

        private void AddTriangle(List<Vector2> triangles, float angle1, float angle2, Segment segment)
        {
            var p1 = Origin;
            var p2 = new Vector2(Origin.x + (float) Math.Cos(angle1), Origin.y + (float) Math.Sin(angle1));
            var p3 = Vector2.zero;
            var p4 = Vector2.zero;

            if (segment != null)
            {
                // Stop the triangle at the intersecting segment
                p3.x = segment.P1.Position.x;
                p3.y = segment.P1.Position.y;
                p4.x = segment.P2.Position.x;
                p4.y = segment.P2.Position.y;
            }
            else
            {
                // Stop the triangle at a fixed distance; this probably is
                // not what we want, but it never gets used in the demo
                p3.x = Origin.x + (float) Math.Cos(angle1) * Radius * 2;
                p3.y = Origin.y + (float) Math.Sin(angle1) * Radius * 2;
                p4.x = Origin.x + (float) Math.Cos(angle2) * Radius * 2;
                p4.y = Origin.y + (float) Math.Sin(angle2) * Radius * 2;
            }

            var pBegin = VectorMath.LineLineIntersection(p3, p4, p1, p2);

            p2.x = Origin.x + (float) Math.Cos(angle2);
            p2.y = Origin.y + (float) Math.Sin(angle2);

            var pEnd = VectorMath.LineLineIntersection(p3, p4, p1, p2);

            triangles.Add(pBegin);
            triangles.Add(pEnd);
        }
    }
}