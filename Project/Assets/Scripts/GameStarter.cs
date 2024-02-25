using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private SceneAsset _gameScene;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void StartGame(List<UserConnectionData> users)
    {
        UsersHolder.SetUsers(users);
        NetworkManager.Singleton.SceneManager.LoadScene(_gameScene.name, LoadSceneMode.Single);
    }
}
