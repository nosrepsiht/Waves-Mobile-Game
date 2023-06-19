using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneObjects : MonoBehaviour
{
    public static SceneObjects Singleton;

    public GameObject mainCamera;

    public Image redforegroundImage;
    public GameObject backgroundUI;

    public GameObject lobbyUI;
    public GameObject shopUI;
    public GameObject lostGameUI;
    public GameObject wonGameUI;
    public GameObject gameMenuUI;
    
    public GameObject beforeWaveUI;
    public GameObject statsUI;
    public GameObject gameUI;
    public GameObject joysticksUI;

    public TextMeshProUGUI wonGame;

    public Slider hpSlider;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI ap;
    public TextMeshProUGUI dp;
    public TextMeshProUGUI coins;
    public TextMeshProUGUI completedWaves;
    public TextMeshProUGUI aliveMobs;

    public JoystickManager joystickMovement;
    public JoystickManager joystickRotation;

    void Awake()
    {
        Singleton = this;
    }
}
