using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : Singletone<GameStateManager>
{
    public GAME_STATE gameState { get; private set; }

    public enum GAME_STATE
    { 
        Attack,
        Build,
        Choosing
    }

    protected override void Awake()
    {
        base.Awake(); 

        gameState = GAME_STATE.Choosing;
    }

    public void SetGameStateBuild()
    {
        gameState = GAME_STATE.Build;
        Debug.Log(gameState);
    }
    
    public void SetGameStateAttack()
    {
        gameState = GAME_STATE.Attack;
        Debug.Log(gameState);
    }
    
    public void SetGameStateChoosing()
    {
        gameState = GAME_STATE.Choosing;
        Debug.Log(gameState);
    }

    public bool IsGameStateBuild()
    { 
        return gameState == GAME_STATE.Build;
    }
}
