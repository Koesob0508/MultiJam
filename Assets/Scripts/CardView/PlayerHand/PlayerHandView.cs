using System;

namespace MultiJam
{
    public class PlayerHandView : CardPileView, IPlayerHandView
    {
        #region Fields

        event Action<ICardView> onCardPlayed = card => { };
        event Action<ICardView> onCardSelected = card => { };

        #endregion
        #region Properties

        /// <summary>
        ///     Card currently selected by the player.
        /// </summary>
        public ICardView SelectedCard { get; private set; }

        /// <summary>
        ///     Event raised when a card is played.
        /// </summary>
        public Action<ICardView> OnCardPlayed
        {
            get => onCardPlayed;
            set => onCardPlayed = value;
        }

        /// <summary>
        ///     Event raised when a card is selected.
        /// </summary>
        public Action<ICardView> OnCardSelected
        {
            get => onCardSelected;
            set => onCardSelected = value;
        }

        #endregion

        #region Operations

        public void SelectCard(ICardView card)
        {
            SelectedCard = card ?? throw new ArgumentNullException("Null is not a valid argument.");

            DisableCards();
            NotifyCardSelected();
        }

        public void UnselectCard(ICardView card)
        {
            if (card == null) return;

            SelectedCard = null;
            card.UnSelect();
            NotifyPileChange();
            EnableCards();
        }

        public void UnSelect() => UnselectCard(SelectedCard);

        public void PlayCard(ICardView card)
        {
            if (card == null)
                throw new ArgumentNullException("Null is not a valid argument");

            SelectedCard = null;
            RemoveCard(card);
            OnCardPlayed?.Invoke(card);
            EnableCards();
            NotifyPileChange();
        }

        public void PlaySelected()
        {
            if (SelectedCard == null) return;

            PlayCard(SelectedCard);
        }

        public void EnableCards()
        {
            foreach (var otherCard in Cards)
                otherCard.Enable();
        }

        public void DisableCards()
        {
            foreach (var otherCard in Cards)
                otherCard.Disable();
        }

        void NotifyCardSelected() => OnCardSelected?.Invoke(SelectedCard);
        #endregion
    }
}