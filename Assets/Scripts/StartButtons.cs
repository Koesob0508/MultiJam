using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace MultiJam
{
    public class StartButtons : MonoBehaviour
    {
        public GameObject panel;
        public Button host;
        public Button server;
        public Button client;

        private void Awake()
        {
            host.onClick.AddListener(() =>
            {
                NetworkManager.Singleton.StartHost();
                panel.SetActive(false);
            });

            server.onClick.AddListener(() =>
            {
                NetworkManager.Singleton.StartServer();
                panel.SetActive(false);
            });

            client.onClick.AddListener(() =>
            {
                NetworkManager.Singleton.StartClient();
                panel.SetActive(false);
            });
        }
    }
}