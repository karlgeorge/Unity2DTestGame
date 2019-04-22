using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveX;
    public float moveY;
    public float push;
    public Text score;
    public Text win;
    public Text hint;



    Rigidbody2D rb2D;
    int s = 0;
    int loss = 0;
    bool end = false;
    // Start is called before the first frame update
    void Start()
    {
        end = false;
        rb2D = GetComponent<Rigidbody2D>();
        SetScore();
        win.text = "";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveX, moveY);
        rb2D.AddForce(push * movement);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            s = s + 1;
            SetScore();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            loss = loss + 1;
            SetScore();
        }
    }

    void SetScore()
    {
        if (end)
        {
            return;
        }
        score.text = "目前分数: " + (s - loss).ToString();
        if (loss != 0)
        {
            hint.text = "惩罚：" + loss.ToString() + " !";
            hint.color = Color.red;
        }
        if (s - loss >= 12)
        {
            end = true;
            win.text = "你赢了！";
            Invoke("Restart", 5.0f);
        }
        else if (14 - 12 - loss < 0)
        {
            end = true;
            win.text = "你输了！";
            Invoke("Restart", 5.0f);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(0);
    }
}