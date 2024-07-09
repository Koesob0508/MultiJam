using System;

namespace MultiJam
{
    public interface ICardViewPile
    {
        Action<ICardView[]> OnPileChanged { get; set; }
        void AddCard(ICardView viewCard);
        void RemoveCard(ICardView viewCard);
    }
}