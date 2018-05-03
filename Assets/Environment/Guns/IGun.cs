using UnityEngine;

namespace Assets.Environment.Guns
{
    public interface IGun
    {
        Vector3 Position { get; }
        Vector3 Direction { get; }
    }
}