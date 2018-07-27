using UnityEngine;


public class Coin : MonoBehaviour {

    [SerializeField] AudioClip coinPickup;
    bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !triggered)
        {
            triggered = true;
            Vector3 camPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(coinPickup, camPos);
            GameObject.FindObjectOfType<GameSession>().PickedUpCoin();
            Destroy(this.gameObject);
        }
    }
}
