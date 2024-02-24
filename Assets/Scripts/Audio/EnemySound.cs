using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    [SerializeField] private BaseBee _baseEnemy;
    [SerializeField] private SFXPlayer _sfxPlayer;

    [Space(10)]

    [SerializeField] private SFX _bounce;
    [SerializeField] private SFX _damage;
    [SerializeField] private SFX _kill;

    [SerializeField] private SFX _spawned;
    [SerializeField] private SFX _dead;

    private void Awake()
    {
        _baseEnemy.Spawned += BaseEnemy_Spawned;
        _baseEnemy.Dead += BaseEnemy_Dead;
        _baseEnemy.Interaction += BaseEnemy_Interaction;
    }

    private void BaseEnemy_Interaction(object sender, FInteraction e)
    {
        SFX sfx = null;
        switch (e.result)
        {
            case EInteractionResult.Bounce:
                sfx = _bounce;
                break;
            case EInteractionResult.Damage:
                sfx = _damage;
                break;
            case EInteractionResult.Kill:
                sfx = _kill;
                break;
        }

        if (sfx == null) return;
        _sfxPlayer.PlaySFX(sfx);
    }

    private void BaseEnemy_Dead(object sender, System.EventArgs e)
    {
        if (_dead == null) return;
        _sfxPlayer.PlaySFX(_dead);
    }

    private void BaseEnemy_Spawned(object sender, System.EventArgs e)
    {
        if (_spawned == null) return;
        _sfxPlayer.PlaySFX(_spawned);
    }
}
