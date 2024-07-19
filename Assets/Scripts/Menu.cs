using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public static Menu Instance { get; private set; }
    public bool Pause { get; private set; }
    public int loseTime = 5;
    public float alertAnimationTime = 0.25f;
    public float alertTime = 2.0f;
    public float alertAnimationScaleFrom = 0.0f;
    public float alertAnimationScaleTo = 1.0f;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TextMeshProUGUI alertPanel;
    [SerializeField] private TextMeshProUGUI ammoPanel;
    [SerializeField] private TextMeshProUGUI losePanel;
    [SerializeField] private TextMeshProUGUI loseText;
    [SerializeField] private RectTransform menuPanel;
    [SerializeField] private RectTransform winPanel;
    [SerializeField] private RectTransform selectPanel;


    public bool lose;

    private Coroutine loseCoroutine;
    private Coroutine alertCoroutine;

    private void Awake()
    {
        Instance = this;
        lose = false;
        Pause = false;
        loseCoroutine = null;
        alertCoroutine = null;
        audioSource = GetComponent<AudioSource>();
        selectPanel?.gameObject.SetActive(false);
        alertPanel?.gameObject.SetActive(false);
        losePanel?.gameObject.SetActive(false);
        loseText?.gameObject.SetActive(false);
        winPanel?.gameObject.SetActive(false);
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            menuPanel?.gameObject.SetActive(false);
        }
    }

    private void Update()
    { 
        if (Input.GetButtonDown("Cancel") && !lose && SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (menuPanel.gameObject.activeInHierarchy)
            {
                Pause = false;
                menuPanel.gameObject.SetActive(false);
            }
            else
            {
                Pause = true;
                menuPanel.gameObject.SetActive(true);
            }
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        selectPanel.gameObject.SetActive(false);
        winPanel.gameObject.SetActive(false);
        menuPanel.gameObject.SetActive(true);
    }

    public void SelectMenu()
    {
        Pause = true;
        selectPanel?.gameObject.SetActive(true);
        menuPanel?.gameObject.SetActive(false);
    }

    public void WinText()
    {
        audioSource.PlayOneShot(Resources.Load<AudioClip>($@"Sounds\Win_Sound"));
        winPanel.gameObject.SetActive(true);
    }

    public void SelectLevel(int number)
    {
        SceneManager.LoadScene(number);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AmmoUpdate(string name, string value)
    {
        ammoPanel.text = $"{name}:{value}";
    }

    public void DisplayeAlert(string text, Color color)
    {
        if (alertCoroutine != null) return;
        alertPanel.text = text;
        alertPanel.color = color;
        alertCoroutine = StartCoroutine(AlertCoroutine(alertAnimationScaleFrom, alertAnimationScaleTo, alertAnimationTime, alertTime));
    }

    public void LoseTimerStart()
    {
        if (loseCoroutine != null) return;
        loseCoroutine = StartCoroutine(LoseCoroutine());
    }

    public void LoseTimerStop()
    {
        if (loseCoroutine != null && !lose)
        {
            losePanel.gameObject.SetActive(false);
            loseText.gameObject.SetActive(false);
            StopCoroutine(loseCoroutine);
            loseCoroutine = null;
        }
    }
  
    public IEnumerator LoseCoroutine()
    {
        losePanel.gameObject.SetActive(true);
        int timer = loseTime;
        while (true)
        {
            losePanel.text = timer.ToString();
            yield return new WaitForSeconds(1);
            timer--;
            if (timer < 0) break;
        }
        lose = true;
        Pause = true;
        selectPanel?.gameObject.SetActive(false);
        loseText?.gameObject.SetActive(true);
        menuPanel?.gameObject.SetActive(true);
        losePanel.gameObject.SetActive(false);
        audioSource.PlayOneShot(Resources.Load<AudioClip>($@"Sounds\Lost_Sound"));
        loseCoroutine = null;
    }

    public IEnumerator AlertCoroutine(float from, float to, float animtime = 1.0f, float time = 2.0f)
    {
        alertPanel.gameObject.SetActive(true);
        Vector3 fromScale = new Vector3(from, from, from);
        Vector3 toScale = new Vector3(to, to, to);
        float multiply = 1.0f / animtime;
        float timer = 0.0f;
        while (timer < 1.0f)
        {
            timer += Time.unscaledDeltaTime * multiply;
            alertPanel.rectTransform.localScale = Vector3.Lerp(fromScale, toScale, timer);
            yield return null;
        }
        alertPanel.rectTransform.localScale = toScale;
        yield return new WaitForSeconds(time);
        while (timer > 0.0f)
        {
            timer -= Time.unscaledDeltaTime * multiply;
            alertPanel.rectTransform.localScale = Vector3.Lerp(fromScale, toScale, timer);
            yield return null;
        }
        alertPanel.rectTransform.localScale = fromScale;
        alertPanel.gameObject.SetActive(false);
        alertCoroutine = null;
    }
}
