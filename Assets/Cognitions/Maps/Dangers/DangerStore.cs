using System.Collections.Generic;
using Assets.Cognitions.Maps.Nodes;
using Assets.Modules.Guns.Bullets;
using Assets.Resources;
using Assets.Utilities;
using UnityEngine;

namespace Assets.Cognitions.Maps.Dangers
{
    public class DangerStore
    {
        private readonly BaseNode[,] _baseGrid;
        private readonly List<LineOfFire> _registeredLines = new List<LineOfFire>();
        private const float DefaultDangerTime = 0.1f;

        public DangerStore(BaseNode[,] baseGrid, BulletsFactory[] registeredBulletsFactories)
        {
            _baseGrid = baseGrid;
            foreach (var bulletsFactory in registeredBulletsFactories)
            {
                bulletsFactory.HasCreatedBullet += RegisterLineOfFire;
            }
        }

        public void RegisterLineOfFire(Vector3 startPosition, Vector3 direction)
        {
            _registeredLines.Add(new LineOfFire(_baseGrid, startPosition, direction, DefaultDangerTime));
            _registeredLines[_registeredLines.Count - 1].Register();
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
                            Gizmos.color = Color.red;
                            //Gizmos.DrawCube(_baseGrid[z, x].Center, new Vector3(1,0.1f,1));
                            DrawArrow.ForDebug(_baseGrid[z, x].Position - offset,
                                2 * offset, Color.red, 0.1f, 0);
                            DrawArrow.ForDebug(_baseGrid[z, x].Position - offset2,
                                2 * offset2, Color.red, 0.1f, 0);
                        }
                    }
                }
            }
        }
    }
}