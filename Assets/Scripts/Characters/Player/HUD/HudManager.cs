//Hud Manager is used to controll all Elements of the player character HUD
using Assets.Characters.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using TMPro.Examples;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Characters.Player
{
    public class StatusBar
    {
        private GameObject parent;  
        private Image foreGround, backGround;
        private TextMeshProUGUI barText;

        //Set the fill of the bar
        public void SetFill(float fillPercent)
        {
            foreGround.fillAmount = fillPercent;
            fillPercent = Mathf.Clamp01(fillPercent);

            foreGround.fillAmount = fillPercent;
        }
        //Set the text of the bar
        public void SetText(float current, float max)
        {
            barText.text = $"{current} / {max}";
        }
        public void SetText(string text)
        {
            barText.text = text;
        }
        //Constructor
        public StatusBar(GameObject barPrefab)
        {
            parent = barPrefab;
            foreGround = barPrefab.transform.Find("ForeGround").GetComponent<Image>();
            backGround = barPrefab.transform.Find("BackGround").GetComponent<Image>();
            barText = barPrefab.transform.Find("BarText").GetComponent<TextMeshProUGUI>();

            if (foreGround == null)
                Debug.Log("ForeGround null" );
            if (backGround == null)
                Debug.Log("ForeGround null");
            if (barText == null)
                Debug.Log("Text null");        
        }
        //Check if the bar is valid
        public bool validBar()
        {
            return foreGround != null && backGround != null && barText != null;
        }
    }
    public class HudManager : MonoBehaviour
    {

        [SerializeField] private GameObject healthBarObject, staminaBarPrefab, ManBarPrefab;

        private StatusBar healthBar;
        private StatusBar staminaBar;
        private StatusBar magicBar;

        private PlayerCharacter player;
        private void Awake()
        {
            player = GetComponent<PlayerCharacter>();
        }
        //Gets the objects tied tied to the hud, and initialze them here
        private void Start()
        {
            healthBar = new StatusBar(healthBarObject);
            staminaBar = new StatusBar(staminaBarPrefab);
            magicBar = new StatusBar(ManBarPrefab);
        }

        //Update the values of the bars
        public void FixedUpdate()
        {
            if (healthBar.validBar())
            {
                healthBar.SetFill(player.healthPercent);
                healthBar.SetText(player.currentHealth, player.maxHealth);
            }
            if (staminaBar.validBar())
            {
                staminaBar.SetFill(player.staminaPercent);
                staminaBar.SetText(player.currentStamina, player.maxStamina);
            }
            if (magicBar.validBar())
            {
                magicBar.SetFill(player.manaPercent);
                magicBar.SetText(player.currentMana, player.maxMana);
            }
        }
    }
}
