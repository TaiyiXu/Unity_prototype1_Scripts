using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaBar;

    private int maxStamina = 10000;
    private int currentStamina;

    public static StaminaBar instance;

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regen;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;


    }

    public void useStamina(int amount)
    {
        if(currentStamina- amount >= 0)
        {
            currentStamina -= amount;
            staminaBar.value = currentStamina;

            if (regen != null)
                StopCoroutine(RegenStamina());

            regen = StartCoroutine(RegenStamina());
        }
        else
        {
            Debug.Log("not enough stamina");
        }


    }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(2);

        while(currentStamina< maxStamina)
        {
            currentStamina += maxStamina / 1000;
            staminaBar.value = currentStamina;
            yield return regenTick;
        }

        regen = null;
    }

    public int getCurrentStamina()
    {
        return currentStamina;
    }
}
