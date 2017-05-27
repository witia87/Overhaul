using Assets.Flux;
using UnityEngine;

namespace Assets.Map
{
    public class InitializeMapPayload : IPayload
    {
        public Vector3[,] Grid;
        public float UnitSize;
    }
}