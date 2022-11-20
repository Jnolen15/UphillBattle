using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private CardManager cardManager;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private EnemyManager enemyManager;
    private bool firstTurn;

    [SerializeField] private GameObject EndTurnButton;

    public enum State
    {
        //Growdeck,
        Setup,
        Play,
        PCombat,
        ECombat,
        Reinforce
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
        enemyManager = this.GetComponent<EnemyManager>();

        firstTurn = true;
    }

    private void Start()
    {
        ActivatePhase();
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

    public void AdvacePhase()
    {
        switch (state)
        {
            /*case State.Growdeck:
                break;*/
            case State.Setup:
                state = State.Play;
                break;
            case State.Play:
                state = State.PCombat;
                break;
            case State.PCombat:
                state = State.ECombat;
                break;
            case State.ECombat:
                state = State.Reinforce;
                break;
            case State.Reinforce:
                state = State.Setup;
                break;
        }

        ActivatePhase();
    }

    private void ActivatePhase()
    {
        switch (state)
        {
            /*case State.Growdeck:
                break;*/
            case State.Setup:
                if (firstTurn)
                {
                    playerManager.Gold += 5;
                    playerManager.Courage += 5;
                    cardManager.DrawCards(5);
                    enemyManager.PlaceEnemies(3);
                    firstTurn = false;
                } else
                {
                    playerManager.Gold += 2;
                    playerManager.Courage -= 1;
                    cardManager.DrawCards(2);
                }
                StartCoroutine(PauseTillNextState(2f));
                break;
            case State.Play:
                EndTurnButton.SetActive(true);
                break;
            case State.PCombat:
                EndTurnButton.SetActive(false);
                boardManager.PlayerCombat();
                StartCoroutine(PauseTillNextState(2f));
                break;
            case State.ECombat:
                boardManager.EnemyCombat();
                StartCoroutine(PauseTillNextState(2f));
                break;
            case State.Reinforce:
                enemyManager.PlaceEnemies(1);
                StartCoroutine(PauseTillNextState(2f));
                break;
        }
    }

    IEnumerator PauseTillNextState(float pauseTime)
    {
        yield return new WaitForSeconds(pauseTime);
        AdvacePhase();
    }
}
