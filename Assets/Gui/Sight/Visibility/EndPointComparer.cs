﻿using System.Collections.Generic;

namespace Assets.Gui.Sight.Visibility
{
    internal class EndPointComparer : IComparer<EndPoint>
    {
        // Helper: comparison function for sorting points by angle
        public int Compare(EndPoint a, EndPoint b)
        {
            // Traverse in angle order
            if (a.Angle > b.Angle)
            {
                return 1;
            }

            if (a.Angle < b.Angle)
            {
                return -1;
            }

            // But for ties we want Begin nodes before End nodes
            if (!a.Begin && b.Begin)
            {
                return 1;
            }

            if (a.Begin && !b.Begin)
            {
                return -1;
            }

            return 0;
        }
    }
}