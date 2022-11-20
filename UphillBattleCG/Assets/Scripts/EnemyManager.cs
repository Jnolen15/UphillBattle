using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private GameObject tokenPrefab;
    [SerializeField] private List<UnitSO> AvailableEnemies = new List<UnitSO>();

    // Need smarter place
    // More balanced picks
    // Place enemies into their correct lanes

    public void PlaceEnemies(int num)
    {
        StartCoroutine(PlaceEnemiesWPauses(num));
    }

    IEnumerator PlaceEnemiesWPauses(int num)
    {
        for (int i = 0; i < num; i++)
        {
            yield return new WaitForSeconds(0.5f);

            GameObject randSlot = boardManager.GetRandomOpenEnemySlot();

            if (randSlot == null) yield break;

            var token = Instantiate<GameObject>(tokenPrefab, randSlot.transform);
            var rand = Random.Range(0, AvailableEnemies.Count);
            token.GetComponent<TokenUnit>().SetUp(AvailableEnemies[rand]);
            randSlot.GetComponent<TokenSlot>().SlotToken(token);
        }
    }
}
