using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton;

    public GameObject player;
    public List<GameObject> aliveMobsList = new List<GameObject>();

    public int completedWaves;

    //int[] viewModes = { 10, 25, 50, 75, 100 };
    //int viewModeIndex = 0;

    bool mute;

    void Awake()
    {
        Singleton = this;

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        Menu("lobby");
        AudioManager.Singleton.PlayLoop("Nature");
    }

    void Menu(string _menu)
    {
        HideMenu();

        SceneObjects.Singleton.backgroundUI.SetActive(true);

        if (_menu == "lobby")
        {
            SceneObjects.Singleton.lobbyUI.SetActive(true);
        }

        else if (_menu == "shop")
        {
            SceneObjects.Singleton.shopUI.SetActive(true);
        }

        else if (_menu == "lost")
        {
            SceneObjects.Singleton.lostGameUI.SetActive(true);
        }

        else if (_menu == "won")
        {
            SceneObjects.Singleton.wonGameUI.SetActive(true);
        }

        else if (_menu == "menu")
        {
            SceneObjects.Singleton.gameMenuUI.SetActive(true);
        }
    }

    void HideMenu()
    {
        SceneObjects.Singleton.backgroundUI.SetActive(false);
        SceneObjects.Singleton.lobbyUI.SetActive(false);
        SceneObjects.Singleton.shopUI.SetActive(false);
        SceneObjects.Singleton.lostGameUI.SetActive(false);
        SceneObjects.Singleton.wonGameUI.SetActive(false);
        SceneObjects.Singleton.gameMenuUI.SetActive(false);

    }

    void CreatePlayer()
    {
        AudioManager.Singleton.ResumeLoop("Nature");
        AudioManager.Singleton.PauseLoop("Wave");
        HideMenu();
        player = Instantiate(Prefabs.Singleton.player, new Vector3(0f, 1f, 0f), Quaternion.identity);

        SceneObjects.Singleton.mainCamera.transform.parent = player.transform;
        SceneObjects.Singleton.mainCamera.transform.position = new Vector3(0f, SceneObjects.Singleton.mainCamera.transform.position.y, 0f);

        SceneObjects.Singleton.beforeWaveUI.SetActive(true);

        completedWaves = 0;
    }

    public void PlayerIsDead()
    {
        foreach (GameObject _mobGO in aliveMobsList)
        {
            Destroy(_mobGO);
        }

        aliveMobsList.Clear();

        Menu("lost");
        AudioManager.Singleton.ResumeLoop("Nature");
        AudioManager.Singleton.PauseLoop("Wave");
    }

    public void MobIsKilled()
    {
        player.GetComponent<PlayerStats>().coins += 2;
        if (aliveMobsList.Count <= 0)
        {
            WaveIsCompleted();
        }
    }

    public void WaveIsCompleted()
    {
        AudioManager.Singleton.ResumeLoop("Nature");
        AudioManager.Singleton.PauseLoop("Wave");
        completedWaves++;

        if (completedWaves == 3)
        {
            AudioManager.Singleton.Play("Win");
            Menu("won");
            SceneObjects.Singleton.wonGame.text = "YOU WON!";
        }

        else
        {
            player.GetComponent<PlayerStats>().hp = player.GetComponent<PlayerStats>().hpMax;
            SceneObjects.Singleton.beforeWaveUI.SetActive(true);
        }
    }

    public void StartWave()
    {
        if (completedWaves == 0)
        {
            SpawnMobs(15, "Goblin");
        }

        else if (completedWaves == 1)
        {
            SpawnMobs(20, "Neon");
        }

        else if (completedWaves == 2)
        {
            SpawnMobs(1, "Mark");
        }
    }

    public void SpawnMobs(int _count, string _class)
    {
        for (int i = 1; i <= _count; i++)
        {
            GameObject _mob = Instantiate(Prefabs.Singleton.mob, GetPos(), Quaternion.identity);
            _mob.GetComponent<MobStats>().mobName = _class;
            _mob.name = _class;
            aliveMobsList.Add(_mob);

            if (_class == "Goblin")
            {
                _mob.GetComponent<MobStats>().hpMax = 10f;
                _mob.GetComponent<MobStats>().hp = 10f;
                _mob.GetComponent<MobStats>().ap = 1f;
                _mob.GetComponent<MobStats>().dp = 0f;

                _mob.GetComponentInChildren<Renderer>().material = Materials.Singleton.green;
            }

            else if (_class == "Neon")
            {
                _mob.GetComponent<MobStats>().hpMax = 40f;
                _mob.GetComponent<MobStats>().hp = 40f;
                _mob.GetComponent<MobStats>().ap = 20f;
                _mob.GetComponent<MobStats>().dp = 1f;

                _mob.GetComponentInChildren<Renderer>().material = Materials.Singleton.purple;
            }

            else if (_class == "Mark")
            {
                _mob.GetComponent<MobStats>().hpMax = 200f;
                _mob.GetComponent<MobStats>().hp = 200f;
                _mob.GetComponent<MobStats>().ap = 50f;
                _mob.GetComponent<MobStats>().dp = 10f;

                _mob.GetComponentInChildren<Renderer>().material = Materials.Singleton.orange;
            }
        }
    }

    Vector3 GetPos()
    {
        float newRange = 10f;

        Vector3 newPos = Vector3.zero;
        newPos.y = 1f;

        if (Random.value > 0.5f)
        {
            newPos.x = Random.Range(-25f, 25f);

            if (Random.value > 0.5f)
            {
                newPos.z = 25f;
            }

            else
            {
                newPos.z = -25f;
            }
        }

        else
        {
            newPos.z = Random.Range(-25f, 25f);

            if (Random.value > 0.5f)
            {
                newPos.x = 25f;
            }

            else
            {
                newPos.x = -25f;
            }
        }

        newPos = new Vector3(Random.Range(newPos.x - newRange, newPos.x + newRange), newPos.y, Random.Range(newPos.z - newRange, newPos.z + newRange));

        return newPos;
    }

    #region Buttons

    public void StartBF()
    {
        CreatePlayer();

        SceneObjects.Singleton.statsUI.SetActive(true);
        SceneObjects.Singleton.gameUI.SetActive(true);
        SceneObjects.Singleton.joysticksUI.SetActive(true);
    }

    public void QuitBF()
    {
        Application.Quit();
    }

    public void StartWaveBF()
    {
        AudioManager.Singleton.PauseLoop("Nature");
        AudioManager.Singleton.Play("StartWave");
        AudioManager.Singleton.PlayLoop("Wave");

        StartWave();
        HideMenu();
        SceneObjects.Singleton.beforeWaveUI.SetActive(false);
    }

    public void ShopBF()
    {
        Menu("shop");
    }

    public void RespawnBF()
    {
        CreatePlayer();
    }

    public void RestartBF()
    {
        foreach (GameObject _mobGO in aliveMobsList)
        {
            Destroy(_mobGO);
        }

        aliveMobsList.Clear();

        SceneObjects.Singleton.mainCamera.transform.parent = null;
        Destroy(player);

        CreatePlayer();
    }

    public void CloseBF()
    {
        HideMenu();
    }

    public void MenuBF()
    {
        Menu("menu");
    }

    /*public void ChangeViewBF()
    {
        viewModeIndex++;

        if (viewModes.Length <= viewModeIndex)
        {
            viewModeIndex = 0;
        }

        SceneObjects.Singleton.mainCamera.transform.localPosition = new Vector3(0f, viewModes[viewModeIndex], 0f);
    }*/

    public void BuyAPBF()
    {
        if (player.GetComponent<PlayerStats>().coins > 0)
        {
            AudioManager.Singleton.Play("Equip");
            player.GetComponent<PlayerStats>().coins--;
            player.GetComponent<PlayerStats>().ap += 1f;
        }
    }

    public void BuyDPBF()
    {
        if (player.GetComponent<PlayerStats>().coins > 0)
        {
            AudioManager.Singleton.Play("Equip");
            player.GetComponent<PlayerStats>().coins--;
            player.GetComponent<PlayerStats>().dp += 1f;
        }
    }

    public void ToggleSoundBF()
    {
        mute = !mute;
        
        if (mute)
        {
            AudioListener.volume = 0;
        }

        else
        {
            AudioListener.volume = 1;
        }
    }

    #endregion
}