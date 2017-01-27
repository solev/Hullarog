using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public PlayerAbilities pAbilities;
    public PlayerHealth ph;
    public Image abilityCooldown1, abilityCooldown2, abilityCooldown3, abilityCooldown4;
    public Slider StaminaSlider, HealthSlider;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float cld1 = Mathf.Clamp((pAbilities.getCooldownMax(Ability.First) - pAbilities.getCooldown(Ability.First)) / pAbilities.getCooldownMax(Ability.First), 0, 1);
        abilityCooldown1.fillAmount = cld1;
        abilityCooldown1.transform.parent.GetComponent<Button>().interactable = !(cld1 > 0);

        float cld2 = Mathf.Clamp((pAbilities.getCooldownMax(Ability.Second) - pAbilities.getCooldown(Ability.Second)) / pAbilities.getCooldownMax(Ability.Second), 0, 1);
        abilityCooldown2.fillAmount = cld2;
        abilityCooldown2.transform.parent.GetComponent<Button>().interactable = !(cld2 > 0);

        float cld3 = Mathf.Clamp((pAbilities.getCooldownMax(Ability.Third) - pAbilities.getCooldown(Ability.Third)) / pAbilities.getCooldownMax(Ability.Third), 0, 1);
        abilityCooldown3.fillAmount = cld3;
        abilityCooldown3.transform.parent.GetComponent<Button>().interactable = !(cld3 > 0);


        HealthSlider.value = Mathf.Clamp(ph.currentHealth / ph.totalHealth, 0, 1);
        StaminaSlider.value = Mathf.Clamp((pAbilities.getStamina() / pAbilities.getTotalStamina()), 0, 1);        
    }

        
}
