using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RoundController
{
    AI ai;
    RoundState currentState;
    UXController UX;
    List<string> turns = new List<string>();
    const int MAXTURNPERTEAM = 4;
    public RoundController(GameState gameState, AI ai, UXController ux)
    {
        for (int turnIndex = 0; turnIndex < MAXTURNPERTEAM; turnIndex++) {
            for (Team team = Team.Player; team <= Team.Npc; team++) {
                Turn turn = new Turn();
                int playersInTeam = gameState.CharacterInTeam(team);
                CharacterState characterState = gameState.CharacterAtIndex(turnIndex % playersInTeam,team);
                turns.Add(characterState.id);
            }
        }
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

        RoundState createRoundState = turn.ability.Use(turn.character, turn.targets, 0 ,currentState);
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
