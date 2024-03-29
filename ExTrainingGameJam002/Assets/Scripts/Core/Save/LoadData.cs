using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Player;
using TimerSystem;
using StageSystem;
using SoundSystem;

public class LoadData : MonoBehaviour
{
    public GameObject player;
    public AreaClear areaClear;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public TimerController timer;
    public CameraMove cm;
    private BGMControl bgmcontrol;

    public void Start()
    {
        bgmcontrol = FindObjectOfType<BGMControl>();
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/savedata.dat"))
        {
            // セーブデータファイルが存在する場合は読み込む
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedata.dat", FileMode.Open);
            SaveData saveData = (SaveData)bf.Deserialize(file);
            file.Close();

            // セーブデータの情報を各オブジェクトに反映させる
            Vector2 playerPosition = new Vector2(saveData.playerPositionX, saveData.playerPositionY);
            player.transform.position = playerPosition;
            areaClear.HaveKey = saveData.HaveKey;
            Vector2 enemy1Position = new Vector2(saveData.enemy1PositionX, saveData.enemy1PositionY);
            Vector2 enemy2Position = new Vector2(saveData.enemy2PositionX, saveData.enemy2PositionY);
            Vector2 enemy3Position = new Vector2(saveData.enemy3PositionX, saveData.enemy3PositionY);
            enemy1.transform.position = enemy1Position;
            enemy2.transform.position = enemy2Position;
            enemy3.transform.position = enemy3Position;

            timer.currentTime = saveData.Timer;

            cm.currentPointIndex = saveData.AreaPointIndexNum;

            bgmcontrol.AreaStart();
            // 他の情報もここで反映させる
        }
    }
}