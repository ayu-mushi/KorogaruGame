using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {

   public Transform goal;

   void Start () {
    UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    if(agent.pathStatus != UnityEngine.AI.NavMeshPathStatus.PathInvalid) {
      agent.destination = goal.position;
    } else {
      Debug.Log("NavMeshがダメ");
    }
   }
}
