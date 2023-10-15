using System;
using Photon.Pun;
using UnityEngine.SceneManagement;

namespace Code.MultiPlayer
{
    public class ConnectSever : MonoBehaviourPunCallbacks
    {
        private void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            SceneManager.LoadScene("Lobby");
        }
    }
}