﻿using Assets.Gui.Board;
using Assets.Gui.Cameras;
using Assets.Gui.PlayerInput;
using Assets.Gui.View;
using UnityEngine;

namespace Assets.Gui
{
    public class GuiComponent : MonoBehaviour
    {
        protected IViewStore ViewStore;
        protected ICameraStore CameraStore;
        protected IBoardStore BoardStore;
        protected IMouseStore MouseStore;

        protected virtual void Awake()
        {
            ViewStore = FindObjectOfType<ViewStore>();
            CameraStore = FindObjectOfType<CameraStore>();
            BoardStore = FindObjectOfType<BoardStore>();
            MouseStore = FindObjectOfType<MouseStore>();
    }
    }
}
