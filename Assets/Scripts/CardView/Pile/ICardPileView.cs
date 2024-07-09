using System;

namespace MultiJam
{
    public interface ICardPileView
    {
        Action<ICardView[]> OnPileChanged { get; set; }
        void AddCard(ICardView viewCard);
        void RemoveCard(ICardView viewCard);
    }
}