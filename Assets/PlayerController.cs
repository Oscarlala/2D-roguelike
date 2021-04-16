using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed;
    public Text inventoryText;
    public static int itemsCollected = 0;
    public GameObject bulletPrefab;
    public float fireRate;
    public float fireDelay;
    private float lastFired;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float vert = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");

        float fireVert = Input.GetAxis("ShootVertical");
        float fireHor = Input.GetAxis("ShootHorizontal");

        if((fireHor != 0 || fireVert != 0) && Time.time > lastFired + fireDelay) {
            Fire(fireHor, fireVert);
            lastFired = Time.time;
        }

        rb.velocity = new Vector3(hor * moveSpeed, vert * moveSpeed, 0);
        inventoryText.text = "Items collected: " + itemsCollected;
    }

    void Fire(float x, float y) {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * fireRate : Mathf.Ceil(x) * fireRate,
            (y < 0) ? Mathf.Floor(y) * fireRate : Mathf.Ceil(y) * fireRate,
            0
        );
    }
}
