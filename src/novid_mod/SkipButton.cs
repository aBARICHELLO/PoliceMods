using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class SkipButton: MonoBehaviour {
	public void Disable() {
		if (this.activated) {
			return;
		}
		this.activated = true;
		this.animator.SetBool(this.IsVisibleHash, false);
		this.timeHeld = 0f;
		this.visibilityTimeout = 0f;
	}

	public void Enable() {
		if (!this.activated) {
			return;
		}
		this.activated = false;
		this.animator.SetBool(this.IsVisibleHash, false);
		this.timeHeld = 0f;
		this.visibilityTimeout = 0f;
	}

	private void Awake() {
		this.animator = base.GetComponent < Animator > ();
		if (this.Icon != null) {
			this.Icon.SetIcon(this.SkipActions[0]);
		}
	}

	// ActivateSkip on Update
	private void Update() {
		this.ActivateSkip();
	}

	private void ActivateSkip() {
		if (this.activated) {
			return;
		}
		this.activated = true;
		this.animator.SetBool(this.IsVisibleHash, false);
		this.OnSkip.Invoke();
	}

	private bool IsSkipDown {
		get {
			if (InputManager.GetMouseButton(0)) {
				return true;
			}
			checked {
				for (int i = 0; i < this.SkipActions.Length; i++) {
					if (InputManager.GetButton(this.SkipActions[i].GetName())) {
						return true;
					}
				}
				return false;
			}
		}
	}

	private void OnApplicationFocus(bool focus) {
		this.timeHeld = 0f;
	}

	private bool IsLineSkipPressed {
		get {
			if (InputManager.GetMouseButtonDown(1)) {
				return true;
			}
			checked {
				for (int i = 0; i < this.LineSkipActions.Length; i++) {
					if (InputManager.GetButtonDown(this.LineSkipActions[i].GetName())) {
						return true;
					}
				}
				return false;
			}
		}
	}

	[SerializeField]
	private float HoldDuration;

	[SerializeField]
	private float VisibilityDuration;

	[SerializeField]
	private InputActions[] SkipActions = new InputActions[] {
		InputActions.Submit
	};

	[SerializeField]
	private InputActions[] LineSkipActions = new InputActions[] {
		InputActions.Action1
	};

	[SerializeField]
	private InputIcon Icon;

	[SerializeField]
	private Image FillImage;

	[SerializeField]
	private UnityEvent OnSkip;

	[SerializeField]
	private UnityEvent OnSkipLine;

	private Animator animator;

	private int IsVisibleHash = Animator.StringToHash("IsVisible");

	private float visibilityTimeout;

	private float timeHeld;

	private bool activated;
}
