using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Player;
using TimerSystem;
using StageSystem;

public class LoadData : MonoBehaviour
{
    public GameObject player;
    public AreaClear areaClear;
    public GameObject enemy1;
    public GameObject enemy2;
    public TimerController timer;
    public CameraMove cm;

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/savedata.dat"))
        {
            // �Z�[�u�f�[�^�t�@�C�������݂���ꍇ�͓ǂݍ���
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedata.dat", FileMode.Open);
            SaveData saveData = (SaveData)bf.Deserialize(file);
            file.Close();

            // �Z�[�u�f�[�^�̏����e�I�u�W�F�N�g�ɔ��f������
            Vector2 playerPosition = new Vector2(saveData.playerPositionX, saveData.playerPositionY);
            player.transform.position = playerPosition;
            areaClear.HaveKey = saveData.HaveKey;
            Vector2 enemy1Position = new Vector2(saveData.enemy1PositionX, saveData.enemy1PositionY);
            Vector2 enemy2Position = new Vector2(saveData.enemy2PositionX, saveData.enemy2PositionY);
            enemy1.transform.position = enemy1Position;
            enemy2.transform.position = enemy2Position;

            timer.currentTime = saveData.Timer;

            cm.currentPointIndex = saveData.AreaPointIndexNum;
            // ���̏��������Ŕ��f������
        }
    }
}