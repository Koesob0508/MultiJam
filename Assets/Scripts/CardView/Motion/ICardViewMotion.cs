using UnityEngine;

namespace MultiJam
{
    /// <summary>
    ///     Interface for simple motion for card
    /// </summary>
    public interface ICardViewMotion
    {
        /// <summary>
        ///     Move motion module.
        /// </summary>
        BaseCardViewMotion Movement { get; }

        /// <summary>
        ///     Rotate motion module.
        /// </summary>
        BaseCardViewMotion Rotation { get; }

        /// <summary>
        ///     Scale motion module.
        /// </summary>
        BaseCardViewMotion Scale { get; }

        /// <summary>
        ///     Move in the 3d space using only the X and Y axis.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        /// <param name="delay"></param>
        void MoveTo(Vector3 position, float speed, float delay = 0);

        /// <summary>
        ///     Move in the 3d space.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        /// <param name="delay"></param>
        void MoveToWithZ(Vector3 position, float speed, float delay = 0);

        /// <summary>
        ///     Rotate in the 3d space.
        /// </summary>
        /// <param name="euler"></param>
        /// <param name="speed"></param>
        /// <param name="delay"></param>
        void RotateTo(Vector3 euler, float speed, float delay = 0);

        /// <summary>
        ///     Scale in the 3d space.
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="speed"></param>
        /// <param name="delay"></param>
        void ScaleTo(Vector3 scale, float speed, float delay = 0);
    }
}