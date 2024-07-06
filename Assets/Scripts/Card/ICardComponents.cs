using UnityEngine;

namespace MultiJam
{
    public interface ICardComponents
    {
        Camera MainCamera { get; }
        SpriteRenderer[] Renderers { get; }
        SpriteRenderer Renderer { get; }
        Collider Collider { get; }
        Rigidbody Rigidbody { get; }
        IMouseInput Input { get; }
        MonoBehaviour MonoBehaviour { get; }
        GameObject gameObject { get; }
        Transform transform { get; }
    }
}