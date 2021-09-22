using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController
{
    AI ai;
    RoundState currentState;
    UXController UX;


    public RoundController(GameState gameState, AI ai, UXController ux)
    {
        
        this.ai = ai;
        this.UX = ux;
        this.currentState = new RoundState(gameState);

    }

    private RoundState RoundState(GameState gameState)
    {
        throw new NotImplementedException();
    }

    void StartPlayer() { }

    void processTurn(Turn turn)
    {

        RoundState createRoundState = turn.ability.Use(turn.character, turn.targets, currentState);
        UX.processTurn(turn, this, createRoundState);
        currentState = createRoundState;
    }

    void StartNPCTurn()
    {
        try
        {
            Turn turn = ai.GetTurn(currentState);
            processTurn(turn);
        }
        catch (TurnNotFoundException _)
        {

        }
    }
}
