using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {

    public Text PercentText;
    public RectTransform HealthBar;

    public Damageable damageable;

    private float initBarWidth;

    private void Start () {
        damageable.OnTakeDamage.AddListener (updateUI);
        initUI ();
        updateUI ();
    }

    private void initUI () {
        // assume that bar is 100% on start
        initBarWidth = HealthBar.rect.width;
    }

    private void updateUI () {
        updatePercentText ();
        updateBar ();
    }

    private void updatePercentText () {
        PercentText.text = Mathf.RoundToInt (getPercentHp () * 100).ToString ();
    }

    private void updateBar () {
        float percent = getPercentHp ();
        HealthBar.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, initBarWidth * percent);
    }

    private float getPercentHp () {
        return (float) damageable.GetCurrentHp () / (float) damageable.GetMaximumHp ();
    }
}
