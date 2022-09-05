using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PinkController1 : MonoBehaviour
{
    public GameObject target;
    NavMeshAgent agent;
    [SerializeField, Range(0F, 90F), Tooltip("射出する角度")]
    private float angle;
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.destination = target.transform.position;
        StartCoroutine(MoveNormalSpeed(agent));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator MoveNormalSpeed(NavMeshAgent agent)
    {
        agent.autoTraverseOffMeshLink = false; // OffMeshLinkによる移動を禁止
        var agentRigidbody = agent.GetComponent<Rigidbody>();

        while (true)
        {
            // OffmeshLinkに乗るまで普通に移動
            yield return new WaitWhile(() => agent.isOnOffMeshLink == false);

            // OffMeshLinkに乗ったので、NavmeshAgentによる移動を止めて、
            // OffMeshLinkの終わりまでNavmeshAgent.speedと同じ速度で移動
            
            agent.Stop();
            agentRigidbody.isKinematic = false;

            Vector3 pointA = transform.position;
            Vector3 pointB = new Vector3 (agent.currentOffMeshLinkData.endPos.x, agent.currentOffMeshLinkData.endPos.y+1.5f, agent.currentOffMeshLinkData.endPos.z);

            float rad = angle * Mathf.PI / 180;
            float x = Vector2.Distance(new Vector2(pointA.x, pointA.z), new Vector2(pointB.x, pointB.z));
            float y = pointA.y - pointB.y;
            float speed = Mathf.Sqrt(-Physics.gravity.y * Mathf.Pow(x, 2) / (2 * Mathf.Pow(Mathf.Cos(rad), 2) * (x * Mathf.Tan(rad) + y)));

            Vector3 velocity = new Vector3(pointB.x - pointA.x, x * Mathf.Tan(rad), pointB.z - pointA.z).normalized * speed;

            agentRigidbody.AddForce(velocity * agentRigidbody.mass, ForceMode.Impulse);

/*
            Vector3 forceDirection = new Vector3(-1.0f, 3.0f, 0f);

            // 上の向きに加わる力の大きさを定義
            float forceMagnitude = 2.5f;

            // 向きと大きさからSphereに加わる力を計算する
            Vector3 force = forceMagnitude * forceDirection;

            // SphereオブジェクトのRigidbodyコンポーネントへの参照を取得
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();

            // 力を加えるメソッド
            // ForceMode.Impulseは撃力
            rb.AddForce(force, ForceMode.Impulse);
*/


            yield return new WaitWhile(() =>
            {
                return Vector3.Distance(transform.localPosition, pointB) > 0.3f;
            });
            

            // NavmeshAgentを到達した事にして、Navmeshを再開
            agent.CompleteOffMeshLink();
            agent.Resume();
            agentRigidbody.isKinematic = true;
        }
    }
}
