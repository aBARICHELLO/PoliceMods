using System;
using UnityEngine;


public class StartCarAnimation: MonoBehaviour {
	public static bool IsPlaying {
		get;
		private set;
	}

	public void Play(Action callback) {
		base.transform.SetAsLastSibling();
		base.transform.localScale = Vector2.one;
		base.transform.localPosition = Vector2.zero;
		StartCarAnimation.IsPlaying = true;
		this.callback = callback;
		this.playing = true;
		base.gameObject.SetActive(true);
		this.Sound.Play();
	}

	public void OnAnimationFinished() {
		StartCarAnimation.IsPlaying = false;
		this.DoCallback();
	}

	public void ShowSubtitle() {
		this.Subtitle.Show(null);
	}

	public void HideSubtitle() {
		this.Subtitle.Hide(null);
	}

	// Stop and DoCallback on Update
	private void Update() {
		this.Stop();
		this.DoCallback();
	}

	private void Awake() {
		if (!this.playing) {
			base.gameObject.SetActive(false);
		}
	}

	private void Stop() {
		StartCarAnimation.IsPlaying = false;
		this.Sound.Stop();
		this.Animator.speed = 0f;
	}

	private void DoCallback() {
		Action action = this.callback;
		if (action != null) {
			this.callback = null;
			action();
		}
	}

	[SerializeField]
	private bool CanBeSkipped;

	[SerializeField]
	private AudioSource Sound;

	[SerializeField]
	private Animator Animator;

	[SerializeField]
	private StoryTextField Subtitle;

	private Action callback;

	private bool playing;
}
