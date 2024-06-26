using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace MultiJam
{
    public class StartButtons : MonoBehaviour
    {
        [SerializeField] private Button host;
        [SerializeField] private Button server;
        [SerializeField] private Button client;

        private void Awake()
        {
            host.onClick.AddListener(() =>
            {
                NetworkManager.Singleton.StartHost();
            });

            server.onClick.AddListener(() =>
            {
                NetworkManager.Singleton.StartServer();
            });

            client.onClick.AddListener(() =>
            {
                NetworkManager.Singleton.StartClient();
            });
        }
    }
}