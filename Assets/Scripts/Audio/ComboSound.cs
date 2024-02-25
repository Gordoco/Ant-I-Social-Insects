using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSound : MonoBehaviour
{
    [SerializeField] private ComboManager _comboManager;
    [SerializeField] private SFX _combo;

    private void Awake()
    {
        _comboManager.ComboMilestoneAchieved += ComboManager_ComboMilestoneAchieved;
    }

    private void ComboManager_ComboMilestoneAchieved(object sender, System.EventArgs e)
    {
        if (_combo != null) SFXPlayer.PlaySFX(_combo);
    }
}
