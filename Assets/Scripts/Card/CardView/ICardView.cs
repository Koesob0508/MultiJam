namespace MultiJam
{
    /// <summary>
    /// Card가 해야할 동작 명세
    /// </summary>
    public interface ICardView : ICardViewComponents, ICardViewMotion
    {
        bool IsDragging { get; }
        bool IsHovering { get; }
        bool IsDisabled { get; }
        bool IsPlayer { get; }

        void Enable();
        void Disable();
        void Select();
        void UnSelect();
        void Hover();
        void Draw();
        void Discard();
    }
}