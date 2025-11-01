using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Interactebles")]
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;
    public GameObject cherryObject;
    public GameObject strawberryObject;
    public GameObject orangeObject;
    public GameObject appleObject;
    public GameObject melonObject;
    public GameObject galaxianObject;
    public GameObject bellObject;
    public GameObject keyObject;

    [Header("Audio Elements")]
    public AudioSource source;
    public AudioClip waka1;
    public AudioClip waka2;
    public AudioClip pacDeath;
    public AudioClip ghostEaten;
    public AudioClip extraLife;
    public AudioClip fruitEaten;

    public bool isMusicPlaying = false;
    public bool isGhostSirenPlaying = false;

    [Header("UI Elements")]
    public Text scoreText;
    public Text highscoreText;
    public GameObject gameOverText;
    public GameObject readyText;
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;
    public GameObject firstFruit;
    public GameObject secondFruit;
    public GameObject thirdFruit;
    public GameObject fourthFruit;
    public GameObject fifthFruit;
    public GameObject sixthFruit;
    public GameObject seventhFruit;
    public Sprite cherrySprite;
    public Sprite strawberrySprite;
    public Sprite orangeSprite;
    public Sprite appleSprite;
    public Sprite melonSprite;
    public Sprite galaxianSprite;
    public Sprite bellSprite;
    public Sprite keySprite;

    private Image firstFruitImage;
    private Image secondFruitImage;
    private Image thirdFruitImage;
    private Image fourthFruitImage;
    private Image fifthFruitImage;
    private Image sixthFruitImage;
    private Image seventhFruitImage;

    [Header("Variables")]
    public float frightenedDuration = 0;
    public int ghostMultiplier { get; private set; } = 1;
    public int score { get; private set; }
    public int highscore { get; private set; }  
    public int lives { get; private set; }
    public int currentRound { get; private set; }

    public int pelletsEaten {  get; private set; }

    private bool gotExtraLife = false;
    private bool isPacmanDead = false;

    private void Start()
    {
        firstFruitImage = firstFruit.GetComponent<Image>();
        secondFruitImage = secondFruit.GetComponent<Image>();
        thirdFruitImage = thirdFruit.GetComponent<Image>();
        fourthFruitImage = fourthFruit.GetComponent<Image>();
        fifthFruitImage = fifthFruit.GetComponent<Image>();
        sixthFruitImage = sixthFruit.GetComponent<Image>();
        seventhFruitImage = seventhFruit.GetComponent<Image>();

        highscore = PlayerPrefs.GetInt("highscore");
        scoreText.text = this.score.ToString();
        highscoreText.text = this.highscore.ToString();
        NewGame();
        UpdateHighScoreUI();
    }

    private void Update()
    {
        if (this.lives <= 0 && Input.GetKeyDown(KeyCode.Space))
        {
            NewGame();
        }

        if (this.score >= 10000 && !gotExtraLife)
        {
            gotExtraLife = true;
            this.lives += 1;
            source.PlayOneShot(extraLife);

            if (this.lives == 2)
            {
                life1.gameObject.SetActive(true);
            }
            else if (this.lives == 3)
            {
                life2.gameObject.SetActive(true);
            }
            else if (this.lives == 4)
            {
                life3.gameObject.SetActive(true);
            }
        }

        SpawnFruit();

        scoreText.text = this.score.ToString();

        CheckHighScore();
    }

    private void CheckHighScore()
    {
        if (score > PlayerPrefs.GetInt("highscore", 0))
        {
            PlayerPrefs.SetInt("highscore", score);
            UpdateHighScoreUI();
        }
    }

    private void UpdateHighScoreUI()
    {
        highscoreText.text = $"{PlayerPrefs.GetInt("highscore", 0)}";
    }

    private void NewGame()
    {
        // Reset everything
        SetScore(0);
        SetLives(3);
        NewRound();

        currentRound = 1;
        gotExtraLife = false;
        frightenedDuration = 8;

        gameOverText.SetActive(false);

        life1.gameObject.SetActive(true);
        life2.gameObject.SetActive(true);

        secondFruit.gameObject.SetActive(false);
        thirdFruit.gameObject.SetActive(false);
        fourthFruit.gameObject.SetActive(false);
        fifthFruit.gameObject.SetActive(false);
        sixthFruit.gameObject.SetActive(false);
        seventhFruit.gameObject.SetActive(false);

        firstFruitImage.sprite = cherrySprite;
        secondFruitImage.sprite = strawberrySprite;
        thirdFruitImage.sprite = orangeSprite;
        fourthFruitImage.sprite = orangeSprite;
        fifthFruitImage.sprite = appleSprite;
        sixthFruitImage.sprite= appleSprite;
        seventhFruitImage.sprite = melonSprite;

        this.ghosts[4].gameObject.SetActive(false);
        this.ghosts[5].gameObject.SetActive(false);
        this.ghosts[6].gameObject.SetActive(false);
        this.ghosts[7].gameObject.SetActive(false);
        this.ghosts[8].gameObject.SetActive(false);
    }

    private void NewRound()
    {
        pelletsEaten = 0;
        currentRound += 1;

        frightenedDuration = 8 - (currentRound - 1);

        if (currentRound == 6)
        {
            frightenedDuration += 3;
        }
        else if (currentRound == 9)
        {
            frightenedDuration += 1.5f;
        }
        else if (currentRound == 13)
        {
            frightenedDuration += 0.5f;
        }

        // Get all the pellets and re-enable them
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    private void ResetState()
    {
        isMusicPlaying = true;
        isGhostSirenPlaying = true;
        isPacmanDead = false;

        // Control lives UI
        if (this.lives <= 1)
        {
            life1.gameObject.SetActive(false);
        }
        else if (this.lives <= 2)
        {
            life2.gameObject.SetActive(false);
        }
        else if (this.lives <= 3)
        {
            life3.gameObject.SetActive(false);
        }

        ResetGhostMultiplier();
        ManageFruitUI();

        StartCoroutine(DelayStart());

        // Reset ghosts
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();

            this.ghosts[5].gameObject.SetActive(false);
            this.ghosts[6].gameObject.SetActive(false);
            this.ghosts[7].gameObject.SetActive(false);
            this.ghosts[8].gameObject.SetActive(false);
            
            if (currentRound == 1)
            {
                this.ghosts[4].gameObject.SetActive(false);
            }
            else if (currentRound <= 3)
            {
                this.ghosts[4].gameObject.SetActive(true);
            }
            else if (currentRound <= 5)
            {
                this.ghosts[5].gameObject.SetActive(true);
            }
            else if (currentRound <= 8)
            {
                this.ghosts[5].gameObject.SetActive(true);
                this.ghosts[6].gameObject.SetActive(true);
            }
            else if (currentRound <= 12)
            {
                this.ghosts[5].gameObject.SetActive(true);
                this.ghosts[6].gameObject.SetActive(true);
                this.ghosts[7].gameObject.SetActive(true);
            }
            else if (currentRound >= 13)
            {
                this.ghosts[5].gameObject.SetActive(true);
                this.ghosts[6].gameObject.SetActive(true);
                this.ghosts[7].gameObject.SetActive(true);
                this.ghosts[8].gameObject.SetActive(true);
            }
        }

        // Reset Pacman
        this.pacman.ResetState();
    }

    private void GameOver()
    {
        gameOverText.SetActive(true);

        // Disable ghosts
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }

        // Disable Pacman
        this.pacman.gameObject.SetActive(false);
    }

    private void SetScore(int score)
    {
        this.score = score;
    }

    private void SetLives (int lives)
    {
        this.lives = lives;
    }

    public void GhostEaten(Ghost ghost)
    {
        source.PlayOneShot(ghostEaten);

        int points = ghost.points * this.ghostMultiplier;
        SetScore(this.score + points);
        this.ghostMultiplier = this.ghostMultiplier * 2;
    }

    public void PacmanEaten()
    {
        isMusicPlaying = false;
        isGhostSirenPlaying = false;

        this.pacman.gameObject.SetActive(false);

        ResetFruit();

        if (!isPacmanDead)
        {
            isPacmanDead = true;
            SetLives(this.lives - 1);
            source.PlayOneShot(pacDeath);
        }

        // Determine if game over needs to happen
        if (this.lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
        }
        else
        {
            GameOver();
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        pelletsEaten += 1;

        // Play audio for even and odd pellets eaten
        if (pelletsEaten % 2 == 0)
        {
            source.PlayOneShot(waka2);
        }
        else
        {
            source.PlayOneShot(waka1);
        }

        pellet.gameObject.SetActive(false);

        SetScore(this.score + pellet.points);

        // Has the player finished the level?
        if (!HasRemainingPellets())
        {
            isMusicPlaying = false;
            isGhostSirenPlaying= false;

            source.Stop();

            ResetFruit();

            for (int i = 0; i < this.ghosts.Length; i++)
            {
                this.ghosts[i].gameObject.SetActive(false);
            }

            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        this.ghostMultiplier = 1;

        // Set all ghosts to frightened
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].frightened.Enable(frightenedDuration);
        }

        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), frightenedDuration);
    }

    public void FruitEaten(DestroyFruit fruit)
    {
        source.PlayOneShot(fruitEaten);

        if (fruit.gameObject.activeSelf)
        {
            fruit.gameObject.SetActive(false);
        }

        SetScore(this.score + fruit.points);

        fruit.fruitScoreText.text = (fruit.points).ToString();

        StopAllCoroutines();
        StartCoroutine(fruit.DisplayScoreText(2f));
    }

    private bool HasRemainingPellets()
    {
        // Are there any pellets left?
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    } 

    private void SpawnFruit()
    {
        // Spawn fruits in their respective rounds
        if (pelletsEaten == 70 || pelletsEaten == 170)
        {
            if (currentRound == 1)
            {
                cherryObject.gameObject.SetActive(true);
            }
            else if (currentRound == 2)
            {
                strawberryObject.gameObject.SetActive(true);
            }
            else if (currentRound == 3 || currentRound == 4)
            {
                orangeObject.gameObject.SetActive(true);
            }
            else if (currentRound == 5 || currentRound == 6)
            {
                appleObject.gameObject.SetActive(true);
            }
            else if (currentRound == 7 || currentRound == 8)
            {
                melonObject.gameObject.SetActive(true);
            }
            else if (currentRound == 9 || currentRound == 10)
            {
                galaxianObject.gameObject.SetActive(true);
            }
            else if (currentRound == 11 || currentRound == 12)
            {
                bellObject.gameObject.SetActive(true);
            }
            else
            {
                keyObject.gameObject.SetActive(true);
            }

            CancelInvoke();
            Invoke(nameof(ResetFruit), 10.0f);
        }
    }

    private void ResetFruit()
    {
        cherryObject.gameObject.SetActive(false);
        strawberryObject.gameObject.SetActive(false);
        orangeObject.gameObject.SetActive(false);
        appleObject.gameObject.SetActive(false);
        melonObject.gameObject.SetActive(false);
        galaxianObject.gameObject.SetActive(false);
        bellObject.gameObject.SetActive(false);
        keyObject.gameObject.SetActive(false);
    }

    private void ManageFruitUI()
    {
        // Display fruit UI akin to round
        if (currentRound == 1)
        {
            firstFruit.SetActive(true);
        }
        else if (currentRound == 2)
        {
            secondFruit.SetActive(true);
        }
        else if (currentRound == 3)
        {
            thirdFruit.SetActive(true);
        }
        else if (currentRound == 4)
        {
            fourthFruit.SetActive(true);
        }
        else if (currentRound == 5)
        {
            fifthFruit.SetActive(true);
        }
        else if (currentRound == 6)
        {
            sixthFruit.SetActive(true);
        }
        else if (currentRound == 7)
        {
            seventhFruit.SetActive(true);
        }
        else if (currentRound == 8)
        {
            firstFruitImage.sprite = strawberrySprite;
            secondFruitImage.sprite = orangeSprite;
            fourthFruitImage.sprite = appleSprite;
            sixthFruitImage.sprite = melonSprite;
        }
        else if (currentRound == 9)
        {
            firstFruitImage.sprite = orangeSprite;
            thirdFruitImage.sprite = appleSprite;
            fifthFruitImage.sprite = melonSprite;
            seventhFruitImage.sprite = galaxianSprite;
        }
        else if (currentRound == 10)
        {
            secondFruitImage.sprite = appleSprite;
            fourthFruitImage.sprite = melonSprite;
            sixthFruitImage.sprite = galaxianSprite;
        }
        else if (currentRound == 11)
        {
            firstFruitImage.sprite = appleSprite;
            thirdFruitImage.sprite = melonSprite;
            fifthFruitImage.sprite = galaxianSprite;
            seventhFruitImage.sprite = bellSprite;
        }
        else if (currentRound == 12)
        {
            secondFruitImage.sprite = melonSprite;
            fourthFruitImage.sprite = galaxianSprite;
            sixthFruitImage.sprite = bellSprite;
        }
        else if (currentRound == 13)
        {
            firstFruitImage.sprite = melonSprite;
            thirdFruitImage.sprite = galaxianSprite;
            fifthFruitImage.sprite = bellSprite;
            seventhFruitImage.sprite = keySprite;
        }
        else if (currentRound == 14)
        {
            secondFruitImage.sprite = galaxianSprite;
            fourthFruitImage.sprite = bellSprite;
            sixthFruitImage.sprite = keySprite;
        }
        else if (currentRound == 15)
        {
            firstFruitImage.sprite = galaxianSprite;
            thirdFruitImage.sprite = bellSprite;
            fifthFruitImage.sprite = keySprite;
        }
        else if (currentRound == 16)
        {
            secondFruitImage.sprite = bellSprite;
            fourthFruitImage.sprite = keySprite;
        }
        else if (currentRound == 17)
        {
            firstFruitImage.sprite = bellSprite;
            thirdFruitImage.sprite = keySprite;
        }
        else if (currentRound == 18)
        {
            secondFruitImage.sprite = keySprite;
        }
        else
        {
            firstFruitImage.sprite = keySprite;
        }
    }

    private void ResetGhostMultiplier()
    {
        this.ghostMultiplier = 1;
    }

    private IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(3.0f);
        readyText.gameObject.SetActive(false);
    }
}