using System;
using System.Collections.Generic;
using UnityEngine;

namespace MultiJam
{
    public abstract class CardPileView : MonoBehaviour, ICardPileView
    {
        #region Properties

        /// <summary>
        ///     List with all cards.
        /// </summary>
        public List<ICardView> Cards { get; private set; }

        /// <summary>
        ///     Event raised when add or remove a card.
        /// </summary>
        event Action<ICardView[]> onPileChanged = hand => { };

        Action<ICardView[]> ICardPileView.OnPileChanged
        {
            get => onPileChanged;
            set => onPileChanged = value;
        }

        #endregion

        #region UnityCallbacks

        protected virtual void Awake()
        {
            Cards = new List<ICardView>();

            Clear();
        }

        #endregion

        #region Operations

        /// <summary>
        ///     Add a card to the pile.
        /// </summary>
        /// <param name="card"></param>
        public virtual void AddCard(ICardView card)
        {
            if (card == null)
                throw new ArgumentNullException("Null is not a valid argument");

            Cards.Add(card);
            card.transform.SetParent(transform);
            NotifyPileChange();

            // card.Draw();
        }

        /// <summary>
        ///     Remove a card from the pile.
        /// </summary>
        /// <param name="card"></param>
        public virtual void RemoveCard(ICardView card)
        {
            if (card == null)
                throw new ArgumentNullException("Null is not a valid argument");

            Cards.Remove(card);

            NotifyPileChange();
        }

        protected virtual void Clear()
        {
            var childCards = GetComponentsInChildren<ICardView>();

            foreach (var viewCard in childCards)
                Destroy(viewCard.gameObject);

            Cards.Clear();
        }

        /// <summary>
        ///     Notify all listeners of this pile that some change has been made.
        /// </summary>
        public void NotifyPileChange() => onPileChanged?.Invoke(Cards.ToArray());

        #endregion

    }
}