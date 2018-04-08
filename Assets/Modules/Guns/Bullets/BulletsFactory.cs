using UnityEngine;

namespace Assets.Modules.Guns.Bullets
{
    public class BulletsFactory : MonoBehaviour
    {
        [SerializeField] public LayerMask _moduleLayerMask;
        public float AngleOfImprecision = 0.1f;
        public float BulletMaximalLifetime = 2f;
        public float BulletsAngularDrag = 0.1f;
        public float BulletsDrag = 0.1f;
        public float BulletsMass = 0.1f;
        public Vector3 BulletsSize = new Vector3(0.05f, 0.05f, 0.1f);
        public float InitialVelocity = 10f;

        public float StunTime = 0.2f;

        public void Create()
        {
            var bullet =
                (GameObject) Instantiate(UnityEngine.Resources.Load("Prefabs\\Units\\Guns\\Bullets\\Bullet"));
            bullet.transform.localScale = BulletsSize;
            bullet.transform.position = transform.position;
            bullet.transform.eulerAngles = transform.eulerAngles + new Vector3(
                                               (Random.value - 0.5f) * 2 * AngleOfImprecision,
                                               (Random.value - 0.5f) * 2 * AngleOfImprecision,
                                               0);
            bullet.GetComponent<Bullet>().ModuleLayerMask = _moduleLayerMask;
            bullet.GetComponent<Bullet>().StunTime = StunTime;

            var bulletRigidbody = bullet.GetComponent<Rigidbody>();
            bulletRigidbody.mass = BulletsMass;
            bulletRigidbody.drag = BulletsDrag;
            bulletRigidbody.angularDrag = BulletsAngularDrag;
            bulletRigidbody.AddForce(transform.forward * InitialVelocity, ForceMode.VelocityChange);
        }
    }
}