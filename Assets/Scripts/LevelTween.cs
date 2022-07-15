using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LevelTween : MonoBehaviour
{
    [SerializeField]
    GameObject backPanel, homeButton, replayButton, nextButton,
    star1, star2, star3, score, gems, levelSucess;
    [SerializeField] TMP_Text textGem, textLevel;
    [SerializeField] Sprite win, close;
    public int i;

    void Start()
    {
        if (i == 0)
            Destroy(nextButton);
        else
            Destroy(replayButton);
        StartCoroutine("ActiveTween");

    }

    IEnumerator ActiveTween()
    {
        yield return new WaitForSeconds(1);
        backPanel.SetActive(true);
        LeanTween.alpha(backPanel, 1f, .5f).setDelay(.5f);
        //LeanTween.rotateAround(colorWheel, Vector3.forward, -360f, 10f).setLoopClamp();
        LeanTween.scale(levelSucess, new Vector3(1.5f, 1.5f, 1.5f), 1f).setDelay(.1f).setEase(LeanTweenType.easeOutElastic).setOnComplete(LevelComplete);
        //LeanTween.moveLocal(levelSucess, new Vector3(-30f, 350.88f, 2f), 0.7f).setDelay(2f).setEase(LeanTweenType.easeInOutCubic);
        //LeanTween.scale(levelSucess, new Vector3(1f, 1f, 1f), 2f).setDelay(1.7f).setEase(LeanTweenType.easeInOutCubic);
    }

    void LevelComplete()
    {

        LeanTween.moveLocal(backPanel, new Vector3(0f, 0f, 0f), 0.7f).setDelay(.1f).setEase(LeanTweenType.easeOutCirc).setOnComplete(StarsAnim);
        LeanTween.scale(homeButton, new Vector3(1f, 1f, 1f), 2f).setDelay(.8f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.alpha(score.GetComponent<RectTransform>(), 1f, .5f).setDelay(.2f);
        LeanTween.alpha(gems.GetComponent<RectTransform>(), 1f, .5f).setDelay(.5f);
        
        if(replayButton) LeanTween.scale(replayButton, new Vector3(1f, 1f, 1f), 2f).setDelay(.9f).setEase(LeanTweenType.easeOutElastic);
        if(nextButton) LeanTween.scale(nextButton, new Vector3(1f, 1f, 1f), 2f).setDelay(.9f).setEase(LeanTweenType.easeOutElastic);
    }


    void StarsAnim()
    {
        int c = GameManager.Instance.coin_lv * (GameManager.Instance.up < 1 ? 1 : GameManager.Instance.up); 
        textGem.text = c.ToString();
        if(GameManager.Instance.up < 0)
            textLevel.text = "x0";
        else
        textLevel.text = "x" + GameManager.Instance.up.ToString();

        LeanTween.scale(textGem.gameObject, new Vector3(1f, 1f, 1f), 1f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(textLevel.gameObject, new Vector3(1f, 1f, 1f), 1f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(star1, new Vector3(1f, 1f, 1f), 2f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(star2, new Vector3(1f, 1f, 1f), 2f).setDelay(.1f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(star3, new Vector3(1f, 1f, 1f), 2f).setDelay(.2f).setEase(LeanTweenType.easeOutElastic);

    }
}
