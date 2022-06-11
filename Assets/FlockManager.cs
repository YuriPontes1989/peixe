using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FlockManager : MonoBehaviour
{
    // declarando as variaveis de valor minimo de velocidade  maximo, distancia e velocidade de rota��o
    public GameObject fishPrefab;
    public int numFish = 20;
    public GameObject[] allFish;
    public Vector3 swinLimits = new Vector3(5, 5, 5);
    public Vector3 goalPos;
    [Header("Configura��es do Cardume")]
    [Range(0.0f, 5.0f)]
    public float minSpeed;
    [Range(0.0f, 5.0f)]
    public float maxSpeed;
    [Range(1.0f, 10.0f)]
    public float neighbourDistance;
    [Range(0.0f, 5.0f)]
    public float rotationSpeed;
    void Start()
    {
        // � criado um array com todos os peixeis e atrav�s de um loof for, os peixeis s�o instanciados em uma posi��o aleat�ria dentro dos limites do nado, al�m de a posi��o do objetivo ser declarada como a posi��o  do gameobject
        allFish = new GameObject[numFish];
        for (int i = 0; i < numFish; i++)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-swinLimits.x,
            swinLimits.x),
            Random.Range(-swinLimits.y,
            swinLimits.y),
            Random.Range(-swinLimits.z,
            swinLimits.z));
            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity);
            allFish[i].GetComponent<Flock>().myManager = this;
        }
        goalPos = this.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        //  se o n�mero aleat�rio gerado pela fun��o random, for menor que 10, a posi��o do objetivo � alterada para a sua posi��o mais uma posi��o aleat�rio dentro dos limites do nado
        goalPos = this.transform.position;
        if (Random.Range(0, 100) < 10)
            goalPos = this.transform.position + new Vector3(Random.Range(-swinLimits.x,
            swinLimits.x),
            Random.Range(-swinLimits.y,
            swinLimits.y),
            Random.Range(-swinLimits.z,
            swinLimits.z));
    }
}