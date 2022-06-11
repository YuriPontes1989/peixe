using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Flock : MonoBehaviour
{
    // definindo as variaveis
    public FlockManager myManager;
    public float speed;
    bool turning = false;
    // Start is called before the first frame update
    void Start()
    {
        //velocidade minima e maxima do mymanger onde tem os peixes aleatorizada 
        speed = Random.Range(myManager.minSpeed,
        myManager.maxSpeed);
    }
    void Update()
    {
        // cria um bound "limite" no mymanager
        Bounds b = new Bounds(myManager.transform.position, myManager.swinLimits * 2);
        // Cria o raycast
        RaycastHit hit = new RaycastHit();
        // instancia  o vector3
        Vector3 direction = myManager.transform.position - transform.position;
        //detecta a colisão se o peixe entrar dentro do limite
        if (!b.Contains(transform.position))
        {
            turning = true;
            direction = myManager.transform.position - transform.position;
        }
        //gera um Raycast na frente doS peixeS 
        else if (Physics.Raycast(transform.position, this.transform.forward * 50, out hit))
        {
            // a direção é  refletida  quando o o peixei detecta  a parede
            turning = true;
            direction = Vector3.Reflect(this.transform.forward, hit.normal);
        }
        // se não detectar nada ele desvia 
        else
            turning = false;
        //se estiver ativa ele rotaciona até uma das posições apresentadas nos ifs anteriores
        if (turning)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(direction),
            myManager.rotationSpeed * Time.deltaTime);
        }
        else
        {
            // velocidade aleatória 
            if (Random.Range(0, 100) < 10)
                speed = Random.Range(myManager.minSpeed,
                myManager.maxSpeed);
            if (Random.Range(0, 100) < 20)
                ApplyRules();
        }
        transform.Translate(0, 0, Time.deltaTime * speed);
    }
    void ApplyRules()
    {
        GameObject[] gos;
        gos = myManager.allFish;
        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.01f;
        float nDistance;
        int groupSize = 0;
        foreach (GameObject go in gos)
        {
            if (go != this.gameObject)
            {
                nDistance = Vector3.Distance(go.transform.position, this.transform.position);
                if (nDistance <= myManager.neighbourDistance)
                {
                    vcentre += go.transform.position;
                    groupSize++;
                    if (nDistance < 1.0f)
                    {
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }
                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }
        if (groupSize > 0)
        {
            vcentre = vcentre / groupSize + (myManager.goalPos - this.transform.position);
            speed = gSpeed / groupSize;
            Vector3 direction = (vcentre + vavoid) - transform.position;
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(direction),
                myManager.rotationSpeed * Time.deltaTime);
        }
    }
}
        
