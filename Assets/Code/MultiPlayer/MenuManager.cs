using System;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace Code.MultiPlayer 
{
    public class MenuManager : MonoBehaviourPunCallbacks
    {
        public TMP_InputField InputCreate;
        public TMP_InputField InputJoin;
        public Button Create;
        public Button Join;
        public GameObject LobbyPanel;
        public GameObject ButtonsHolder;
        public TMP_Text errorsText;
    }
}