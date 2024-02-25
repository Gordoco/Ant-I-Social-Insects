using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    [SerializeField] private SFX comboSounds;
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject progBar;

    private int[] comboMilestones = { 10, 20, 30, 40, 50, 60, 100 };
    private int combo = 0;
    private int lastMilestone = 0;
    private Vector2 initLocalPos;
    private float baseIncrease = 0.5f;
    private float movementFactor = 10;
    [SerializeField] private float increasedFactor = 0.1f;
    [SerializeField] private float rotationFactor = 0.1f;

    public event System.EventHandler ComboMilestoneAchieved;

    private void Start()
    {
        if (!text || !comboSounds || !progBar) Destroy(gameObject);
        progBar.GetComponent<Slider>().value = 0;
        initLocalPos = text.rectTransform.anchoredPosition;
    }

    private void Update()
    {
        progBar.GetComponent<Slider>().value -= Time.deltaTime * Mathf.Clamp(((float)combo)/30, 0, 0.75f);
        if (progBar.GetComponent<Slider>().value <= 0)
        {
            combo = 0;
            lastMilestone = 0;
        }
        text.text = "" + combo;
        CheckForCombo();
    }

    public void KillBee(bool bSword)
    {
        if (!bSword) combo++;
        else combo += 2;
        progBar.GetComponent<Slider>().value = 1;
    }

    private void CheckForCombo()
    {
        if (lastMilestone < comboMilestones.Length && combo >= comboMilestones[lastMilestone])
        {
            StopAllCoroutines();
            text.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            text.rectTransform.localScale = new Vector3(1, 1, 1);
            text.rectTransform.anchoredPosition = initLocalPos;
            StartCoroutine(ComboTextAnimation(lastMilestone));
            lastMilestone++;
            OnComboMilestoneAchieved();
        }
    }

    private IEnumerator ComboTextAnimation(int severity)
    {
        int rand = Random.Range(0, 2) == 1 ? 1 : -1;
        for (int i = 0; i < 24; i++) {
            text.rectTransform.rotation = Quaternion.Euler(text.rectTransform.rotation.eulerAngles + new Vector3(0, 0, (lastMilestone * rotationFactor) + rand));
            text.rectTransform.localScale += new Vector3(baseIncrease + (increasedFactor * lastMilestone), baseIncrease + (increasedFactor * lastMilestone), 0);
            text.rectTransform.anchoredPosition += new Vector2(0, -movementFactor);
            yield return new WaitForSeconds(1/12);
        }
        for (int i = 0; i < 24; i++)
        {
            text.rectTransform.rotation = Quaternion.Euler(text.rectTransform.rotation.eulerAngles + new Vector3(0, 0, -(rand + (lastMilestone * rotationFactor))));
            text.rectTransform.localScale += new Vector3(-(baseIncrease + (increasedFactor * lastMilestone)), -(baseIncrease + (increasedFactor * lastMilestone)), 0);
            text.rectTransform.anchoredPosition += new Vector2(0, movementFactor);
            yield return new WaitForSeconds(1 / 12);
        }
    }

    protected virtual void OnComboMilestoneAchieved() =>
        ComboMilestoneAchieved?.Invoke(this, System.EventArgs.Empty);
}
