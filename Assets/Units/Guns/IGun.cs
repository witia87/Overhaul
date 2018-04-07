using UnityEngine;

namespace Assets.Units.Guns
{
    public interface IGun
    {
        Vector3 Position { get; }
        Vector3 Direction { get; }
    }
}