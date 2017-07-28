using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

namespace Assets.Modules.Targeting.Vision
{
    public class Target: ITarget
    {
        public float TargetLastSeenTime;

        public bool IsVisible
        {
            get { return TargetLastSeenTime > Time.time - 5 * Time.smoothDeltaTime; }
        }

        public Vector3 LastSeenPosition { get; set; }
        public Vector3 LastSeenMovementDirection { get; set; }
        public Unit Unit { get; private set; }

        public Vector3 Center { get { return Unit.Targeting.Center; } }

        public Vector3 MovementDirectionPrediction
        {
            get { return Unit.gameObject.transform.position - LastSeenPosition; }
        }

        public Target(Unit unit)
        {
            Unit = unit;
        }
    }
}