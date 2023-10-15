using System;
using Code.CoinSystem;
using Photon.Pun;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Health;
    public float speed;
    
    public Rigidbody2D rigidbody;
    public Animator Animator;
    public GameObject BulletSpawn;
    public SpriteRenderer spriteRenderer;
    public PhotonView photonView;
    
    public event Action OnTriggerBullet;
    public event Action<Coin> OnTriggerCoin;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bullet"))
            OnTriggerBullet?.Invoke();
        
        if(other.CompareTag("Coin"))
            OnTriggerCoin?.Invoke(other.GetComponent<Coin>());
    }
    
}