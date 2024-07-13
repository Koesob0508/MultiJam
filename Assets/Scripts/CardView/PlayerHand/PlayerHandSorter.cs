using System;
using UnityEngine;

namespace MultiJam
{
    /// <summary>
    ///     나중에 들어온 카드일수록 위로 올라가도록 조정함
    /// </summary>
    [RequireComponent(typeof(PlayerHandView))]
    public class PlayerHandSorter : MonoBehaviour
    {
        const float OffsetZ = -0.1f;

        ICardPileView PlayerHand { get; set; }

        private void Awake()
        {
            PlayerHand = GetComponent<IPlayerHandView>();
            // PlayerHand.OnPileChanged += Sort;
        }

        public void Sort(ICardView[] cards)
        {
            if (cards == null)
                throw new ArgumentException("Can't sort a card list null");

            var layerZ = 0f;

            foreach(var card in cards)
            {
                var localCardPosition = card.transform.localPosition;
                localCardPosition.z = layerZ;
                card.transform.localPosition = localCardPosition;
                layerZ += OffsetZ;
            }
        }
    }
}