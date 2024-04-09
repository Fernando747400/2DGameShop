using NaughtyAttributes;
using Obvious.Soap;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("SO Dependenices")]
    [Required][SerializeField] private ScriptableEventNoParam _enemyDeathChannel;
    [Required][SerializeField] private ScriptableEventInt _damagePlayerChannel;
    [Required][SerializeField] private ScriptableEventInt _enemyDamageChannel;
    [Required][SerializeField] private ScriptableEventBool _gamePausedChannel;

    [Header("Variable Dependenices")]
    [Required][SerializeField] private Animator _enemyAnimator;
    [Required][SerializeField] private IntVariable _maxHealth;
    [Required][SerializeField] private IntVariable _currentHealth;
    [Required][SerializeField] private IntVariable _attackDamage;
    [Required][SerializeField] private FloatVariable _timeBetweenAttacks;

    [Space(10f)]
    [Header("Settings")]
    [MinMaxSlider(0.7f, 1.3f)][SerializeField] private Vector2 _attackTimeOffset;

    private float _elapsedTime = 0f;
    private float _attackTime = 15f;

    private bool _attacking = false;
    private bool _gamePaused = false;

    private void OnEnable()
    {
        _enemyDamageChannel.OnRaised += ReceiveDamage;
        _gamePausedChannel.OnRaised += (bool value) => _gamePaused = value;
        AttackTimeRandomOffset();
    }

    private void OnDisable()
    {
        _enemyDamageChannel.OnRaised -= ReceiveDamage;
        _gamePausedChannel.OnRaised -= (bool value) => _gamePaused = value;
    }

    private void Start()
    {
        _currentHealth.Value = _maxHealth.Value;
    }

    private void Update()
    {
        if (_gamePaused && !_attacking) return;
        IdleCountdown();
    }

    public void StartAttack()
    {
        _attacking = true;
    }

    public void SendAttack()
    {
        _damagePlayerChannel.Raise(_attackDamage);
    }

    public void FinishAttack()
    {
        _attacking = false;
        _enemyAnimator.SetInteger("State", 0);
    }

    private void IdleCountdown()
    {
        CheckAnimator();
        if (_attacking) return;
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime < _attackTime) return;
        _elapsedTime = 0;
        AttackTimeRandomOffset();
        _attacking = true;
        _enemyAnimator.SetInteger("State", 1);
    }

    private void ReceiveDamage(int damage)
    {
        _currentHealth.Value -= damage;
        Debug.Log("Enemey received " +damage+ " damage. Health left: " +_currentHealth);
        if(_currentHealth.Value <= 0)
        {
            Debug.Log("Enemy Died");
            _enemyDeathChannel.Raise();
            _currentHealth.Value = _maxHealth.Value;
        }
    }

    private void CheckAnimator()
    {
        if (_attacking && _enemyAnimator.GetInteger("State") == 0)
        {
            _attacking = false;
            _elapsedTime = 0;
        }
    }

    private void AttackTimeRandomOffset()
    {
        _attackTime = Random.Range(_attackTimeOffset.x,_attackTimeOffset.y) * _timeBetweenAttacks.Value;
    }
}
