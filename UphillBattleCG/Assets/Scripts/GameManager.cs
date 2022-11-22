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
    [SerializeField] private List<int> killRewards = new List<int>();
    private bool firstTurn;

    [SerializeField] private GameObject EndTurnButton;
    [SerializeField] private GameObject GrowDeckMenu;
    [SerializeField] private GameObject ulockOption1;
    [SerializeField] private GameObject ulockOption2;

    public enum State
    {
        Growdeck,
        Setup,
        Play,
        PCombat,
        ECombat,
        Reinforce
    }
    public State state;

    // FOR TESTING
    public TextMeshProUGUI phase;

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

    public void AdvacePhase()
    {
        switch (state)
        {
            case State.Growdeck:
                state = State.Setup;
                break;
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
                state = State.Growdeck;
                break;
        }

        phase.text = state.ToString();
        ActivatePhase();
    }

    private void ActivatePhase()
    {
        switch (state)
        {
            case State.Growdeck:
                if (killRewards.Count > 0)
                {
                    if (playerManager.Kills >= killRewards[0])
                    {
                        GrowDeck();
                    }
                    else
                        AdvacePhase();
                }
                else
                    AdvacePhase();
                break;
            case State.Setup:
                // Innitial turn resources
                if (firstTurn)
                {
                    playerManager.Gold += 5;
                    playerManager.Courage += 5;
                    playerManager.Health = 20;
                    cardManager.DrawCards(5);
                    enemyManager.PlaceEnemies(3);
                    firstTurn = false;
                } else
                {
                    playerManager.Gold += 2;
                    playerManager.Courage -= 1;
                    if (playerManager.Courage < 0) playerManager.Health += playerManager.Courage;
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
                //StartCoroutine(PauseTillNextState(2f));
                break;
            case State.ECombat:
                boardManager.EnemyCombat();
                //StartCoroutine(PauseTillNextState(2f));
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

    private void GrowDeck()
    {
        if (killRewards.Count > 0)
        {
            if (playerManager.Kills >= killRewards[0])
            {
                Debug.Log("Kill reward for " + killRewards[0]);
                killRewards.RemoveAt(0);
                GrowDeckMenu.SetActive(true);
                ulockOption1 = cardManager.GetUnlockableActionCard();
                ulockOption2 = cardManager.GetUnlockableActionCard();

                GrowDeckMenu.transform.GetChild(1).gameObject.GetComponent<CardVisual>().SetUp(ulockOption1.GetComponent<CardAction>().action);
                GrowDeckMenu.transform.GetChild(2).gameObject.GetComponent<CardVisual>().SetUp(ulockOption2.GetComponent<CardAction>().action);
            }
        }
    }

    public void CloseGrowDeckMenu()
    {
        GrowDeckMenu.SetActive(false);
        ActivatePhase();
    }

    public void ChooseGrowDeck(int choice)
    {
        if(choice == 1)
        {
            cardManager.AddToDeck(ulockOption1);
        }
        else if (choice == 2)
        {
            cardManager.AddToDeck(ulockOption2);
        }

        ulockOption1 = null;
        ulockOption2 = null;
    }
}
