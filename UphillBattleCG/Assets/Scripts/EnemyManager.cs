using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private GameObject tokenPrefab;
    [SerializeField] private List<UnitSO> FrontlineEnemies = new List<UnitSO>();
    [SerializeField] private List<UnitSO> BacklineEnemies = new List<UnitSO>();
    [SerializeField] private List<UnitSO> T2FrontlineEnemies = new List<UnitSO>();
    [SerializeField] private List<UnitSO> T2BacklineEnemies = new List<UnitSO>();
    [SerializeField] private List<UnitSO> T3FrontlineEnemies = new List<UnitSO>();
    [SerializeField] private List<UnitSO> T3BacklineEnemies = new List<UnitSO>();

    // Need smarter place
    // More balanced picks
    // Place enemies into their correct lanes

    public void PlaceEnemies(int num)
    {
        Debug.Log("Spawning " + num + "Enemies");
        StartCoroutine(PlaceEnemiesWPauses(num));
    }

    IEnumerator PlaceEnemiesWPauses(int num)
    {
        for (int i = 0; i < num; i++)
        {
            yield return new WaitForSeconds(0.5f);

            GameObject randSlot = boardManager.GetRandomOpenEnemySlot();

            if (randSlot == null) yield break;

            if (randSlot.GetComponent<TokenSlot>().position == TokenSlot.Position.Frontline)
            {
                var token = Instantiate<GameObject>(tokenPrefab, randSlot.transform);
                var rand = Random.Range(0, FrontlineEnemies.Count);
                token.GetComponent<TokenUnit>().SetUp(FrontlineEnemies[rand]);
                randSlot.GetComponent<TokenSlot>().SlotToken(token);
            } else if (randSlot.GetComponent<TokenSlot>().position == TokenSlot.Position.Backline)
            {
                var token = Instantiate<GameObject>(tokenPrefab, randSlot.transform);
                var rand = Random.Range(0, BacklineEnemies.Count);
                token.GetComponent<TokenUnit>().SetUp(BacklineEnemies[rand]);
                randSlot.GetComponent<TokenSlot>().SlotToken(token);
            }
        }
    }
}
