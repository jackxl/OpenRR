using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{

    public GameObject m_roadpieceSpawn;
    public GameObject a;
    public GameObject b;
    public GameObject c;
    public GameObject d;
    public GameObject e;
    public GameObject f;
    public GameObject g;

    public GameObject[] m_roadPieces;

    public GameObject m_playerPrefab;

    public int m_trackLengthInpieces;

    private Vector3 m_nextRoadPiecePoint;
    private Quaternion m_nextRoadPieceRotation;

    private void initArray()
    {
        m_roadPieces = new GameObject[6];

        m_roadPieces[0] = a;
        m_roadPieces[1] = b;
        m_roadPieces[2] = c;
        m_roadPieces[3] = d;
        m_roadPieces[4] = e;
        m_roadPieces[5] = f;
        //m_roadPieces[6] = g;


    }

    // Use this for initialization
    void Start()
    {
        initArray();

        Vector3 playerSpawnPosition = m_roadpieceSpawn.transform.position;
        playerSpawnPosition += new Vector3(0, 5, 0);
        Instantiate(m_playerPrefab, playerSpawnPosition , m_roadpieceSpawn.transform.rotation);

        for (int i = 0; i < m_trackLengthInpieces; i++)
        {
            if(i == 0)
            {
                SaveExitPointTransform(m_roadpieceSpawn);
            }

            GameObject roadpieceToAdd;

            //var numberGenerator = new System.Random(System.DateTime.Now.Minute + i * 3);
            //roadpieceToAdd = m_roadPieces[numberGenerator.Next(0, m_roadPieces.Length)];

            roadpieceToAdd = m_roadPieces[UnityEngine.Random.Range(0,m_roadPieces.Length)];

            Instantiate(roadpieceToAdd, new Vector3(m_nextRoadPiecePoint.x, m_nextRoadPiecePoint.y, m_nextRoadPiecePoint.z), m_nextRoadPieceRotation);

            Vector3 entrypoint = roadpieceToAdd.transform.Find("entryPoint").position;
            roadpieceToAdd.transform.Translate(m_nextRoadPiecePoint - entrypoint); //move the new piece in respect to the difference of its center and its entry point

            SaveExitPointTransform(roadpieceToAdd);
        }

    }

    private void SaveExitPointTransform(GameObject roadpieceToAdd)
    {
        m_nextRoadPiecePoint = roadpieceToAdd.transform.Find("exitPoint").gameObject.transform.position;
        m_nextRoadPieceRotation = roadpieceToAdd.transform.Find("exitPoint").gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
