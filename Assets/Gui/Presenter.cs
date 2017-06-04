using Assets.Presentation.Camera;
using UnityEngine;

namespace Assets.Presentation
{
    public class Presenter : MonoBehaviour
    {
        protected virtual void Update()
        {
            gameObject.transform.position =
                CameraComponent.GetClosestPixelatedPosition(gameObject.transform.position);
        }
    }
}