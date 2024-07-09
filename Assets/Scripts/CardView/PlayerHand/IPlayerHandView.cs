using System;
using System.Collections.Generic;

namespace MultiJam
{
    public interface IPlayerHandView : ICardViewPile
    {
        List<ICardView> Cards { get; }
        Action<ICardView> OnCardPlayed { get; set; }
        Action<ICardView> OnCardSelected { get; set; }

        void PlaySelected();
        void UnSelect();
        void PlayCard(ICardView viewCard);
        void SelectCard(ICardView viewCard);
        void UnselectCard(ICardView viewCard);
    }
}