using System.Collections.Generic;
using Code.CoinSystem;
using Code.UI_System;
using Configs;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Unity.Mathematics;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

namespace PlayerManagement
{
    public class PlayerController: IOnController, IOnFixedUpdate
    {
        private readonly GameObject _playerGameObject;
        private readonly GameObject _bulletGameObject;
        private readonly VariableJoystick _joystick;
        private readonly UIPanel _panel;
        private readonly HealthBar _healthBar;
        private readonly CoinController _coinController;
        private readonly UIController _uiController;
        private readonly GameConfig _gameConfig;

        private Quaternion _rotation;
        private int _damage = 10;
        private bool _isDead = false;
        
        private Player _player;
        private BulletView _bullet;
        private bool _enabled = true;
        

        public PlayerController(ConfigsHolder configsHolder, UIPanel panel, CoinController coinController, UIController uiController)
        {
            _playerGameObject = configsHolder.ObjectsHolder.Player;
            _bulletGameObject = configsHolder.ObjectsHolder.Bullet;
            _gameConfig = configsHolder.GameConfig;
            _joystick = panel.JoyStick;
            _panel = panel;
            _healthBar = panel.healthBar;
            _coinController = coinController;
            _uiController = uiController;
            SpawnPlayer();
        }

        private void Damage()
        {
            if (_player.Health > 0)
            {
                _player.Health -= _gameConfig.Damage;
                _healthBar.Damage(_damage);
            }
            else
            {
                DestoryPlayer(_player);
                PhotonNetwork.LeaveRoom();
                PhotonNetwork.LoadLevel("Lobby");
                PhotonNetwork.AutomaticallySyncScene = false;
            }
        }

        private void DestoryPlayer(Player player)
        {
            PhotonNetwork.Destroy(player.gameObject);
            _panel.Fire.onClick.RemoveListener(Fire);
            player.OnTriggerBullet -= Damage;
        }
        private void SpawnPlayer()
        {
            var playerObj = PhotonNetwork.Instantiate(_playerGameObject.name, 
                new Vector3(Random.Range(14, 42), -0.57f, 0), quaternion.identity);
            
            var player = playerObj.GetComponent<Player>();
            player.Health = _gameConfig.Health;
            player.speed = _gameConfig.PlayerSpeed;
            player.spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            
            _panel.AddPlayer(player);
            _panel.Fire.onClick.AddListener(Fire);
            
            player.OnTriggerBullet += Damage;
            player.OnTriggerCoin += _coinController.UpCoin;
            _player = player;
        }
        
        private void Fire()
        {
            var position = _player.BulletSpawn.transform.position;
            var bulletObject = PhotonNetwork.Instantiate(_bulletGameObject.name, new Vector3(position.x, position.y, 0), quaternion.identity);
            var bulletPhoton = bulletObject.GetComponent<PhotonView>();
            _bullet = bulletObject.GetComponent<BulletView>();
            if (bulletPhoton.IsMine)
            {
                var transform = _bullet.transform;
                var bulletRotation = _rotation;
                
                transform.position = position;
                transform.rotation = bulletRotation;
                
                if (_bullet.transform.rotation.y == 1)
                {
                    _bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.left * _gameConfig.BulletSpeed, ForceMode2D.Impulse);
                }
                else
                {
                    _bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.right * _gameConfig.BulletSpeed, ForceMode2D.Impulse);
                }
            }
        }
            

        private void Move()
        {
            if(_player.photonView.IsMine)
            {
                Vector2 direction = (Vector2.right * _joystick.Horizontal);
                _player.rigidbody.velocity = new Vector2(direction.x * _player.speed, 0);
                _player.transform.rotation = _rotation;
                if (direction.x < 0) 
                {
                    _rotation.y = 180;
                }
                else if (direction.x > 0)
                {
                    _rotation.y = 0;
                }
                
                _player.transform.rotation = _rotation;
                
                _player.Animator.SetFloat("velocityX", Mathf.Abs(direction.x) / _player.speed);
            }
        }
        public void OnFixedUpdate(float fixedDeltaTime)
        {
            if (_enabled)
            {
                Move();
                if (PhotonNetwork.PlayerList.Length == 1)
                {
                    _uiController.Victory(_player.photonView);
                    _enabled = false;
                }
            }
            
            
        }
    }
}