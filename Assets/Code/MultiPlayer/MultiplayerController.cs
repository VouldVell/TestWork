using System.Collections.Generic;
using System.Threading.Tasks;
using Configs;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Code.MultiPlayer
{
    public class MultiplayerController : MonoBehaviourPunCallbacks
    {
        [SerializeField] private MenuManager _manager;
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private PlayersInfo _playerinfo;
        
        private RoomOptions _roomOptions;

        private void Start()
        {
            _roomOptions = new RoomOptions();
            
            
            _manager.Create.onClick.AddListener(CreateRoom);
            _manager.Join.onClick.AddListener(JoinRoom);
            _playerinfo.Start.onClick.AddListener(() => PhotonNetwork.LoadLevel("Game"));
            _playerinfo.Start.interactable = false;
        }

        private void CreateRoom()
        {
            _manager.ButtonsHolder.SetActive(false);
            _manager.LobbyPanel.SetActive(true);
            PhotonNetwork.AutomaticallySyncScene = true;
            _roomOptions.MaxPlayers = _gameConfig.MaxPlayers;
            PhotonNetwork.NickName = "Player" + Random.Range(1, 1000);
            PhotonNetwork.CreateRoom(_manager.InputCreate.text, _roomOptions);
        }

        private void JoinRoom()
        {
            _manager.ButtonsHolder.SetActive(false);
            _manager.LobbyPanel.SetActive(true);
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.NickName = "Player" + Random.Range(1, 1000);
            PhotonNetwork.JoinRoom(_manager.InputJoin.text);
            
        }

        public override async void OnJoinRoomFailed(short returnCode, string message)
        {
            _manager.ButtonsHolder.SetActive(true);
            _manager.LobbyPanel.SetActive(false);
            _manager.errorsText.text = "there is no such room";
            await Task.Delay(3000);
            _manager.errorsText.text = "";
        }

        public override async void OnCreateRoomFailed(short returnCode, string message)
        {
            _manager.ButtonsHolder.SetActive(true);
            _manager.LobbyPanel.SetActive(false);
            _manager.errorsText.text = "such a room already exists";
            await Task.Delay(3000);
            _manager.errorsText.text = "";
        }

        public override void OnJoinedRoom()
        {
            _playerinfo.RegisteredPlayer(PhotonNetwork.PlayerList.Length);
        }

        private void Update()
        {
            _playerinfo.RegisteredPlayer(PhotonNetwork.PlayerList.Length);
            if (PhotonNetwork.IsMasterClient)
            {
                if (PhotonNetwork.PlayerList.Length >= _gameConfig.MinPlayers && PhotonNetwork.PlayerList.Length <= _gameConfig.MaxPlayers)
                {
                    _playerinfo.Start.interactable = true;
                }
            }
        }

        public void Dispose()
        {
            _manager.Create.onClick.RemoveAllListeners();
            _manager.Join.onClick.RemoveAllListeners();
        }
        
        
    }
}