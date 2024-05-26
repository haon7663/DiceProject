using System.Linq;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Creature : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public CreatureData creatureData;
    
    [Header("체력")]
    public float maxHp;
    public float curHp;
    public Image healthBar;
    public TMP_Text healthTMP;
    public TMP_Text valueTMP;

    public void SetUp()
    {
        maxHp = creatureData.hp;
        curHp = maxHp;
    }
    public IEnumerator CardCoroutine(CardData cardData, Creature target)
    {
        var value = 0;
        if (creatureData.creatureType == CreatureType.Enemy)
        {
            value = cardData.diceTypes.Aggregate(cardData.basicValue, (current, t) => current + DiceManager.inst.GetDiceValue(t));
        }
        else
        {
            yield return StartCoroutine(DiceManager.inst.SpinDice(cardData.diceTypes, cardData.basicValue));
            value = DiceManager.inst.totalValue + cardData.basicValue;
        }

        valueTMP.text = value.ToString();
        
        yield return YieldInstructionCache.WaitForSeconds(0.4f);
        target.OnDamage(value);

        var typeInt = creatureData.creatureType == CreatureType.Enemy ? 1 : -1;
        
        _spriteRenderer.sprite = creatureData.attackSprite;
        transform.position = new Vector3(typeInt * 0.8f, 2.25f);
        transform.DOMove(new Vector3(typeInt * 0.7f, 2.25f), 1);
        
        CameraMovement.inst.VibrationForTime(0.5f);
        CameraMovement.inst.ProductionAtTime(new Vector3(typeInt * -0.6f, 0.65f, -10), typeInt * -5f, 4f);
        
        yield return YieldInstructionCache.WaitForSeconds(1.5f);
        
        _spriteRenderer.sprite = creatureData.idleSprite;
        transform.position = new Vector3(typeInt * 1.5f, 2.25f);
        
        CameraMovement.inst.ProductionAtTime(new Vector3(0, 0, -10), 0, 5, true);
        valueTMP.text = "";
        TurnManager.inst.TurnEnd();
        
        DiceManager.inst.dicePanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(99999, 0);
        DiceManager.inst.cardPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        DiceManager.inst.DestroyDices();
    }
    
    public void OnDamage(int damage)
    {
        curHp -= damage;
        healthBar.fillAmount = curHp / maxHp;
        healthTMP.text = curHp + " / " + maxHp;
    }
}