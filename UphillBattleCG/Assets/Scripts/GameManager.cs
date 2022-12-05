using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public BoardManager boardManager;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private CardManager cardManager;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private List<int> killRewards = new List<int>();
    private bool firstTurn;

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject cardPreview;
    [SerializeField] private GameObject EndTurnButton;
    [SerializeField] private GameObject GrowDeckMenu;
    [SerializeField] private GameObject ulockOption1;
    [SerializeField] private GameObject ulockOption2;
    private bool hadFirstUpgrde;
    private bool hadSecondUpgrde;
    private bool wonGame;

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip DealCards;
    [SerializeField] private AudioClip GiveCoins;

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

    private void Update()
    {
        // Wait for Lost
        if (playerManager.Health <= 0)
        {
            LooseGame();
        }

        // Wait for Win
        if (playerManager.Kills >= 30 && !wonGame)
        {
            wonGame = true;
            WinGame();
        }
    }

    // ============== SCENE MANAGEMENT ==============
    public void StartGame()
    {
        menuManager.CloseMain();
        ActivatePhase();
    }

    public void LooseGame()
    {
        menuManager.OpenLost();
    }

    public void WinGame()
    {
        menuManager.OpenWon();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ============== GAME STATES ==============
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
                if (playerManager.Kills >= 12 && !hadFirstUpgrde)
                {
                    UpgradeUnits();
                }
                else if (playerManager.Kills >= 20 && !hadSecondUpgrde)
                {
                    UpgradeUnits();
                }
                else if (killRewards.Count > 0)
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
                source.PlayOneShot(DealCards);
                source.PlayOneShot(GiveCoins);
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
                    cardManager.DrawCards(3);
                }
                StartCoroutine(PauseTillNextState(2f));
                break;
            case State.Play:
                EndTurnButton.SetActive(true);
                break;
            case State.PCombat:
                EndTurnButton.SetActive(false);
                boardManager.PlayerTurn();
                boardManager.PlayerCombat();
                break;
            case State.ECombat:
                boardManager.EnemyTurn();
                boardManager.EnemyCombat();
                break;
            case State.Reinforce:
                enemyManager.Reinforce();

                StartCoroutine(PauseTillNextState(2f));
                break;
        }
    }

    IEnumerator PauseTillNextState(float pauseTime)
    {
        yield return new WaitForSeconds(pauseTime);
        AdvacePhase();
    }

    // ============== DECK UPGRADING ==============
    private void GrowDeck()
    {
        if (killRewards.Count > 0)
        {
            if (playerManager.Kills >= killRewards[0])
            {
                Debug.Log("Kill reward for " + killRewards[0]);
                killRewards.RemoveAt(0);
                GrowDeckMenu.SetActive(true);
                GrowDeckMenu.transform.GetChild(1).gameObject.SetActive(true);
                GrowDeckMenu.transform.GetChild(2).gameObject.SetActive(false);
                ulockOption1 = cardManager.GetUnlockableActionCard();
                ulockOption2 = cardManager.GetUnlockableActionCard();

                GrowDeckMenu.transform.GetChild(1).GetChild(0).gameObject.GetComponent<ActionCardVisual>().SetUp(ulockOption1.GetComponent<CardAction>().action);
                GrowDeckMenu.transform.GetChild(1).GetChild(1).gameObject.GetComponent<ActionCardVisual>().SetUp(ulockOption2.GetComponent<CardAction>().action);
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
            cardManager.UnlockableActionCards.Add(ulockOption2);
        }
        else if (choice == 2)
        {
            cardManager.AddToDeck(ulockOption2);
            cardManager.UnlockableActionCards.Add(ulockOption1);
        }

        ulockOption1 = null;
        ulockOption2 = null;
    }

    private void UpgradeUnits()
    {
        GrowDeckMenu.SetActive(true);
        GrowDeckMenu.transform.GetChild(1).gameObject.SetActive(false);
        GrowDeckMenu.transform.GetChild(2).gameObject.SetActive(true);

        if (!hadFirstUpgrde)
        {
            GrowDeckMenu.transform.GetChild(2).GetChild(0).gameObject.GetComponent<UnitCardVisual>().SetUp(cardManager.UpgradeCards[1]);
            GrowDeckMenu.transform.GetChild(2).GetChild(1).gameObject.GetComponent<UnitCardVisual>().SetUp(cardManager.UpgradeCards[2]);
        } else if (!hadSecondUpgrde)
        {
            GrowDeckMenu.transform.GetChild(2).GetChild(0).gameObject.GetComponent<UnitCardVisual>().SetUp(cardManager.UpgradeCards[4]);
            GrowDeckMenu.transform.GetChild(2).GetChild(1).gameObject.GetComponent<UnitCardVisual>().SetUp(cardManager.UpgradeCards[5]);
        }
    }

    public void ChooseUpgrade(GameObject unit)
    {
        Debug.Log("Chose upgrade " + unit.GetComponent<UnitCardVisual>().unit);

        if (!hadFirstUpgrde)
        {
            cardManager.UpgradeUnits(cardManager.UpgradeCards[0], unit.GetComponent<UnitCardVisual>().unit);
            cardManager.GetExtraUnitCard(unit.GetComponent<UnitCardVisual>().unit);
        }
        else if (!hadSecondUpgrde)
        {
            cardManager.UpgradeUnits(cardManager.UpgradeCards[3], unit.GetComponent<UnitCardVisual>().unit);
            cardManager.GetExtraUnitCard(unit.GetComponent<UnitCardVisual>().unit);
        }

        if (!hadFirstUpgrde)
            hadFirstUpgrde = true;
        else if (!hadSecondUpgrde)
            hadSecondUpgrde = true;
    }

    // ============== CARD PREVIEW ==============
    public void PreviewCard(UnitSO data)
    {
        cardPreview.GetComponent<UnitCardVisual>().SetUp(data);
        cardPreview.SetActive(true);
    }

    public void ClosePreview()
    {
        cardPreview.SetActive(false);
    }
}
