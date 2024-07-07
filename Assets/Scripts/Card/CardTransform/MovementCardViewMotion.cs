using UnityEngine;

namespace MultiJam
{
    public class MovementCardViewMotion : CardViewMotionBase
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
            var current = View.transform.position;
            var amount = Speed * Time.deltaTime;
            var delta = Vector3.Lerp(current, Target, amount);
            if (!WithZ)
                delta.z = View.transform.position.z;
            View.transform.position = delta;
        }

        protected override bool CheckFinalState()
        {
            var distance = Target - View.transform.position;

            if (!WithZ)
                distance.z = 0;

            return distance.magnitude <= Threshold;
        }

        protected override void OnMotionEnds()
        {
            WithZ = false;
            IsOperating = false;
            var target = Target;
            target.z = View.transform.position.z;
            View.transform.position = target;
            base.OnMotionEnds();
        }
    }
}