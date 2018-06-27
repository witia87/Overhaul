using UnityEngine;

namespace Assets.Cognitions.Maps.Paths
{
    public interface IPath
    {
        /// <summary>
        /// Points to the next position to go toward to be on the path
        /// </summary>
        Vector3 PositionToGo { get; }

        /// <summary>
        /// Informs whether path is still valid for the current position,
        /// or should the next path be requested. 
        /// PositionToGo still contains an aproximation though.
        /// </summary>
        bool IsPathStillValid { get; }
    }
}