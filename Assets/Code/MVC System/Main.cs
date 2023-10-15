using Code.CoinSystem;
using Code.UI_System;
using Configs;
using PlayerManagement;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    [SerializeField] private ConfigsHolder _configsHolder;
    [SerializeField] private UIPanel _uiPanel;
    
    private Controller _controllers;

    private void Start()
    {
        Application.targetFrameRate = 60;
        _controllers = new Controller();

        new GameInit(_controllers, _configsHolder, _uiPanel);

        _controllers.OnStart();
    }

    private void Update()
    {
        _controllers.OnUpdate(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        _controllers.OnFixedUpdate(Time.fixedDeltaTime);
    }

    private void LateUpdate()
    {
        _controllers.OnLateUpdate(Time.deltaTime);
    }    
}