using Assets.Cognitions.Maps.Covers;
using Assets.Cognitions.Maps.MapGrids;
using UnityEngine;

namespace Assets.Cognitions.Maps.MapGraphs.Rooms
{
    public class RoomInitializer : MonoBehaviour
    {
        public float Width;
        public float Height;

        private void Initialize(MapGrid grid, MapGraph graph)
        {
            var entrances = GetComponentsInChildren<RoomEntrance>();
            var covers = GetComponentsInChildren<Cover>();
            

            Destroy(gameObject);
        }

        public void Update()
        {
        }

        public void OnDrawGizmos()
        {
            /*var offset = new Vector3(0.2f, 0, 0.2f);
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
                        else if(_baseGrid[z, x].IsCovered(Vector3.right) ||
                                _baseGrid[z, x].IsCovered(Vector3.left) ||
                                _baseGrid[z, x].IsCovered(Vector3.forward)||
                                _baseGrid[z, x].IsCovered(Vector3.back))
                        {
                            DrawArrow.ForDebug(_baseGrid[z, x].Position - offset,
                                2 * offset, Color.green, 0.1f, 0);
                            DrawArrow.ForDebug(_baseGrid[z, x].Position - offset2,
                                2 * offset2, Color.green, 0.1f, 0);
                        }
                    }
                }
            }*/
        }
    }
}