using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActionManager : MonoBehaviour
{
    private static PlayerActionManager instance;
    public static PlayerActionManager Instance { get { return instance; } }

    [SerializeField] private float throwCooldownDuration;
    [HideInInspector] public float throwCooldownTime;

    [SerializeField] private Image throwIcon;
    [SerializeField] private Image throwIconCircle;

    [SerializeField] private Color cooldownColor;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        throwCooldownTime = 0;
    }

    private void Update()
    {
        UpdateThrowCooldown();

        if (throwCooldownTime <= 0)
        {
            throwIcon.color = Color.white;
            throwIconCircle.color = Color.white;
        }
        else
        {
            throwIcon.color = cooldownColor;
            throwIconCircle.color = cooldownColor;
        }
    }

    public void ThrowCooldown()
    {
        throwCooldownTime = throwCooldownDuration;
    }

    private void UpdateThrowCooldown()
    {
        if(throwCooldownTime > 0)
        {
            throwCooldownTime -= Time.deltaTime;
        }
        else
        {
            throwCooldownTime = 0;
        }
    }
}
