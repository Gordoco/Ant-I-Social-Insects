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

    private int combo = 0;

    private void Start()
    {
        if (!text || !comboSounds || !progBar) Destroy(gameObject);
        progBar.GetComponent<Slider>().value = 0;
    }

    private void Update()
    {
        progBar.GetComponent<Slider>().value -= Time.deltaTime * Mathf.Clamp(((float)combo)/30, 0, 0.75f);
        if (progBar.GetComponent<Slider>().value <= 0) combo = 0;
        text.text = "" + combo;
    }

    public void KillBee(bool bSword)
    {
        if (!bSword) combo++;
        else combo += 2;
        progBar.GetComponent<Slider>().value = 1;
    }
}
