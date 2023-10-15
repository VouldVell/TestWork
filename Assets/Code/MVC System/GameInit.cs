using Code.CoinSystem;
using Code.UI_System;
using Configs;
using PlayerManagement;
using UnityEngine;
using UnityEngine.UI;


public class GameInit
{
    public GameInit(Controller controller, ConfigsHolder configs, UIPanel uiPanel)
    {
        var uiController = new UIController(uiPanel);
        var mapController = new CoinController(uiPanel.CoinInfo, configs.GameConfig);
        var playerController = new PlayerController(configs, uiPanel, mapController, uiController);
        
        
        controller.Add(playerController);
    }
}