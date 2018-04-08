using UnityEngine;

namespace Assets.Modules.Guns
{
    public interface IGun
    {
        Vector3 Position { get; }
        Vector3 Direction { get; }
    }
}