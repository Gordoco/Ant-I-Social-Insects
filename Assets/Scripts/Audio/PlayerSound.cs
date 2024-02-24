using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] private PlayerController _pc;
    [SerializeField] private SFX _swordHit;
    [SerializeField] private SFX _swordSwing;

    private void Awake()
    {
        _pc.SwordHit += Pc_SwordHit;
        _pc.SwingSword += Pc_SwingSword;
    }

    private void Pc_SwingSword(object sender, System.EventArgs e)
    {
        SFXPlayer.PlaySFX(_swordSwing);
    }

    private void Pc_SwordHit(object sender, System.EventArgs e)
    {
        // SFXPlayer.PlaySFX(_swordHit);
    }
}
