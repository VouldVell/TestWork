using System;
using Photon.Pun;

namespace Code.UI_System
{
    public class UIController : IDisposable
    {
        private readonly UIPanel _panel;

        public UIController(UIPanel panel)
        {
            _panel = panel;
            _panel.ButtonsExitToLobby.onClick.AddListener((() =>
            {
                PhotonNetwork.LeaveRoom();
                PhotonNetwork.LoadLevel("Lobby");
            }));
        }
        public void Victory(PhotonView view)
        {
            _panel.VictoryPanel.gameObject.SetActive(true);
            foreach (var panel in _panel.Managment)
            {
                panel.SetActive(false);
            }
            _panel.VictoryPaneCountCoins.text = $"collected coin: {_panel.CoinInfo.CurrentCoinsAmount.ToString()}";
            _panel.VictoryPanePlayerName.text = $"{view.Owner.NickName}: You Winner";
        }


        public void Dispose()
        {
            _panel.ButtonsExitToLobby.onClick.RemoveAllListeners();
        }
    }
}