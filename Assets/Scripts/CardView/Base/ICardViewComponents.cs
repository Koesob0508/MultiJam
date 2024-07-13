using UnityEngine;

namespace MultiJam
{
    public interface ICardViewComponents
    {
        Camera MainCamera { get; }
        SpriteRenderer[] Renderers { get; }
        MeshRenderer Renderer { get; }
        Collider Collider { get; }
        Rigidbody Rigidbody { get; }
        IMouseInput Input { get; }
        MonoBehaviour MonoBehaviour { get; }
        GameObject gameObject { get; }
        Transform transform { get; }
    }
}