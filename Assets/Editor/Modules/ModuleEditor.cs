using Assets.Modules;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Modules
{
    [CustomEditor(typeof (Module), true)]
    public class ModuleEditor : UnityEditor.Editor
    {
        public float BottomSpikeRatio = 0.2f;
        public float CornerOffset = 0.2f;

        public float FlatRatio = 0.2f;
        public int Index;
        public string[] Options = {"Box", "Octagon", "Dodecadon"};
        public float TopSpikeRatio = 0.1f;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            BottomSpikeRatio = EditorGUILayout.FloatField("Collider bottom spike ratio:", BottomSpikeRatio);
            TopSpikeRatio = EditorGUILayout.FloatField("Collider top spike ratio:", TopSpikeRatio);
            FlatRatio = EditorGUILayout.FloatField("Flat parts ratio:", FlatRatio);
            CornerOffset = EditorGUILayout.FloatField("Corner offset ratio:", CornerOffset);

            var module = (Module) target;

            Index = EditorGUILayout.Popup(Index, Options);
            if (GUILayout.Button("Setup Collider and Rigidbody"))
            {
                SetupColliderAndRigidbody(module);
            }
        }

        private void SetupColliderAndRigidbody(Module module)
        {
            switch (Index)
            {
                case 0:
                    RemoveComponents(module.gameObject);
                    var extendedBoxCollider = module.gameObject.AddComponent<MeshCollider>();
                    extendedBoxCollider.sharedMesh = BoxMeshHelper.Create(module.Size.x, module.Size.y, module.Size.z,
                        BottomSpikeRatio);
                    extendedBoxCollider.convex = true;

                    AddRigidbody(module.gameObject);

                    GUIUtility.ExitGUI();
                    break;
                case 1:
                    RemoveComponents(module.gameObject);
                    var meshCollider = module.gameObject.AddComponent<MeshCollider>();
                    meshCollider.sharedMesh = ExtendedOctagonMeshHelper.Create(module.Size, BottomSpikeRatio,
                        TopSpikeRatio);
                    meshCollider.convex = true;

                    AddRigidbody(module.gameObject);

                    GUIUtility.ExitGUI();
                    break;
                case 2:
                    RemoveComponents(module.gameObject);
                    var dodecanonCollider = module.gameObject.AddComponent<MeshCollider>();
                    dodecanonCollider.sharedMesh = DodecadonMeshHelper.Create(module.Size, FlatRatio, CornerOffset,
                        BottomSpikeRatio, TopSpikeRatio);
                    dodecanonCollider.convex = true;

                    AddRigidbody(module.gameObject);

                    GUIUtility.ExitGUI();
                    break;
                default:
                    Debug.LogError("Unrecognized Option");
                    break;
            }
        }

        private void AddRigidbody(GameObject gameObject)
        {
            var rigidbody = gameObject.AddComponent<Rigidbody>();
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            rigidbody.mass = 40;
            rigidbody.drag = 20;
            rigidbody.angularDrag = 10;
        }

        private void RemoveComponents(GameObject gameObject)
        {
            var rigidbodies = gameObject.GetComponents<Rigidbody>();
            foreach (var rigidbody in rigidbodies)
            {
                DestroyImmediate(rigidbody);
            }

            var colliders = gameObject.GetComponents<Collider>();
            foreach (var collider in colliders)
            {
                DestroyImmediate(collider);
            }
        }
    }
}