using UnityEngine;

namespace App.Player
{
    public class Bank : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {

            }
        }
    }
}