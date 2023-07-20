using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Hero
{
  public class HeroAnimator : MonoBehaviour, IAnimationStateReader
  {
    [SerializeField] private Animator _animator;

    private static readonly int TakeHash = Animator.StringToHash("IsTaking");
    private static readonly int WinHash = Animator.StringToHash("Win");

    private readonly int _idleStateHash = Animator.StringToHash("Idle");
    private readonly int _takeStateHash = Animator.StringToHash("Take");
    private readonly int _winStateHash = Animator.StringToHash("Win");

    public event Action<AnimatorState> StateEntered;
    public event Action<AnimatorState> StateExited;

    public AnimatorState State { get; private set; }
    public bool IsTaking => State == AnimatorState.Take;
    public bool IsWin => State == AnimatorState.Win;

    private void Update()
    {
      Debug.Log(State);
    }

    public void EnteredState(int stateHash)
    {
      State = StateFor(stateHash);
      StateEntered?.Invoke(State);
    }

    public void ExitedState(int stateHash)
    {
      StateExited?.Invoke(StateFor(stateHash));
    }

    public float GetLength() =>
      _animator.runtimeAnimatorController.animationClips[1].length;

    public void StartTaking()
    {
      _animator.SetTrigger(TakeHash);
    }

    public void PlayWin()
    {
      _animator.SetTrigger(WinHash);
    }

    private AnimatorState StateFor(int stateHash)
    {
      AnimatorState state;
      if (stateHash == _idleStateHash)
      {
        state = AnimatorState.Idle;
      }
      else if (stateHash == _takeStateHash)
      {
        state = AnimatorState.Take;
      }
      else if (stateHash == _winStateHash)
      {
        state = AnimatorState.Win;
      }
      else
      {
        state = AnimatorState.Unknown;
      }

      return state;
    }
  }
}