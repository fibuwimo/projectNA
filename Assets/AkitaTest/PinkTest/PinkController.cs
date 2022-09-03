using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PinkController : MonoBehaviour
{
    public GameObject target;
    NavMeshAgent agent;
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

        while (true)
        {
            // OffmeshLinkに乗るまで普通に移動
            yield return new WaitWhile(() => agent.isOnOffMeshLink == false);

            // OffMeshLinkに乗ったので、NavmeshAgentによる移動を止めて、
            // OffMeshLinkの終わりまでNavmeshAgent.speedと同じ速度で移動
            agent.Stop();
            yield return new WaitWhile(() =>
            {
                Vector3 heightPosition = new Vector3(agent.transform.position.x, agent.currentOffMeshLinkData.endPos.y+1.0f, agent.transform.position.z);
                Vector3 endPosition = new Vector3(agent.currentOffMeshLinkData.endPos.x, agent.currentOffMeshLinkData.endPos.y+1.0f, agent.currentOffMeshLinkData.endPos.z);

                if (Mathf.Abs(agent.currentOffMeshLinkData.endPos.y+1.0f - agent.transform.position.y) > 0.1f) {
                    transform.localPosition = Vector3.MoveTowards(
                                                transform.localPosition,
                                                heightPosition, agent.speed * Time.deltaTime);
                }
                else {

                    transform.localPosition = Vector3.MoveTowards(
                                                transform.localPosition,
                                                endPosition, agent.speed * Time.deltaTime);
                }
                return Vector3.Distance(transform.localPosition, endPosition) > 0.1f;
            });

            // NavmeshAgentを到達した事にして、Navmeshを再開
            agent.CompleteOffMeshLink();
            agent.Resume();
        }
    }
}
