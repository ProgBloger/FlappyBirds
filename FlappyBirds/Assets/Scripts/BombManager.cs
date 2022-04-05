using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombManager : MonoBehaviour
{
    [SerializeField] Spawner spawner;
    public GameObject blowPicture;
    public Sprite bomb;
    public Sprite bombInactive;
    public Button bombButton;
    private bool bombIsActive;

    // Start is called before the first frame update
    void Start()
    {
        SetBombInactive();
    }

    public void OnBombClicked()
    {
        if(bombIsActive)
        {
            SetBombInactive();
            BlowUpTheBomb();
        }
    }

    public  void SetBombActive()
    {
        bombIsActive = true;
        bombButton.image.sprite = bomb;
    }

    public void SetBombInactive()
    {
        bombIsActive = false;
        bombButton.image.sprite = bombInactive;
    }

    private void BlowUpTheBomb()
    {
        blowPicture.SetActive(true);
        spawner.StopSpawn();
        spawner.BlowUpPipes();
        StartCoroutine("ResumeSpawn");
    }

    private IEnumerator ResumeSpawn()
    {
        yield return new WaitForSeconds(1);
        
        blowPicture.SetActive(false);

        yield return new WaitForSeconds(2);
        
        spawner.StartSpawn();
    }
}
