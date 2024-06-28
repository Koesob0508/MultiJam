using Unity.Netcode;
using UnityEngine;

namespace MultiJam
{
    public class GameManager : NetworkBehaviour
    {
        [SerializeField] private bool gameStarted = false;

        private void Start()
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            if(NetworkManager.Singleton != null)
            {
                NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
            }
        }

        private void OnClientConnected(ulong clientId)
        {
            if(IsServer)
            {
                Debug.Log("Is Server");
                if(!gameStarted && AreAllPlayersConnected())
                {
                    StartGame();
                }
            }
            else
            {
                Debug.Log("Is Client");
                gameStarted = true;
            }
        }

        private bool AreAllPlayersConnected()
        {
            return NetworkManager.Singleton.ConnectedClientsList.Count == 2;
        }

        private void StartGame()
        {
            gameStarted = true;

            Debug.Log("Start Game");

            //client.InitWorldClientRpc(0);
        }
    }
}