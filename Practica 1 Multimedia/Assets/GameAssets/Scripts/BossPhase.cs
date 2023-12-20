using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossPhase : MonoBehaviour
{
    public enum BossState { IDLE, ATTACKING }

    protected EnemyBoss boss;
    private BossState state;

    public BossState State
    {
        get => state;
        set
        {
            state = value;
            if (state == BossState.IDLE)
                boss.NotifyIdleState();
        }
    }


    protected virtual void Start()
    {
        boss = GetComponent<EnemyBoss>();
    }


}
