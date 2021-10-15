using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Timers;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

public class EnemyController : MonoBehaviour
{
    private GameObject _player;
    private Rigidbody2D _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        this._player = GameObject.FindWithTag("Player");
        this._rigidbody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.up = (this._player.transform.position - this.gameObject.transform.position).normalized;
        this._rigidbody.velocity = GameStateController.GameState == GameState.Active ? this.gameObject.transform.up * 3 : Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        Destroy(this.gameObject);
        HpController.ModHp(-1);
    }
}
