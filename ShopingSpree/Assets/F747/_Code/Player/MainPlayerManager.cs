using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class MainPlayerManager : MonoBehaviour
{
    [Header("Dependencies")]
    [Required][SerializeField] private Animator _playerAnimator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _playerAnimator.Play("Rogue_attack_01");
        }
    }
}
