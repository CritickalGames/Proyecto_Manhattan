using UnityEngine;
 using System.Collections;
 
 public class AutoDestroy : MonoBehaviour {
     void Start () 
     {
         Destroy (gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length); 
     }
 }
