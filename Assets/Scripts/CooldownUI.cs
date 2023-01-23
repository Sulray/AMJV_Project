using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{
    private TMP_Text cooldownText;

    // Start is called before the first frame update
    void Start()
    {
        cooldownText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ShowCooldown(int cooldown)
    {
        while (cooldown != 0)
        {
            cooldownText.text = cooldown.ToString();
            yield return new WaitForSeconds(1);
            cooldown -= 1;
        }
        cooldownText.text = "";
    }
}
