﻿using UnityEngine;

namespace Assets.Gui.UnitPresentation
{
    public class LegsPresenter : ModulePresenter
    {
        private int _lastCameraPlaneX;
        private int _lastCameraPlaneY;
        private float _lastLattitude;

        public bool HasPositionChanged { get; private set; }

        public void RecalculatePosition(int x, int y, float lattitude)
        {
            HasPositionChanged = x != _lastCameraPlaneX ||
                                 y != _lastCameraPlaneY ||
                                 Mathf.Abs(lattitude - _lastLattitude) > 0.1f;

            _lastCameraPlaneX = x;
            _lastCameraPlaneY = y;
            _lastLattitude = lattitude;
        }

        protected void RefreshPosition()
        {
            transform.position = CameraStore.Pixelation.TransformCameraPlanePositionToWorldPosition(
                new Vector2(_lastCameraPlaneX, _lastCameraPlaneY), _lastLattitude);
        }

        public void Refresh()
        {
            RefreshAngles();
            RefreshPosition();
        }

        protected Animator Animator;
        protected override void Awake()
        {
            base.Awake();
            Animator = GetComponent<Animator>();
        }

        protected virtual void Update()
        {
            Animator.SetFloat("Speed", GetVelocity());
        }

        private float GetVelocity()
        {
            return Mathf.Sign(Module.transform.InverseTransformDirection(Module.Rigidbody.velocity).z) *
                Module.Rigidbody.velocity.magnitude;
        }
    }
}