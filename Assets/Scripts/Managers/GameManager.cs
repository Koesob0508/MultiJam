using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace MultiJam
{
    public class GameManager : NetworkBehaviour
    {
        private bool isGameStarted = false;
        private Define.Player _playerType = Define.Player.Unknown;
        public Define.Player PlayerType { get => _playerType; }

        public List<int> LeaderDeck;
        public List<int> FollowerDeck;

        public void Init()
        {
            if (Managers.Network != null)
            {
                Managers.Network.OnClientConnectedCallback += OnClientConnected;
            }
        }

        public void Clear()
        {
            if (Managers.Network != null)
            {
                Managers.Network.OnClientConnectedCallback -= OnClientConnected;
            }
        }

        private void OnClientConnected(ulong clientId)
        {
            if(Managers.Network.IsHost)
            {
                _playerType = Define.Player.Leader;
            }
            else
            {
                _playerType = Define.Player.Follower;
            }

            if (!isGameStarted)
            {
                if (Managers.Network.IsHost)
                {
                    if (AreAllPlayersConnected())
                    {
                        Debug.Log("Starting game for all clients");
                        StartGameClientRpc();
                        InitDeck();
                    }
                }
            }
        }

        private bool AreAllPlayersConnected()
        {
            return Managers.Network.ConnectedClientsList.Count == 2;
        }

        [ClientRpc]
        private void StartGameClientRpc()
        {
            isGameStarted = true;
            Managers.Board.InitBoard(0);
        }

        private void InitDeck()
        {
            LeaderDeck = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            FollowerDeck = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            ShuffleDeck(LeaderDeck);
            ShuffleDeck(FollowerDeck);
        }

        private void ShuffleDeck(List<int> _deck)
        {
            int n = _deck.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                int value = _deck[k];
                _deck[k] = _deck[n];
                _deck[n] = value;
            }
        }

        [ServerRpc]
        public void DrawCardServerRpc(Define.Player _type)
        {
            int result = -1;

            switch(_type)
            {
                case Define.Player.Leader:
                    result = LeaderDeck[LeaderDeck.Count - 1];

                    break;
                case Define.Player.Follower:
                    result = FollowerDeck[FollowerDeck.Count - 1];

                    break;
                default:
                    break;
            }
        }
    }
}