using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private GameObject tokenPrefab;
    [SerializeField] private List<UnitSO> FrontlineEnemies = new List<UnitSO>();
    [SerializeField] private List<UnitSO> BacklineEnemies = new List<UnitSO>();
    [SerializeField] private List<UnitSO> T2FrontlineEnemies = new List<UnitSO>();
    [SerializeField] private List<UnitSO> T2BacklineEnemies = new List<UnitSO>();
    [SerializeField] private List<UnitSO> T3FrontlineEnemies = new List<UnitSO>();
    [SerializeField] private List<UnitSO> T3BacklineEnemies = new List<UnitSO>();
    private int lastRoundKills;
    private int lastRoundDeaths;

    // Need smarter place
    // More balanced picks
    // Place enemies into their correct lanes

    private void Awake()
    {
        playerManager = gameObject.GetComponent<PlayerManager>();
    }

    public void Reinforce()
    {
        if (lastRoundKills < playerManager.Kills || lastRoundDeaths < playerManager.Deaths)
        {
            var killdiff = playerManager.Kills - lastRoundKills;
            var deathdiff = playerManager.Deaths - lastRoundDeaths;
            var numEnemies = killdiff - deathdiff;
            if (numEnemies < 0) numEnemies = 0;
            if (boardManager.GetNumEnemies() < boardManager.GetNumPlayers())
            {
                Debug.Log("More player units than enemies, +1 enemy");
                numEnemies++;
            } else if (boardManager.GetNumEnemies() > boardManager.GetNumPlayers())
            {
                Debug.Log("More Enemies than player units, -1 enemy");
                numEnemies--;
            }
            Debug.Log("kills this round: " + killdiff + " Deaths this round: " + deathdiff + " number of enemies to spawn: " + numEnemies);
            PlaceEnemies(numEnemies);
            lastRoundKills = playerManager.Kills;
            lastRoundDeaths = playerManager.Deaths;
        }
        else
        {
            PlaceEnemies(1);
        }
    }

    public void PlaceEnemies(int num)
    {
        if (playerManager.Kills < 12)
        {
            Debug.Log("Spawning " + num + " T1 Enemies");
            StartCoroutine(PlaceEnemiesWPauses(num, FrontlineEnemies, BacklineEnemies));
        }
        else if (playerManager.Kills >= 12 && playerManager.Kills < 20)
        {
            Debug.Log("Spawning " + num + " T2 Enemies");
            StartCoroutine(PlaceEnemiesWPauses(num, T2FrontlineEnemies, T2BacklineEnemies));
        }
        else if (playerManager.Kills >= 20)
        {
            Debug.Log("Spawning " + num + " T3 Enemies");
            StartCoroutine(PlaceEnemiesWPauses(num, T3FrontlineEnemies, T3BacklineEnemies));
        }
    }

    IEnumerator PlaceEnemiesWPauses(int num, List<UnitSO> frontlineEnemies, List<UnitSO> backlineEnemies)
    {
        for (int i = 0; i < num; i++)
        {
            yield return new WaitForSeconds(0.5f);

            GameObject randSlot = boardManager.GetRandomOpenEnemySlot();

            if (randSlot == null) yield break;

            if (randSlot.GetComponent<TokenSlot>().position == TokenSlot.Position.Frontline)
            {
                var token = Instantiate<GameObject>(tokenPrefab, randSlot.transform);
                var rand = Random.Range(0, frontlineEnemies.Count);
                token.GetComponent<TokenUnit>().SetUp(frontlineEnemies[rand]);
                randSlot.GetComponent<TokenSlot>().SlotToken(token);
            } else if (randSlot.GetComponent<TokenSlot>().position == TokenSlot.Position.Backline)
            {
                var token = Instantiate<GameObject>(tokenPrefab, randSlot.transform);
                var rand = Random.Range(0, backlineEnemies.Count);
                token.GetComponent<TokenUnit>().SetUp(backlineEnemies[rand]);
                randSlot.GetComponent<TokenSlot>().SlotToken(token);
            }
        }
    }
}
