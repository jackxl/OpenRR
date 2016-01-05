using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{

    public GameObject m_roadpieceSpawn;
    public GameObject m_roadPieceTemplate;
    public GameObject m_playerPrefab;

    public int m_trackLengthInpieces;

    private Vector3 m_nextRoadPiecePoint;
    private Quaternion m_nextRoadPieceRotation;

    // Use this for initialization
    void Start()
    {

        Instantiate(m_playerPrefab, m_roadpieceSpawn.transform.position, m_roadpieceSpawn.transform.rotation);

        for (int i = 0; i < m_trackLengthInpieces; i++)
        {
            if(i == 0)
            {
                SaveExitPointTransform(m_roadpieceSpawn);
            }
            GameObject roadpieceToAdd = m_roadPieceTemplate;
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
