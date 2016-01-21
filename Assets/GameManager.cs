using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public GameObject finish;

    public GameObject[] m_roadPieces;

    public GameObject m_playerPrefab;

    public int m_trackLengthInpieces;

    private Vector3 m_nextRoadPiecePoint;
    private Quaternion m_nextRoadPieceRotation;

    public Text centerText;
    public Text belowCenterText;

    int lastIndex;
    float maxZ;

    private void initArray()
    {
        m_roadPieces = new GameObject[7];

        m_roadPieces[0] = a;
        m_roadPieces[1] = b;
        m_roadPieces[2] = c;
        m_roadPieces[3] = d;
        m_roadPieces[4] = e;
        m_roadPieces[5] = f;
        m_roadPieces[6] = g;


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

            GameObject roadpieceToAdd;

            int index;
            do
            {
                index = UnityEngine.Random.Range(0, m_roadPieces.Length);
            } while (lastIndex == index);

            roadpieceToAdd = m_roadPieces[index];

            lastIndex = index;

            if (i == 0)
            {
                SaveExitPointTransform(m_roadpieceSpawn);
                roadpieceToAdd = m_roadPieces[0];
            }
            else if(i == m_trackLengthInpieces -1)
            {
                roadpieceToAdd = finish;
            }

            Instantiate(roadpieceToAdd, new Vector3(m_nextRoadPiecePoint.x, m_nextRoadPiecePoint.y, m_nextRoadPiecePoint.z), m_nextRoadPieceRotation);

            Vector3 entrypoint = roadpieceToAdd.transform.Find("entryPoint").position;
            roadpieceToAdd.transform.Translate(m_nextRoadPiecePoint - entrypoint); //move the new piece in respect to the difference of its center and its entry point

            SaveExitPointTransform(roadpieceToAdd);

            if (i == m_trackLengthInpieces -1)
            {
                maxZ = roadpieceToAdd.transform.Find("exitPoint").transform.position.z;
            }
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
        if (null == GameObject.Find("Player(Clone)"))
        {
            SceneManager.LoadScene(0);
        }


        if (GameObject.Find("Player(Clone)").transform.position.z > (maxZ - 10.0f))
        {

            PlayerScript ps = (PlayerScript)GameObject.Find("Player(Clone)").GetComponent("PlayerScript");
            if (GameObject.Find("Player(Clone)").transform.position.z > (maxZ - 8.0f))
            {
                belowCenterText.text = "Press space to restart!";

                ps.speed = 0;
                ps.rb.velocity = Vector3.zero;
                ps.rb.drag = 100.0f;
            }
            else
            {
                centerText.text = "FINISHED!";

                ps.cannotExplode = true;
                ps.rb.drag = 20.0f;
            }
        }


    }
}
