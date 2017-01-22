using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatesPlayerSelects : MonoBehaviour
{
    public GameObject playerSelectElementPrefab;
    private GameData gameData;
    private Canvas canvas;
    private CanvasScaler scaler;

    public List<GameObject> playerSelectList;

    void Start()
    {
        gameData = GameObject.Find("/GameData").GetComponent<GameData>();
        canvas = GetComponent<Canvas>();
        scaler = GetComponent<CanvasScaler>();
        playerSelectList = new List<GameObject>();
    }

    void Update()
    {
        if (gameData.playerDataList.Count >= 0 && gameData.playerDataList.Count != playerSelectList.Count)
        {
            // Shrink
            for (int i = playerSelectList.Count; i >= gameData.playerDataList.Count; i--)
            {
                Destroy(playerSelectList[i]);
                playerSelectList.RemoveAt(i);
            }

            // Add
            Vector2 refRes = scaler.referenceResolution;
            for (int i = playerSelectList.Count; i < gameData.playerDataList.Count; i++)
            {
                GameObject playerSelectElem = Instantiate(playerSelectElementPrefab, canvas.transform);
                playerSelectElem.transform.localPosition = playerSelectElementPrefab.transform.position;
                playerSelectElem.transform.localScale = playerSelectElementPrefab.transform.localScale;
                // Relocate
                Vector3 oldPos = playerSelectElem.transform.localPosition;
                playerSelectElem.transform.localPosition = new Vector3(refRes.x * (1f + i) / (1f + gameData.playerDataList.Count) - refRes.x / 2f, oldPos.y, oldPos.z);
                // Setup
                ReflectsPlayerSelection rps = playerSelectElem.GetComponent<ReflectsPlayerSelection>();
                rps.SetPlayerNumber(i);
                playerSelectList.Add(playerSelectElem);
            }
        }
    }
}
