using System;
using System.Collections;
using UnityEngine;

namespace MultiJam
{
    /// <summary>
    /// Card의 Motion 기반
    /// </summary>
    public abstract class BaseCardViewMotion
    {
        /// <summary>
        /// Motion Card
        /// </summary>
        protected ICardView View { get; }
        protected BaseCardViewMotion(ICardView view) => View = view;

        /// <summary>
        /// Dispathces when the motion ends.
        /// </summary>
        public Action OnFinishMotion = () => { };

        /// <summary>
        /// Whether the component is still operating or not.
        /// </summary>
        public bool IsOperating { get; protected set; }

        /// <summary>
        /// Limit magnitude until the reaches the target completely.
        /// </summary>
        protected virtual float Threshold => 0.01f;

        /// <summary>
        /// Target for motion calculation
        /// </summary>
        protected Vector3 Target { get; set; }

        /// <summary>
        /// Speed which the it moves towards the Target.
        /// </summary>
        protected float Speed { get; set; }

        /// <summary>
        /// CardComponent의 Update에서 실행되도록 구현됨
        /// </summary>
        public void Update()
        {
            if (!IsOperating) return;

            if (CheckFinalState())
                OnMotionEnds();
            else
                KeepMotion();
        }

        /// <summary>
        /// Execute the motion with the parameters.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="speed"></param>
        /// <param name="delay"></param>
        /// <param name="withZ"></param>
        public virtual void Execute(Vector3 vector, float speed, float delay = 0, bool withZ = false)
        {
            Speed = speed;
            Target = vector;
            if (delay == 0)
                IsOperating = true;
            else
                View.MonoBehaviour.StartCoroutine(AllowMotion(delay));
        }

        /// <summary>
        /// Keep the motion on update.
        /// </summary>
        protected abstract void KeepMotion();

        /// <summary>
        /// Ends the motion and dispatch motion ends.
        /// </summary>
        protected virtual void OnMotionEnds() => OnFinishMotion?.Invoke();

        /// <summary>
        /// Check if it has reached the threshold.
        /// </summary>
        /// <returns></returns>
        protected abstract bool CheckFinalState();

        IEnumerator AllowMotion(float delay)
        {
            yield return new WaitForSeconds(delay);
            IsOperating = true;
        }

        /// <summary>
        /// Stop the motion. It won't trigger OnFinishMotion.
        /// TODO : Cancel the Delay Coroutine.
        /// </summary>
        public virtual void StopMotion() => IsOperating = false;
    }
}