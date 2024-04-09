using UnityEngine;
using NaughtyAttributes;
using Obvious.Soap;
using UnityEngine.UI;

public class MainPlayerManager : MonoBehaviour
{
    [Header("Dependencies")]
    [Required][SerializeField] private Animator _playerAnimator;
    [Required][SerializeField] private CharacterPiecesSO _characterPiecesSO;
    [Required][SerializeField] private ScriptableEventNoParam _playerDeathChannel;
    [Required][SerializeField] private ScriptableEventInt _enemyDamageChannel;
    [Required][SerializeField] private ScriptableEventInt _damagePlayerChannel;
    [Required][SerializeField] private ScriptableEventBool _gamePausedChannel;
    [SerializeField] private Slider _attackSlider;
    [SerializeField] private Slider _evadeSlider;

    [Header("Variable Dependencies")]
    [Required][SerializeField] private FloatVariable _playerDamage;
    [Required][SerializeField] private FloatVariable _playerSpeed;
    [Required][SerializeField] private FloatVariable _playerHealth;
    [Required][SerializeField] private FloatVariable _maxHealth;

    [Header("Settings")]
    [SerializeField] private AnimationCurve _attackCooldownBySpeed;

    [SerializeField] private float _evadeCooldownTime = 5f;

    private float _attackCooldownTime = 10f;
    private float _elapsedAttackTime = 0f;
    private float _elapsedEvadeTime = 0f;
    private bool _isAttacking = false;
    private bool _isEvading = false;
    private bool _gamePaused;

    private void Awake()
    {
        FirstLoad();
    }

    private void OnEnable()
    {
        _damagePlayerChannel.OnRaised += ReceiveDamage;
        _gamePausedChannel.OnRaised += (bool value) => _gamePaused = value;
        _playerHealth.Load();
    }

    private void OnDisable()
    {
        _damagePlayerChannel.OnRaised -= ReceiveDamage;
        _gamePausedChannel.OnRaised -= (bool value) => _gamePaused = value;
        _playerHealth.Save();
    }

    private void Update()
    {
        ReadInput();
    }

    public void SendAttack()
    {
        if (_gamePaused) return;
        _enemyDamageChannel.Raise(Mathf.RoundToInt(_playerDamage));
    }

    public void StartAttack()
    {
        _isAttacking = true;
    }

    public void FinishAttack()
    {
        _isAttacking = false;
    }

    public void StartEvation()
    {
        _isEvading = true;
    }

    public void FinishEvation()
    {
        _isEvading = false;
    }

    private void ReceiveDamage(int damage)
    {
        if (_isEvading) return;
        _playerHealth.Value -= damage;
        Debug.Log("Player received " + damage + " damage. Health left: " + _playerHealth);
        if (_playerHealth <= 0)
        {
            _playerDeathChannel.Raise();
            Debug.Log("Player Died");
            _playerHealth.Value = _maxHealth.Value;
        }
    }

    private void ReadInput()
    {
        UpdateSliders();
        if (_gamePaused) return;
        _elapsedAttackTime += Time.deltaTime;
        _elapsedEvadeTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && !_isAttacking && !_isEvading)
        {
            if (_elapsedAttackTime < _attackCooldownTime) return;
            _elapsedAttackTime = 0f;
            SetCooldownTime();
            _isAttacking = true;
            _playerAnimator.Play("Rogue_attack_01");
            _elapsedAttackTime = 0f;
        }

        if (Input.GetKeyDown(KeyCode.E) && !_isAttacking && !_isEvading)
        {
            if(_elapsedEvadeTime < _evadeCooldownTime) return;
            _elapsedEvadeTime = 0f;
            _isEvading = true;
            _playerAnimator.Play("Rogue_evade_01");
        }
    }

    private void SetCooldownTime()
    {
        _attackCooldownTime = _attackCooldownBySpeed.Evaluate(_playerSpeed.Value);
    }

    private void UpdateSliders()
    {
        if(_attackSlider == null || _evadeSlider == null) return;
        _attackSlider.value = _elapsedAttackTime / _attackCooldownTime;
        _evadeSlider.value = _elapsedEvadeTime / _evadeCooldownTime;
    }

    private void FirstLoad()
    {
        if (!PlayerPrefs.HasKey(BodyPartType.Hood.ToString()))
        {
            _characterPiecesSO.FirstSave();
            _characterPiecesSO.HardReset();
            _characterPiecesSO.LoadInfo();
        }
        else
        {
            _characterPiecesSO.LoadInfo();
        }
       
    }
}
