using Assets.Maps;
using Assets.Units;
using UnityEngine;

namespace Assets.Gui.UnitsVisibility
{
    public class ModuleVisibilityPresenter : MonoBehaviour
    {
        private Renderer _renderer;
        [SerializeField] private Unit _unit;
        private UnitsVisibilityStore _unitsVisibilityStore;

        private void Awake()
        {
            _unitsVisibilityStore = FindObjectOfType<UnitsVisibilityStore>();
            _renderer = GetComponent<Renderer>();
            //   renderer.material.SetInt("_TexWidth", );
            //  renderer.material.SetInt("_TexHeight", );
        }

        private float _visibilityLevel;
        protected void Update()
        {
            if (_unit.FractionId == FractionId.Player)
            {
                _renderer.material.SetFloat("_VisibilityLevel", 1);
            }
            else
            {
                if (_unitsVisibilityStore.IsUnitVisible(_unit))
                {
                    _visibilityLevel = Mathf.Min(_unitsVisibilityStore.DisapearingTime, _visibilityLevel + Time.deltaTime);
                }
                else
                {
                    _visibilityLevel = Mathf.Max(0, _visibilityLevel - Time.deltaTime);

                }
                _renderer.material.SetFloat("_VisibilityLevel", _visibilityLevel  / _unitsVisibilityStore.DisapearingTime);
            }
        }
    }
}