using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Ability
{

    int level = 0;
    string name;

    float cooldown;
    float coolDownTick;

    public bool active = false;
    public bool canUse = true;

    public int index;

    public GameObject uiSlot;
    public TextMeshProUGUI uiName;
    public TextMeshProUGUI uiCooldown;

    GameObject abilities;
    

    public Ability(string name, float cooldown, int level, int index)
    {
        this.name = name;
        this.cooldown = cooldown;
        this.index = index;
        this.level = level;
        abilities = GameObject.Find("Abilities");

        SetUpUI();
    }

    public Ability(string name, float cooldown, int index)
    {
        this.name = name;
        this.cooldown = cooldown;
        this.index = index;
        abilities = GameObject.Find("Abilities");

        SetUpUI();
    }

    public void SetUpUI()
    {
        uiSlot = GameObject.Find(index.ToString());
        uiName = uiSlot.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        uiCooldown = uiSlot.transform.Find("Cooldown").GetComponent<TextMeshProUGUI>();
        Debug.Log("Set up stuff " + index);
        uiName.text = name;

    }

    public void Fire(GameObject plr)
    {
        if (active) { Debug.Log("Skill is currently being used"); return; }
        active = true;
        canUse = false;

        switch (name)
        {
            case "IceShot":
                Debug.Log("FIREEEEEEEEBALLLU");
                abilities.GetComponent<IceShot>().enabled = true;
                //abilities.GetComponent<TextMeshProUGUI>().enabled = false;
                break;
            //plr.GetComponent<name>()
            default:
                break;
        }

        coolDownTick = cooldown;
        active = false;
    }

    public IEnumerator StartCoolDown()
    {
        Debug.Log("Started cool down for " + name + " (" + cooldown + ")");
        int interval = 50;

        for (int i = 0; i < 1000; i++)
        {
            //Debug.Log("Ticking cooldown.. (" + coolDownTick + ")");
            yield return new WaitForSeconds(cooldown / interval);
            coolDownTick -= cooldown / interval;

            uiCooldown.text = System.Math.Round((decimal)coolDownTick, 2).ToString(); ;

            if (coolDownTick <= 0) { break; }
        }

        coolDownTick = 0;
        uiCooldown.text = "";

        canUse = true;
        Debug.Log("Cooldown ended!");

    }
}


public class Backpack : MonoBehaviour
{
    Ability clickedAbility;
    public List<Ability> activeAbilities = new List<Ability>();
    GameObject backPackUI;

    private void Start()
    {

        backPackUI = GameObject.Find("Backpack");
        Ability fireBall = new Ability("IceShot", 2, activeAbilities.Count + 1);
        activeAbilities.Add(fireBall);

        Ability Punch = new Ability("Pcunh", 1, activeAbilities.Count + 1);

        activeAbilities.Add(Punch);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            clickedAbility = activeAbilities[0];
        }
        else if (Input.GetKeyDown("2"))
        {
            clickedAbility = activeAbilities[1];
        }
        else if (Input.GetKeyDown("3"))
        {
            clickedAbility = activeAbilities[2];
        }
        else if (Input.GetKeyDown("4"))
        {
            clickedAbility = activeAbilities[3];
        }
        else if (Input.GetKeyDown("5"))
        {
            clickedAbility = activeAbilities[4];
        }
        else if (Input.GetKeyDown("6"))
        {
            clickedAbility = activeAbilities[5];
        }
        else if (Input.GetKeyDown("7"))
        {
            clickedAbility = activeAbilities[6];
        }
        else if (Input.GetKeyDown("8"))
        {
            clickedAbility = activeAbilities[7];
        }
        else if (Input.GetKeyDown("9"))
        {
            clickedAbility = activeAbilities[8];
        }
        else if (Input.GetKeyDown("0"))
        {
            clickedAbility = activeAbilities[9];
        }

        if (clickedAbility != null && clickedAbility.canUse == true)
        {
            Debug.Log("Detected Skill!");
            clickedAbility.Fire(gameObject);

            StartCoroutine(clickedAbility.StartCoolDown());
        }
        clickedAbility = null;

    }
}
