using Unity.Netcode;
using UnityEngine;

namespace MultiJam
{
    public class GameManager : NetworkBehaviour
    {
        private bool isGameStarted = false;
        private Define.Player _playerType = Define.Player.Unknown;
        public Define.Player PlayerType { get => _playerType; }

        public void Init()
        {
            if (NetworkManager.Singleton != null)
            {
                NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
            }
        }

        public void Clear()
        {
            if (NetworkManager.Singleton != null)
            {
                NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
            }
        }

        private void OnClientConnected(ulong clientId)
        {
            if(NetworkManager.Singleton.IsHost)
            {
                _playerType = Define.Player.Leader;
            }
            else
            {
                _playerType = Define.Player.Follower;
            }

            if (!isGameStarted)
            {
                if (NetworkManager.Singleton.IsHost)
                {
                    if (AreAllPlayersConnected())
                    {
                        Debug.Log("Starting game for all clients");
                        StartGameClientRpc();
                    }
                }
            }
        }

        private bool AreAllPlayersConnected()
        {
            return NetworkManager.Singleton.ConnectedClientsList.Count == 2;
        }

        [ClientRpc]
        private void StartGameClientRpc()
        {
            isGameStarted = true;
            Managers.Board.InitBoard(0);
        }
    }
}