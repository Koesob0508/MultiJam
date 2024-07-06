using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode.Transports.UTP;

namespace MultiJam
{
    public class UI_StartScene : UI_Scene
    {
        enum GameObjects
        {
            Panel,
        }

        enum InputFields
        {
            InputField,
        }

        enum Buttons
        {
            Button_Host,
            Button_Client,
        }

        private GameObject panel;
        public TMP_InputField inputField;
        public Button button_Host;
        public Button button_Client;

        public override void Init()
        {
            base.Init();

            Bind<GameObject>(typeof(GameObjects));
            Bind<TMP_InputField>(typeof(InputFields));
            Bind<Button>(typeof(Buttons));

            panel = Get<GameObject>((int)GameObjects.Panel);
            inputField = Get<TMP_InputField>((int)InputFields.InputField);
            button_Host = Get<Button>((int)Buttons.Button_Host);
            button_Client = Get<Button>((int)Buttons.Button_Client);

            button_Host.onClick.AddListener(() => OnClickHostButton());

            button_Client.onClick.AddListener(() => OnClickClientButton());
        }

        private async void OnClickHostButton()
        {
            var data = await RelayManager.SetupRelay(10, "production");
            Managers.Network.GetComponent<UnityTransport>().SetRelayServerData(data.IPv4Address, data.Port, data.AllocationIdBytes, data.Key, data.ConnectionData);
            inputField.text = data.JoinCode;
            
            Managers.Network.StartHost();

            panel.SetActive(false);
        }

        private async void OnClickClientButton()
        {
            var data = await RelayManager.JoinRelay(inputField.text, "production");
            Managers.Network.GetComponent<UnityTransport>().SetRelayServerData(data.IPv4Address, data.Port, data.AllocationIdBytes, data.Key, data.ConnectionData, data.HostConnectionData);
            Managers.Network.StartClient();
            
            panel.SetActive(false);
        }
    }
}