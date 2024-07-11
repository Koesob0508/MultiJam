using UnityEngine;

namespace MultiJam
{
    public class MovementCardViewMotion : BaseCardViewMotion
    {
        bool WithZ { get; set; }

        public MovementCardViewMotion(ICardView card) : base(card)
        {
        }

        public override void Execute(Vector3 vector, float speed, float delay = 0, bool withZ = false)
        {
            WithZ = withZ;
            base.Execute(vector, speed, delay, withZ);
        }

        protected override void KeepMotion()
        {
            var current = Handler.transform.position;
            var amount = Speed * Time.deltaTime;
            var delta = Vector3.Lerp(current, Target, amount);
            if (!WithZ)
                delta.z = Handler.transform.position.z;
            Handler.transform.position = delta;
        }

        protected override bool CheckFinalState()
        {
            var distance = Target - Handler.transform.position;

            if (!WithZ)
                distance.z = 0;

            return distance.magnitude <= Threshold;
        }

        protected override void OnMotionEnds()
        {
            WithZ = false;
            IsOperating = false;
            var target = Target;
            target.z = Handler.transform.position.z;
            Handler.transform.position = target;
            base.OnMotionEnds();
        }
    }
}