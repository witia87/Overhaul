using UnityEngine;

namespace Assets.Utilities
{
    public class FrameRateDisplay : MonoBehaviour
    {
        private float _deltaTime;
        private GUIStyle _guiStyle;
        private float _maxTime;

        private void Awake()
        {
            _guiStyle = new GUIStyle();
        }

        private void OnGUI()
        {
            _deltaTime += (Time.deltaTime - _deltaTime) * 0.1f;
            _maxTime = Mathf.Max(_maxTime, _deltaTime);
            int w = Screen.width, h = Screen.height;

            var rect = new Rect(0, 0, w, h * 2 / 100);
            _guiStyle.alignment = TextAnchor.UpperLeft;
            _guiStyle.fontSize = h * 2 / 100;
            _guiStyle.normal.textColor = new Color(0.8f, 0.0f, 0.2f, 1.0f);
            var msec = _deltaTime * 1000.0f;
            var maxmsec = _maxTime * 1000.0f;
            var fps = 1.0f / _deltaTime;
            var text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
            text += string.Format(" ({0:0.0} ms max)", maxmsec);
            GUI.Label(rect, text, _guiStyle);
        }
    }
}