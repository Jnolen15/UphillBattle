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
    public GameObject tokenPrefab;
    public UnitSO goblinData;
    public GameObject Slot1;
    public GameObject Slot2;
    public GameObject Slot3;

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
        if (Input.GetKeyDown(KeyCode.E))
        {
            var token = Instantiate<GameObject>(tokenPrefab, Slot1.transform);
            token.GetComponent<TokenUnit>().SetUp(goblinData);
            Slot1.GetComponent<TokenSlot>().SlotToken(token);

            var token2 = Instantiate<GameObject>(tokenPrefab, Slot2.transform);
            token2.GetComponent<TokenUnit>().SetUp(goblinData);
            Slot2.GetComponent<TokenSlot>().SlotToken(token2);

            var token3 = Instantiate<GameObject>(tokenPrefab, Slot3.transform);
            token3.GetComponent<TokenUnit>().SetUp(goblinData);
            Slot3.GetComponent<TokenSlot>().SlotToken(token3);
        }
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
