using System.Collections.Generic;
using Assets.Cognitions.Maps.MapGrids.Nodes;
using Assets.Environment;
using Assets.Environment.Guns.Bullets;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Cognitions.Maps.Dangers
{
    public class DangerStore
    {
        private const float DefaultDangerTime = 0.5f;
        private readonly BaseNode[,] _baseGrid;
        private FractionId _fractionId;
        private readonly List<LineOfFire> _registeredLines = new List<LineOfFire>();

        public DangerStore(BaseNode[,] baseGrid, BulletsFactory[] registeredBulletsFactories,
            FractionId fractionId)
        {
            _baseGrid = baseGrid;
            _fractionId = fractionId;
            foreach (var bulletsFactory in registeredBulletsFactories)
            {
                bulletsFactory.HasCreatedBullet += RegisterLineOfFire;
            }
        }

        public void RegisterLineOfFire(Vector3 startPosition, Vector3 direction, FractionId fractionId)
        {
            if (fractionId == _fractionId)
            {
                _registeredLines.Add(new LineOfFire(_baseGrid, startPosition, direction, DefaultDangerTime));
                _registeredLines[_registeredLines.Count - 1].Register();
            }
        }

        public void Update()
        {
            for (var i = 0; i < _registeredLines.Count; i++)
            {
                if (_registeredLines[i].IsFinished)
                {
                    _registeredLines[i].Unregister();
                    _registeredLines.RemoveAt(i);
                    i--;
                }
            }
        }

        public void OnDrawGizmos()
        {
            var offset = new Vector3(0.2f, 0, 0.2f);
            var offset2 = new Vector3(-0.2f, 0, 0.2f);
            for (var z = 0; z < _baseGrid.GetLength(0); z++)
            {
                for (var x = 0; x < _baseGrid.GetLength(1); x++)
                {
                    if (_baseGrid[z, x] != null)
                    {
                        if (_baseGrid[z, x].IsDangerous)
                        {
                            DrawArrow.ForDebug(_baseGrid[z, x].Position - offset,
                                2 * offset, Color.red, 0.1f, 0);
                            DrawArrow.ForDebug(_baseGrid[z, x].Position - offset2,
                                2 * offset2, Color.red, 0.1f, 0);
                        }
                        else if (_baseGrid[z, x].IsCovered(Vector3.right) ||
                                 _baseGrid[z, x].IsCovered(Vector3.left) ||
                                 _baseGrid[z, x].IsCovered(Vector3.forward) ||
                                 _baseGrid[z, x].IsCovered(Vector3.back))
                        {
                            DrawArrow.ForDebug(_baseGrid[z, x].Position - offset,
                                2 * offset, Color.green, 0.1f, 0);
                            DrawArrow.ForDebug(_baseGrid[z, x].Position - offset2,
                                2 * offset2, Color.green, 0.1f, 0);
                        }
                    }
                }
            }
        }
    }
}