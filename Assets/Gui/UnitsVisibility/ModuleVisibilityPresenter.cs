using Assets.Modules;
using Assets.Modules.Units;
using UnityEngine;

namespace Assets.Gui.UnitsVisibility
{
    public class ModuleVisibilityPresenter : MonoBehaviour
    {
        private Renderer _renderer;
        private Unit _unit;
        private UnitsVisibilityStore _unitsVisibilityStore;

        private float _visibilityLevel;

        private void Awake()
        {
            _unit = transform.root.GetComponent<Unit>();
            _unitsVisibilityStore = FindObjectOfType<UnitsVisibilityStore>();
            _renderer = GetComponent<Renderer>();
            
            //    Vec
            //   renderer.material.SetInt("_TexWidth", );
            //  renderer.material.SetInt("_TexHeight", );
        }

        protected void Update()
        {
            if (_unit.Fraction == FractionId.Player)
            {
                _renderer.material.SetFloat("_VisibilityLevel", 1);
            }
            else
            {
                if (_unitsVisibilityStore.IsUnitVisible(_unit))
                {
                    _visibilityLevel = Mathf.Min(_unitsVisibilityStore.DisapearingTime,
                        _visibilityLevel + Time.deltaTime);
                }
                else
                {
                    _visibilityLevel = Mathf.Max(0, _visibilityLevel - Time.deltaTime);
                }

                _renderer.material.SetFloat("_VisibilityLevel",
                    _visibilityLevel / _unitsVisibilityStore.DisapearingTime);
            }
        }
    }
}