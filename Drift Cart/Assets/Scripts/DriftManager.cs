using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class DriftManager : MonoBehaviour
{
    public Rigidbody cartRb;
    public TMP_Text totalScoreText;
    public TMP_Text currentScoreText;
    public TMP_Text multyplierText;

    private float speed;
    private float driftAngle = 0;
    private float driftMultyplier = 0;
    private float totalScore = 0;
    private float currentScore = 0;

    private bool isDrifting = false;

    public float minAngle=10;
    private float minSpeed=2;
    private float driftDelay = 0.2f;
    public GameObject driftObject;
    public Color nearStopColor;
    public Color driftEndedColor;
    public Color normalDriftColorl;

    private IEnumerator stopDriftingCoroutine = null;

    void Start()
    {
        driftObject.SetActive(false);
    }
    private void Update()
    {
        ManageDrift();
        ManageUI();
    }
    void ManageDrift()
    {
        speed = cartRb.velocity.magnitude;
        driftAngle = Vector3.Angle(cartRb.transform.forward, (cartRb.velocity + cartRb.transform.forward).normalized);
        if (driftAngle > 130)
        {
            driftAngle= 0;
        }
        if (driftAngle >minAngle && speed>minSpeed)
        {
            if (!isDrifting || stopDriftingCoroutine != null)
            {
                StartDrift();
            }
        }
        else
        {
            if (isDrifting && stopDriftingCoroutine == null)
                {
                StopDrift();
            }
        }
        if (isDrifting)
        {
            currentScore += Time.deltaTime * driftAngle * driftMultyplier;
            driftMultyplier += Time.deltaTime;
            driftObject.SetActive(true);
        }
    }

    async void StartDrift()
    {
        if (!isDrifting)
        {
            await Task.Delay(Mathf.RoundToInt(1000 * driftDelay));
            driftMultyplier= 1;
        }
        if (stopDriftingCoroutine != null)
        {
            StopCoroutine(stopDriftingCoroutine);
            stopDriftingCoroutine = null;
        }
        currentScoreText.color = normalDriftColorl;
        isDrifting= true;
    }
    void StopDrift()
    {
        stopDriftingCoroutine= StoppingDrift();
        StartCoroutine(stopDriftingCoroutine);
    }
    private IEnumerator StoppingDrift()
    {
        yield return new WaitForSeconds(0.1f);
        currentScoreText.color = nearStopColor;
        yield return new WaitForSeconds(driftDelay * 4f);
        totalScore += currentScore;
        isDrifting = false;
        currentScoreText.color = driftEndedColor;
        yield return new WaitForSeconds(0.5f);
        currentScore = 0;
        driftObject.SetActive(false);
    }
    void ManageUI()
    {
        totalScoreText.text ="Score: " + (totalScore + currentScore).ToString("###,###,000");
        multyplierText.text = driftMultyplier.ToString("###,###,##0.0")+"X";
        currentScoreText.text = currentScore.ToString("###,###,000");
    }
}
