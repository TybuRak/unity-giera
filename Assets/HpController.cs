using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class HpController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    static HpController()
    {
        GameStateController.GameStateChanged += GameStateChange;
    }

    private static void GameStateChange(GameState gameState)
    {
        GameObject.Find("GameOver").GetComponent<Text>().enabled = gameState == GameState.GameOver;
    }

    public static void ModHp(int mod)
    {
        Hp += mod;
        if (Hp < 0) Hp = 0;
        UpdateText();
        GameStateController.SetGameState(Hp <= 0 ? GameState.GameOver : GameState.Active);
    }

    public static void SetHp(int hp)
    {
        ModHp(hp - Hp);
    }

    public static void ResetHp()
    {
        SetHp(5);
    }

    public static int Hp = 5;

    private static void UpdateText()
    {
        GameObject.Find("Hp").GetComponent<Text>().text = "HP: " + Hp;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) ModHp(-Hp);
        else if (other.CompareTag("Bullet")) Destroy(other.gameObject);
    }
}
