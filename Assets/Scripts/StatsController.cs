using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatsController : MonoBehaviour {
    bool alreadyClearedData;

    //determine whether score is counted as auto or teleop
    public bool isSandstorm; 
    int rocketPanel1; //treated as just "panel" in sandstorm
    int rocketCargo1; //treated as just "cargo" in sandstorm 
    int rocketPanel2; 
    int rocketCargo2;
    int rocketPanel3;
    int rocketCargo3;
    int shipPanel; 
    int shipCargo;

    //leave blank if not related to scene process; team number & match number in automatic page

    public Toggle sandstormAutonomousToggle; 
    public Toggle climb3;
    public Toggle climb2;
    public Toggle climb1;
    public Toggle bonus2;
    public Toggle bonus1;
    public InputField teamNumber;
    public InputField matchNumber;
    public InputField playerName;
    //account name is secret 

    //displayers
    public Text rocketPanel1Text;
    public Text rocketCargo1Text;
    public Text rocketPanel2Text;
    public Text rocketCargo2Text;
    public Text rocketPanel3Text;
    public Text rocketCargo3Text;
    public Text shipPanelText;
    public Text shipCargoText;

    public void addRocketPanel1() {
        rocketPanel1++;
    }
    public void addRocketCargo1() {
        rocketCargo1++;
    }
    public void addRocketPanel2() {
        rocketPanel2++;
    }
    public void addRocketCargo2() {
        rocketCargo2++;
    }
    public void addRocketPanel3() {
        rocketPanel3++;
    }
    public void addRocketCargo3() {
        rocketCargo3++;
    }
    public void addShipPanel() {
        shipPanel++;
    }
    public void addShipCargo() {
        shipCargo++;
    }
    public void subtractRocketPanel1() {
        if (rocketPanel1 > 0)
            rocketPanel1--;
    }
    public void subtractRocketCargo1() {
        if (rocketCargo1 > 0)
            rocketCargo1--;
    }
    public void subtractRocketPanel2() {
        if (rocketPanel2 > 0)
            rocketPanel2--;
    }
    public void subtractRocketCargo2() {
        if (rocketCargo2 > 0)
            rocketCargo2--;
    }
    public void subtractRocketPanel3() {
        if (rocketPanel3 > 0)
            rocketPanel3--;
    }
    public void subtractRocketCargo3() {
        if (rocketCargo3 > 0)
            rocketCargo3--;
    }
    public void subtractShipPanel() {
        if (shipPanel > 0)
            shipPanel--;
    }
    public void subtractShipCargo() {
        if (shipCargo > 0)
            shipCargo--;
    }

    public void saveData () {
        for (int i = 1; i <= 200; i++) {
            if (PlayerPrefs.GetInt ("team" + i + "number") == 0) {
                PlayerPrefs.SetInt ("team" + i + "number", PlayerPrefs.GetInt ("teamNumber"));
                PlayerPrefs.SetInt ("team" + i + "matchNumber", PlayerPrefs.GetInt ("matchNumber"));
                PlayerPrefs.SetString ("team" + i + "sandstormAutonomous", PlayerPrefs.GetString ("sandstormAutonomous"));
                PlayerPrefs.SetInt ("team" + i + "sandstormPanel", PlayerPrefs.GetInt ("sandstormPanel"));
                PlayerPrefs.SetInt ("team" + i + "sandstormCargo", PlayerPrefs.GetInt ("sandstormCargo"));
                PlayerPrefs.SetString ("team" + i + "sandstormBonus2", PlayerPrefs.GetString ("sandstormBonus2"));
                PlayerPrefs.SetString("team" + i + "sandstormBonus1", PlayerPrefs.GetString("sandstormBonus1"));
                PlayerPrefs.SetInt ("team" + i + "teleOpRocketPanel1", PlayerPrefs.GetInt ("teleOpRocketPanel1"));
                PlayerPrefs.SetInt ("team" + i + "teleOpRocketPanel2", PlayerPrefs.GetInt ("teleOpRocketPanel2"));
                PlayerPrefs.SetInt("team" + i + "teleOpRocketPanel3", PlayerPrefs.GetInt("teleOpRocketPanel3"));
                PlayerPrefs.SetInt ("team" + i + "teleOpRocketCargo1", PlayerPrefs.GetInt ("teleOpRocketCargo1"));
                PlayerPrefs.SetInt ("team" + i + "teleOpRocketCargo2", PlayerPrefs.GetInt ("teleOpRocketCargo2"));
                PlayerPrefs.SetInt("team" + i + "teleOpRocketCargo3", PlayerPrefs.GetInt("teleOpRocketCargo3"));
                PlayerPrefs.SetInt ("team" + i + "teleOpShipPanel", PlayerPrefs.GetInt ("teleOpShipPanel"));
                PlayerPrefs.SetInt ("team" + i + "teleOpShipCargo", PlayerPrefs.GetInt ("teleOpShipCargo"));
                PlayerPrefs.SetString ("team" + i + "climb3", PlayerPrefs.GetString ("climb3"));
                PlayerPrefs.SetString ("team" + i + "climb2", PlayerPrefs.GetString ("climb2"));
                PlayerPrefs.SetString ("team" + i + "climb1", PlayerPrefs.GetString ("climb1"));
                PlayerPrefs.SetString("team" + i + "name", PlayerPrefs.GetString("name"));
                break; 
            }
        }
        //clear current data
        PlayerPrefs.SetInt ("teamNumber", 0);
        PlayerPrefs.SetInt ("matchNumber", 0);
        PlayerPrefs.SetString ("sandstormAutonomous", "false");
        PlayerPrefs.SetInt ("sandstormPanel", 0);
        PlayerPrefs.SetInt ("sandstormCargo", 0);
        PlayerPrefs.SetString("sandstormBonus2", "false");
        PlayerPrefs.SetString("sandstormBonus1", "false");
        PlayerPrefs.SetInt ("teleOpRocketPanel1", 0);
        PlayerPrefs.SetInt ("teleOpRocketPanel2", 0);
        PlayerPrefs.SetInt("teleOpRocketPanel3", 0);
        PlayerPrefs.SetInt ("teleOpRocketCargo1", 0);
        PlayerPrefs.SetInt ("teleOpRocketCargo2", 0);
        PlayerPrefs.SetInt("teleOpRocketCargo3", 0);
        PlayerPrefs.SetInt ("teleOpShipPanel", 0);
        PlayerPrefs.SetInt ("teleOpShipCargo", 0);
        PlayerPrefs.SetString ("climb3", "false");
        PlayerPrefs.SetString ("climb2", "false");
        PlayerPrefs.SetString("climb1", "false");
        PlayerPrefs.SetString("name", "");

        alreadyClearedData = true; 
        SceneManager.LoadSceneAsync (0);
    }
    void Start () {
        if (isSandstorm) {
            rocketPanel1 = PlayerPrefs.GetInt ("sandstormPanel");
            rocketCargo1 = PlayerPrefs.GetInt ("sandstormCargo");
            teamNumber.text = PlayerPrefs.GetInt ("teamNumber").ToString ();
            matchNumber.text = PlayerPrefs.GetInt ("matchNumber").ToString ();
            playerName.text = PlayerPrefs.GetString("name"); 

            bool placeHolder;
            if (bool.TryParse (PlayerPrefs.GetString ("sandstormAutonomous"), out placeHolder))
                sandstormAutonomousToggle.isOn = bool.Parse (PlayerPrefs.GetString ("sandstormAutonomous"));
            if (bool.TryParse(PlayerPrefs.GetString("sandstormBonus2"), out placeHolder))
                bonus2.isOn = bool.Parse(PlayerPrefs.GetString("sandstormBonus2"));
            if (bool.TryParse(PlayerPrefs.GetString("sandstormBonus1"), out placeHolder))
                bonus1.isOn = bool.Parse(PlayerPrefs.GetString("sandstormBonus1"));
        } else {
            rocketPanel1 = PlayerPrefs.GetInt ("teleOpRocketPanel1");
            rocketCargo1 = PlayerPrefs.GetInt ("teleOpRocketCargo1");
            rocketPanel2 = PlayerPrefs.GetInt ("teleOpRocketPanel2");
            rocketCargo2 = PlayerPrefs.GetInt ("teleOpRocketCargo2");
            rocketPanel3 = PlayerPrefs.GetInt ("teleOpRocketPanel3");
            rocketCargo3 = PlayerPrefs.GetInt ("teleOpRocketCargo3");
            shipPanel = PlayerPrefs.GetInt ("teleOpShipPanel");
            shipCargo = PlayerPrefs.GetInt ("teleOpShipCargo");
            bool placeHolder;
            if (bool.TryParse (PlayerPrefs.GetString ("climb3"), out placeHolder))
                climb3.isOn = bool.Parse (PlayerPrefs.GetString ("climb3"));
            if (bool.TryParse (PlayerPrefs.GetString ("climb2"), out placeHolder))
                climb2.isOn = bool.Parse (PlayerPrefs.GetString ("climb2"));
            if (bool.TryParse(PlayerPrefs.GetString("climb1"), out placeHolder)) 
                climb1.isOn = bool.Parse(PlayerPrefs.GetString("climb1")); 
        }
    }
    void Update () {
        if (!alreadyClearedData) {
            if (isSandstorm) {
                if (bonus1.isOn)
                    bonus2.isOn = false;
                else if (bonus2.isOn)
                    bonus1.isOn = false;
                rocketPanel1Text.text = rocketPanel1.ToString();
                rocketCargo1Text.text = rocketCargo1.ToString();
                if (teamNumber.text != "" && teamNumber.text != "-")
                    PlayerPrefs.SetInt("teamNumber", int.Parse(teamNumber.text));
                if (matchNumber.text != "" && matchNumber.text != "-")
                    PlayerPrefs.SetInt("matchNumber", int.Parse(matchNumber.text));

                PlayerPrefs.SetString("name", playerName.text);
                PlayerPrefs.SetString("sandstormAutonomous", sandstormAutonomousToggle.isOn.ToString());
                PlayerPrefs.SetString("sandstormBonus2", bonus2.isOn.ToString());
                PlayerPrefs.SetString("sandstormBonus1", bonus1.isOn.ToString());
                PlayerPrefs.SetInt("sandstormPanel", rocketPanel1);
                PlayerPrefs.SetInt("sandstormCargo", rocketCargo1);
            } else {
                if (climb1.isOn) {
                    climb2.isOn = false;
                    climb3.isOn = false;
                } else if (climb2.isOn) {
                    climb1.isOn = false;
                    climb3.isOn = false;
                } else if (climb3.isOn) {
                    climb1.isOn = false;
                    climb2.isOn = false;
                }
                rocketPanel1Text.text = rocketPanel1.ToString ();
                rocketPanel2Text.text = rocketPanel2.ToString ();
                rocketPanel3Text.text = rocketPanel3.ToString();
                rocketCargo1Text.text = rocketCargo1.ToString ();
                rocketCargo2Text.text = rocketCargo2.ToString ();
                rocketCargo3Text.text = rocketCargo3.ToString();
                shipPanelText.text = shipPanel.ToString();
                shipCargoText.text = shipCargo.ToString();
                PlayerPrefs.SetInt ("teleOpRocketPanel1", rocketPanel1);
                PlayerPrefs.SetInt ("teleOpRocketCargo1", rocketCargo1);
                PlayerPrefs.SetInt ("teleOpRocketPanel2", rocketPanel2);
                PlayerPrefs.SetInt ("teleOpRocketCargo2", rocketCargo2);
                PlayerPrefs.SetInt("teleOpRocketPanel3", rocketPanel3);
                PlayerPrefs.SetInt("teleOpRocketCargo3", rocketCargo3);
                PlayerPrefs.SetInt("teleOpShipPanel", shipPanel);
                PlayerPrefs.SetInt("teleOpShipCargo", shipCargo);
                PlayerPrefs.SetString ("climb3", climb3.isOn.ToString ());
                PlayerPrefs.SetString("climb2", climb2.isOn.ToString());
                PlayerPrefs.SetString ("climb1", climb1.isOn.ToString ());
            }
        }
    }
}
