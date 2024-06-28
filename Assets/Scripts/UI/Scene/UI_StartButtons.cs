using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace MultiJam
{
    public class UI_StartButtons : UI_Scene
    {
        enum GameObjects
        {
            Panel,
        }

        enum Buttons
        {
            Button_Host,
            Button_Client,
        }

        private GameObject panel;
        public Button button_Host;
        public Button button_Client;

        public override void Init()
        {
            base.Init();

            Bind<GameObject>(typeof(GameObjects));
            Bind<Button>(typeof(Buttons));

            panel = Get<GameObject>((int)GameObjects.Panel);
            button_Host = Get<Button>((int)Buttons.Button_Host);
            button_Client = Get<Button>((int)Buttons.Button_Client);

            button_Host.onClick.AddListener(() =>
            {
                NetworkManager.Singleton.StartHost();
                panel.SetActive(false);
            });

            button_Client.onClick.AddListener(() =>
            {
                NetworkManager.Singleton.StartClient();
                panel.SetActive(false);
            });
        }
    }
}