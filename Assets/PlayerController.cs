using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public Camera Camera;
    public GameObject Head;
    public GameObject Bullet;
    public GameObject Enemy;
    private Random _random = new Random();

    private const int JumpForce = 300 * 5;
    private const float MoveForce = 4f;
    private DateTime _next = DateTime.Now;

    // Start is called before the first frame update
    void Start()
    {
        this._rigidbody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateController.GameState != GameState.Active)
        {
            if (Input.GetKeyDown("space"))
            {
                HpController.ResetHp();
                SceneManager.LoadScene(this.gameObject.scene.name);
            }
            return;
        }

        if (DateTime.Now > this._next)
        {
            this._next = DateTime.Now.AddSeconds(0.7f);
            var cam = this.Camera.GetComponent<Camera>();
            float height = 2f * cam.orthographicSize;
            float width = height * cam.aspect;
            var radius = Math.Max(height, width) / 2;
            var degrees = this._random.Next(0, 360);
            var radians = degrees * Mathf.Deg2Rad;
            var x = Mathf.Cos(radians) * radius;
            var y = Mathf.Sin(radians) * radius;
            Debug.Log(x + " " + y);
            var obj = Instantiate(this.Enemy, new Vector3(x, y, 0), Quaternion.identity);
            obj.transform.up = ((Vector2) this.gameObject.transform.position - (Vector2) obj.transform.position)
                .normalized;
        }

        var mousePos = (Vector2) this.Camera.ScreenToWorldPoint(Input.mousePosition);
        var forward = (mousePos - (Vector2) this.Head.transform.position).normalized;
        this.Head.transform.up = forward;

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(this.Bullet, this.Head.transform.position + this.Head.transform.up, this.Head.transform.rotation);
        }

        if (this._rigidbody.velocity.y == 0 && Input.GetKeyDown("space"))
        {
            this._rigidbody.AddForce(Vector2.up * JumpForce);
        }

        var horizontalMovement = Input.GetAxisRaw("Horizontal");
        this.gameObject.transform.position +=
            (Vector3) (Vector2.right * MoveForce * horizontalMovement * Time.deltaTime);
    }
}