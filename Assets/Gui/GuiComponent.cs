using Assets.Gui.Cameras;
using UnityEngine;

namespace Assets.Gui
{
    public class GuiComponent : MonoBehaviour, IGuiStore
    {
        private GameObject _background;
        private GameObject _board;
        private GameObject _outlineBoard;

        [SerializeField] public int _outlineSize = 1;

        [SerializeField] public int _pixelationSize = 4;

        //[HideInInspector] public float GuiFragmentSizeInUnits = 0.1f;

        public Camera GuiCamera { get; private set; }

        public RenderTexture BoardTexture { get; private set; }
        
        public int BoardPixelWidth
        {
            get { return Screen.width / _pixelationSize * 2; }
        }

        public int BoardPixelHeight
        {
            get { return Screen.height / _pixelationSize * 2; }
        }


        public RenderTexture OutlineTexture { get; private set; }

        private CameraComponent _cameraComponent;
        private void Awake()
        {
            CreateBoard();

            CreateOutlineBoard();
            
            CreateGuiCamera();

            _cameraComponent = FindObjectOfType<CameraComponent>();
        }

        private void CreateBoard()
        {
            BoardTexture = new RenderTexture(BoardPixelWidth, BoardPixelHeight, 256);
            BoardTexture.filterMode = FilterMode.Point;
            
            var boardMaterial = Resources.Load("Materials/Board", typeof(Material)) as Material;
            boardMaterial.mainTexture = BoardTexture;

            _board = PrecisionQuadFactory.Create("Board Quad", boardMaterial,
                new Vector3(-BoardPixelWidth / (float)2, -BoardPixelHeight / (float)2, 0),
                new Vector3(+BoardPixelWidth / (float)2, -BoardPixelHeight / (float)2, 0),
                new Vector3(-BoardPixelWidth / (float)2, +BoardPixelHeight / (float) 2, 0),
                new Vector3(+BoardPixelWidth / (float)2, +BoardPixelHeight / (float) 2, 0));
            _board.transform.parent = transform;
            _board.transform.localEulerAngles = Vector3.zero;
            _board.transform.localPosition = Vector3.zero;
        }

        private void CreateOutlineBoard()
        {
            OutlineTexture = new RenderTexture(BoardPixelWidth, BoardPixelHeight, 256);
            OutlineTexture.filterMode = FilterMode.Point;

            var boardMaterial = Resources.Load("Materials/OutlineBoard", typeof(Material)) as Material;
            boardMaterial.mainTexture = OutlineTexture;
            _outlineBoard = PrecisionQuadFactory.Create("Outline Quad", boardMaterial,
                new Vector3(-BoardPixelWidth / (float)2, -BoardPixelHeight / (float)2, 0),
                new Vector3(+BoardPixelWidth / (float)2, -BoardPixelHeight / (float)2, 0),
                new Vector3(-BoardPixelWidth / (float)2, +BoardPixelHeight / (float)2, 0),
                new Vector3(+BoardPixelWidth / (float)2, +BoardPixelHeight / (float)2, 0));
            _outlineBoard.transform.parent = transform;
            _outlineBoard.transform.localEulerAngles = Vector3.zero;
            _outlineBoard.transform.localPosition = Vector3.back;


            var renderer = _outlineBoard.GetComponent<MeshRenderer>();
            renderer.material.SetInt("_TexWidth", BoardTexture.width);
            renderer.material.SetInt("_TexHeight", BoardTexture.height);
            renderer.material.SetInt("_OutlineSize", _outlineSize);
        }

        private void CreateGuiCamera()
        {
            GuiCamera = new GameObject("Gui Camera").AddComponent<Camera>();
            GuiCamera.transform.parent = transform;
            GuiCamera.cameraType = CameraType.Preview;
            GuiCamera.orthographic = true;
            GuiCamera.transform.localPosition = Vector3.back * 10;
            GuiCamera.transform.localEulerAngles = Vector3.zero;
            GuiCamera.orthographicSize = Screen.height / (float) _pixelationSize / 2;
            GuiCamera.aspect = Screen.width / (float) Screen.height;
            Camera.SetupCurrent(GuiCamera);
        }

        private void Update()
        {
            var offset = new Vector3(
                _cameraComponent.CameraPlaneOffset.x * _cameraComponent.PixelsPerUnitInCameraSpace,
                _cameraComponent.CameraPlaneOffset.y * _cameraComponent.PixelsPerUnitInCameraSpace,
                -10);
            GuiCamera.transform.localPosition = offset;
        }
    }
}