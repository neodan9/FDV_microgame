using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float thrustForce = 100f;
    public float rotationSpeed = 120f;

    public GameObject gun, bulletPrefab;

    private Rigidbody _rigid;

    public static int SCORE = 0;

    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        float rotation = Input.GetAxis("Rotate") * Time.deltaTime;
        float thrust = Input.GetAxis("Thrust") * Time.deltaTime;

        Vector3 thrustDirection = transform.right;

        _rigid.AddForce(thrustDirection * thrust * thrustForce);

        transform.Rotate(Vector3.forward, -rotation * rotationSpeed);
        Vector3 newPos = transform.position;
        if(newPos.x > screenBounds.x)
            newPos.x = -screenBounds.x+1;
        else if(newPos.x < -screenBounds.x)
            newPos.x = screenBounds.x-1;
        else if(newPos.y > screenBounds.y)
            newPos.y = -screenBounds.y+1;
        else if(newPos.y < -screenBounds.y)
            newPos.y = screenBounds.y-1;
        transform.position = newPos;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);

            Bullet balaScript = bullet.GetComponent<Bullet>();

            balaScript.targetVector = transform.right;

        }

    }

    private void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.tag == "Enemy"){
            
            SCORE = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
        else{
            Debug.Log("He colisionado con otra cosa...");
        }

    }

}
