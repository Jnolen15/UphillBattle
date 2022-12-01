using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardAction : Card
{
    [Header("Action")]
    public ActionSO action;

    // ========= Visual Componenets =========
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Image art;
    private AudioManager audioManager;

    public override void SetUp()
    {
        cost.text = action.cost.ToString();
        title.text = action.title;
        description.text = action.description;

        art.sprite = action.art;
        art.rectTransform.localPosition = action.cardArtOffset;
        var size = new Vector3(action.cardArtSize, action.cardArtSize, action.cardArtSize);
        art.rectTransform.localScale = size;

        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void Awake()
    {
        if (action) SetUp();
    }

    public override void Play()
    {
        // Test if can be played
        if (playerManager.TryPlayActionCard(action))
        {
            audioManager.PlaySound(action.playedSound);
            Debug.Log("Played card: " + action.title);
            playerManager.CardPlayed();
            this.transform.parent.GetComponent<HandSlot>().Discard(false);
        }
        else
        {
            Debug.Log("Card could not be played");
            playerManager.CardPlayed();
        }
    }

    public override void OnDrag()
    {
        switch (action.position)
        {
            default:
                break;
            case ActionSO.Position.Anywhere:
                boardManager.HighlightSlots(action.needEmpty, action.targetAmbiguous, "Anywhere", "Versatile");
                break;
            case ActionSO.Position.Friendly:
                boardManager.HighlightSlots(action.needEmpty, action.targetAmbiguous, "Friendly", "Versatile");
                break;
            case ActionSO.Position.FFrontline:
                boardManager.HighlightSlots(action.needEmpty, action.targetAmbiguous, "Friendly", "Frontline");
                break;
            case ActionSO.Position.FBackline:
                boardManager.HighlightSlots(action.needEmpty, action.targetAmbiguous, "Friendly", "Backline");
                break;
            case ActionSO.Position.Enemy:
                boardManager.HighlightSlots(action.needEmpty, action.targetAmbiguous, "Enemy", "Versatile");
                break;
            case ActionSO.Position.EFrontline:
                boardManager.HighlightSlots(action.needEmpty, action.targetAmbiguous, "Enemy", "Frontline");
                break;
            case ActionSO.Position.EBackline:
                boardManager.HighlightSlots(action.needEmpty, action.targetAmbiguous, "Enemy", "Backline");
                break;
        }
    }
}
