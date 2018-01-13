﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    private int[][] level = new int[][]
    {
        new int[]{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        new int[]{1, 3, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3, 3, 0, 0, 0, 4, 0, 1},
        new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1},
        new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1},
        new int[]{1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        new int[]{1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        new int[]{1, 0, 0, 0, 0, 0, 3, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        new int[]{1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        new int[]{1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1},
        new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 3, 1},
        new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
        new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1},
        new int[]{1, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1},
        new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1},
        new int[]{1, 0, 0, 0, 3, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1},
        new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1},
        new int[]{1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1},
        new int[]{1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1},
        new int[]{1, 0, 2, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1},
        new int[]{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
    };

    [Header("오브젝트 레퍼런스")]
    public Transform wall;
    public Transform player;
    public Transform orb;

    public static GameController _instance;
    private int orbsCollected;
    private int orbsTotal;

    public Text scoreText;

    public Transform goal;
    private ParticleSystem goalPS;

    private void Awake()
    {
        _instance = this;
    }

    // Use this for initialization
    void Start () {
        BuildLevel();

        GameObject[] orbs;
        orbs = GameObject.FindGameObjectsWithTag("Orb");

        orbsCollected = 0;
        orbsTotal = orbs.Length;

        scoreText.text = "Orbs: " + orbsCollected + "/" + orbsTotal;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CollectedOrb()
    {
        orbsCollected++;
        scoreText.text = "Orbs: " + orbsCollected + "/" + orbsTotal;

        if(orbsCollected >= orbsTotal)
        {
            goalPS.Play();
        }
    }

    void BuildLevel()
    {
        GameObject dynamicParent = GameObject.Find("DynamicObjects");
        for (int yPos = 0; yPos < level.Length; yPos++)
        {
            for (int xPos = 0; xPos < (level[yPos]).Length; xPos++)
            {
                Transform toCreate = null;
                switch(level[yPos][xPos])
                {
                    case 0:
                        break;

                    case 1:
                        toCreate = wall;
                        break;

                    case 2:
                        toCreate = player;
                        break;

                    case 3:
                        toCreate = orb;
                        break;

                    case 4:
                        toCreate = goal;
                        break;

                    default:
                        print("유효하지 않은 값: " + (level[yPos][xPos]).ToString());
                        break;
                }

                if(toCreate != null)
                {
                    Transform newObject = Instantiate(toCreate, new Vector3(xPos, (level.Length - yPos), 0), toCreate.rotation) as Transform;

                    if(toCreate == goal)
                    {
                        goalPS = newObject.gameObject.GetComponent<ParticleSystem>();
                    }

                    newObject.parent = dynamicParent.transform;
                }

                //if(level[yPos][xPos] == 1)
                //{
                //    Transform newObject = Instantiate(wall, new Vector3(xPos, (level.Length - yPos), 0), wall.rotation) as Transform;
                //    newObject.parent = dynamicParent.transform;
                //}
            }
        }
    }
}
