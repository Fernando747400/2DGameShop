using UnityEngine;
using NaughtyAttributes;
using Obvious.Soap;
using DG.Tweening;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Dependencies")]
    [Required][SerializeField] private GameObject _coinsText;
    [Required][SerializeField] private ScriptableEventNoParam _playerDeathChannel;
    [Required][SerializeField] private ScriptableEventNoParam _enemyDeathChannel;
    [Required][SerializeField] private IntVariable _playerCoins;
    [Required][SerializeField] private IntVariable _playerWins;
    [Required][SerializeField] private IntVariable _playerDeaths;

    [Header("Settings")]
    [SerializeField] private int _coinsPerEnemy = 50;
    [SerializeField] private int _coinsPerDeath = 60;

    private void OnEnable()
    {
        _playerDeathChannel.OnRaised += PlayerDeath;
        _enemyDeathChannel.OnRaised += EnemyDeath;
        _playerCoins.OnValueChanged += PunchCoins;
    }

    private void OnDisable()
    {
        _playerDeathChannel.OnRaised -= PlayerDeath;
        _enemyDeathChannel.OnRaised -= EnemyDeath;
        _playerCoins.OnValueChanged -= PunchCoins;
    }

    private void PlayerDeath()
    {
        _playerDeaths.Value++;
        _playerCoins.Value -= _coinsPerDeath;
    }

    private void EnemyDeath()
    {
        _playerWins.Value++;
        _playerCoins.Value += _coinsPerEnemy + _playerWins;
    }

    private void PunchCoins(int newValue)
    {
        _coinsText.transform.DOPunchScale(new Vector3(1.05f, 1.05f, 1.05f), 1f);
        /*
        DOTweenTMPAnimator animator = new DOTweenTMPAnimator(_coinsText.GetComponent<TextMeshProUGUI>());
        Sequence sequence = DOTween.Sequence();
        for (int i = 0; i < animator.textInfo.characterCount; ++i)
        {
            if (!animator.textInfo.characterInfo[i].isVisible) continue;
            Vector3 currCharOffset = animator.GetCharOffset(i);
            sequence.Join(animator.DOOffsetChar(i, currCharOffset + new Vector3(0, 2, 0), 1));
        }
        */
    }
}
