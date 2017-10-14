using Assets.Gui.View;
using UnityEngine;

namespace Assets.Gui.Board
{
    public class BoardStore : MonoBehaviour, IBoardStore
    {
        private ViewStore _viewStore;

        public int BoardTextureWidth
        {
            get { return Mathf.RoundToInt(_viewStore.ScreenWidthInPixels) * 2; }
        }

        public int BoardTextureHeight
        {
            get { return Mathf.RoundToInt(_viewStore.ScreenHeightInPixels) * 2; }
        }

        private void Awake()
        {
            _viewStore = FindObjectOfType<ViewStore>();
        }
    }
}