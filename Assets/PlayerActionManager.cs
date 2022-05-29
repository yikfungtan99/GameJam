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
    [SerializeField] private GameObject throwIconCircle;

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

        throwIcon.color = new Color(1, 1, 1, 1.0f - (throwCooldownTime/throwCooldownDuration));

        if (throwCooldownTime <= 0)
        {
            throwIconCircle.gameObject.SetActive(true);
        }
        else
        {
            throwIconCircle.gameObject.SetActive(false);
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
