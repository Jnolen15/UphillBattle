using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private CardManager cardManager;
    [SerializeField] private PlayerManager playerManager;

    public enum State
    {
        //Growdeck,
        //Setup,
        Play,
        PCombat,
        ECombat
        //Reinforce
    }
    public State state;

    // FOR TESTING
    public TextMeshProUGUI phase;

    void Awake()
    {
        cardManager = this.GetComponent<CardManager>();
        playerManager = this.GetComponent<PlayerManager>();
    }

    void Update()
    {
        // For Testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AdvacePhase();
            ActivatePhase();
        }
        phase.text = state.ToString();
    }

    private void AdvacePhase()
    {
        switch (state)
        {
            /*case State.Growdeck:
                break;
            case State.Setup:
                break;*/
            case State.Play:
                state = State.PCombat;
                break;
            case State.PCombat:
                state = State.ECombat;
                break;
            case State.ECombat:
                state = State.Play;
                break;
                //case State.Reinforce:
                //    break;
        }
    }

    private void ActivatePhase()
    {
        switch (state)
        {
            /*case State.Growdeck:
                break;
            case State.Setup:
                break;*/
            case State.Play:
                break;
            case State.PCombat:
                boardManager.PlayerCombat();
                break;
            case State.ECombat:
                boardManager.EnemyCombat();
                break;
                //case State.Reinforce:
                //    break;
        }
    }
}
