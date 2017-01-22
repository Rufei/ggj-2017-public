using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangesCharacterWithPlayerAxes : MonoBehaviour
{
    public static class CONST
    {
        public const float fudgeFactor = 0.03f;
        public const float traverseCooldown = 0.3f;
    }

    public int playerNum;

    private GameData gameData;
    private CharacterSelectBehavior charSelect;
    private float rightTraverseCooldown;
    private float leftTraverseCooldown;

    void Start()
    {
        gameData = GameObject.Find("/GameData").GetComponent<GameData>();
        charSelect = gameData.GetComponent<CharacterSelectBehavior>();
    }

    void Update()
    {
        if (CheckRightTraverse())
        {
            charSelect.SetPlayerCharacter(playerNum, 1);
        }
        else
        if (CheckLeftTraverse())
        {
            charSelect.SetPlayerCharacter(playerNum, -1);
        }
    }

    bool CheckRightTraverse()
    {
        if (Input.GetAxis(PlayerData.GetAxis(playerNum, "Horizontal")) < 1 - CONST.fudgeFactor)
        {
            rightTraverseCooldown = 0f;
            return false;
        }
        if (rightTraverseCooldown > 0f)
        {
            rightTraverseCooldown -= Time.deltaTime;
            return false;
        }
        rightTraverseCooldown += CONST.traverseCooldown;
        return true;
    }

    bool CheckLeftTraverse()
    {
        if (Input.GetAxis(PlayerData.GetAxis(playerNum, "Horizontal")) > -1 + CONST.fudgeFactor)
        {
            leftTraverseCooldown = 0f;
            return false;
        }
        if (leftTraverseCooldown > 0f)
        {
            leftTraverseCooldown -= Time.deltaTime;
            return false;
        }
        leftTraverseCooldown += CONST.traverseCooldown;
        return true;
    }
}
