using System.Collections;
using UnityEngine;

namespace MultiJam
{
    public class TestPlayerHandView : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        [Tooltip("World point where the deck is positioned")]
        Transform deckPosition;

        [SerializeField]
        [Tooltip("Game view transform")]
        Transform view;

        #endregion

        #region Properties

        int Count { get; set; }

        IPlayerHandView PlayerHand { get; set; }

        #endregion

        #region Unitycallbacks

        private void Awake()
        {
            PlayerHand = transform.parent.GetComponentInChildren<IPlayerHandView>();
        }

        IEnumerator Start()
        {
            for (var i = 0; i < 5; i++)
            {
                yield return new WaitForSeconds(0.2f);
                DrawCard();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab)) DrawCard();
        }

        #endregion

        #region Opertations

        public void DrawCard()
        {
            var cardObj = Managers.Resource.Instantiate("Prefabs/Cards/Card", view);
            cardObj.name = "Card_" + Count;
            var card = cardObj.GetComponent<ICardView>();
            card.transform.position = deckPosition.position;
            Count++;
            PlayerHand.AddCard(card);
        }

        #endregion
    }
}