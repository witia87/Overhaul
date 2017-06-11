﻿using Assets.Utilities;
using UnityEngine;

namespace Assets.Gui
{
    public class GuiComponent : MonoBehaviour, IGuiStore
    {
        private GameObject _background;

        private GameObject _board;
        
        private Camera _guiCamera;
        
        public Material BoardMaterial;
        public RenderTexture BoardTexture;

        public float GuiFragmentSizeInUnits = 0.1f;

        public int _pixelationSize = 4;
        public int PixelationSize
        {
            get { return _pixelationSize;  }
        }


        public int BoardPixelWidth
        {
            get { return Screen.width / PixelationSize; }
        }

        public int BoardPixelHeight
        {
            get { return Screen.height / PixelationSize; }
        }

        public RenderTexture RegisterBoardTexture
        {
            get
            {
                return BoardTexture;
            }
        }

        private void Awake()
        {
            var screenWidth = Screen.width*GuiFragmentSizeInUnits;
            var screenHeight = Screen.height*GuiFragmentSizeInUnits;

            CreateBackground(screenWidth, screenHeight);

            CreateBoard(screenWidth, screenHeight);

            CreateGuiCamera(screenWidth, screenHeight);
        }

        private void CreateBackground(float screenWidth, float screenHeight)
        {
            var guiBackgroundMaterial = Resources.Load("Materials/GuiBackgroundMaterial", typeof (Material)) as Material;
            _background = PrecisionQuadFactory.Create("Gui Backround Quad", guiBackgroundMaterial,
                new Vector3(-screenWidth/2, -screenHeight/2, 0),
                new Vector3(+screenWidth/2, -screenHeight/2, 0),
                new Vector3(-screenWidth/2, +screenHeight/2, 0),
                new Vector3(+screenWidth/2, +screenHeight/2, 0));
            _background.transform.parent = gameObject.transform;
            _background.transform.localEulerAngles = Vector3.zero;
            _background.transform.localPosition = Vector3.back;
        }

        private void CreateBoard(float screenWidth, float screenHeight)
        {
            BoardTexture = new RenderTexture(BoardPixelWidth, BoardPixelHeight, 256);
            BoardTexture.filterMode = FilterMode.Point;

            var boardWidth = BoardPixelWidth*PixelationSize*GuiFragmentSizeInUnits;
            var boardHeight = BoardPixelHeight*PixelationSize*GuiFragmentSizeInUnits;

            var boardMaterial = Resources.Load("Materials/BoardMaterial", typeof (Material)) as Material;
            boardMaterial.mainTexture = BoardTexture;
            _board = PrecisionQuadFactory.Create("Board Quad", boardMaterial,
                new Vector3(-boardWidth/2, -boardHeight/2, 0),
                new Vector3(+boardWidth/2, -boardHeight/2, 0),
                new Vector3(-boardWidth/2, +boardHeight/2, 0),
                new Vector3(+boardWidth/2, +boardHeight/2, 0));
            _board.transform.parent = gameObject.transform;
            _board.transform.localEulerAngles = Vector3.zero;
            _board.transform.localPosition = Vector3.back;
        }

        private void CreateGuiCamera(float screenWidth, float screenHeight)
        {
            _guiCamera = new GameObject("Gui Camera").AddComponent<Camera>();
            _guiCamera.transform.parent = gameObject.transform;
            _guiCamera.cameraType = CameraType.Preview;
            _guiCamera.orthographic = true;
            _guiCamera.transform.localPosition = Vector3.back*10;
            _guiCamera.transform.localEulerAngles = Vector3.zero;
            _guiCamera.orthographicSize = screenHeight/2;
            _guiCamera.aspect = screenWidth/screenHeight;
            Camera.SetupCurrent(_guiCamera);
        }
    }
}