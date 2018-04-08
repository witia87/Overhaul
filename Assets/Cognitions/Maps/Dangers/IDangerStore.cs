using UnityEngine;

namespace Assets.Cognitions.Maps.Dangers
{
    public interface IDangerStore
    {
        void RegisterLineOfFire(Vector3 startPosition, Vector3 direction, float time);
    }
}