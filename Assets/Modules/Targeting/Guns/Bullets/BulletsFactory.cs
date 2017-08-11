using UnityEngine;

namespace Assets.Modules.Targeting.Guns.Bullets
{
    public class BulletsFactory : MonoBehaviour
    {
        public float AngleOfImprecision = 0.1f;
        public float BulletMaximalLifetime = 2f;
        public float BulletsAngularDrag = 0.1f;
        public float BulletsDrag = 0.1f;
        public float BulletsMass = 0.1f;
        public Vector3 BulletsSize = new Vector3(0.05f, 0.05f, 0.1f);
        public float InitialVelocity = 10f;

        public void Create(float horrizontalCorrection)
        {
            var bullet = (GameObject) Instantiate(Resources.Load("Prefabs\\Modules\\Targeting\\Guns\\Bullets\\Bullet"));
            bullet.transform.parent = transform;
            bullet.transform.localScale = BulletsSize;
            bullet.transform.localPosition = Vector3.zero;
            bullet.transform.localEulerAngles = new Vector3((Random.value - 0.5f) * 2 * AngleOfImprecision,
                (Random.value - 0.5f) * 2 * AngleOfImprecision,
                0);
            bullet.transform.forward = new Vector3(bullet.transform.forward.x,
                bullet.transform.forward.y + horrizontalCorrection, bullet.transform.forward.z);
            bullet.transform.parent = null;

            var bulletRigidbody = bullet.GetComponent<Rigidbody>();
            bulletRigidbody.mass = BulletsMass;
            bulletRigidbody.drag = BulletsDrag;
            bulletRigidbody.angularDrag = BulletsAngularDrag;
            bulletRigidbody.AddRelativeForce(Vector3.forward * InitialVelocity, ForceMode.VelocityChange);
        }
    }
}