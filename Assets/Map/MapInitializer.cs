using Assets.Utilities;
using UnityEngine;

namespace Assets.Map
{
    public class MapInitializer : MonoBehaviour
    {
        private readonly int _layerMask = Layers.Floor
                                          | Layers.Map | Layers.MapTransparent
                                          | Layers.Environment | Layers.EnvironmentTransparent;

        private MapStore _mapStore;
        public int GridLength;

        public int GridWidth;
        public float UnitSize;

        private void Awake()
        {
            _mapStore = FindObjectOfType<MapStore>();
            var grid = new Vector3[GridLength, GridWidth];
            for (var z = 0; z < GridLength; z++)
            {
                for (var x = 0; x < GridWidth; x++)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(
                        new Vector3(x + UnitSize/2, 10, z + UnitSize/2),
                        Vector3.down, out hit, 20, _layerMask))
                    {
                        if (hit.transform.tag == "Floor")
                        {
                            grid[z, x] = hit.point;
                        }
                        else
                        {
                            grid[z, x] = Vector3.zero;
                        }
                    }
                }
            }

            _mapStore.InitializeMap(grid, UnitSize);
        }
        

        public void ShowGrid(int scale)
        {
            for (var z = 0; z < _mapStore.Grid[scale].GetLength(0); z++)
            {
                for (var x = 0; x < _mapStore.Grid[scale].GetLength(1); x++)
                {
                    if (_mapStore.Grid[scale][z, x] != null)
                        DrawArrow.ForDebug(_mapStore.Grid[scale][z, x].Position + Vector3.up,
                            Vector3.down, Color.green, 0.1f, 0);
                }
            }
        }
    }
}