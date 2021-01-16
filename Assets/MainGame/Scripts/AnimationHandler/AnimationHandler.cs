using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    #region Classes
    [System.Serializable]
	public class StateNameToAnimationReference
	{
		public string stateName;
		public AnimationReferenceAsset animation;
	}

	[System.Serializable]
	public class AnimationTransition
	{
		public AnimationReferenceAsset from;
		public AnimationReferenceAsset to;
		public AnimationReferenceAsset transition;
	}
	#endregion

    public SkeletonAnimation skeletonAnimation;
	public List<StateNameToAnimationReference> statesAndAnimations = new List<StateNameToAnimationReference>();
	public List<AnimationTransition> transitions = new List<AnimationTransition>(); // Alternately, an AnimationPair-Animation Dictionary (commented out) can be used for more efficient lookups.

	readonly Dictionary<Spine.AnimationStateData.AnimationPair, Spine.Animation> transitionDictionary = new Dictionary<AnimationStateData.AnimationPair, Spine.Animation>(Spine.AnimationStateData.AnimationPairComparer.Instance);

	public Spine.Animation TargetAnimation { get; private set; }

	void Awake()
	{
		foreach (var entry in transitions)
		{
			transitionDictionary.Add(new AnimationStateData.AnimationPair(entry.from.Animation, entry.to.Animation), entry.transition.Animation);
		}
	}

	public void SetFlip(float horizontal)
	{
		if (horizontal != 0)
		{
			skeletonAnimation.Skeleton.ScaleX = horizontal > 0 ? 1f : -1f;
		}
	}
	public void PlayAnimation(string stateShortName, int layerIndex, bool loop = true)
	{
		var animation = GetAnimation(stateShortName);
		if (animation == null)
		{
			return;
		}

		PlayNewAnimation(animation, layerIndex, loop);
	}
	
	private void PlayNewAnimation(Spine.Animation target, int layerIndex, bool loop)
	{
		Spine.Animation transition = null;
		Spine.Animation current = null;

		current = GetCurrentAnimation(layerIndex);
		if (current != null)
			transition = TryGetTransition(current, target);

		if (transition != null)
		{
			skeletonAnimation.AnimationState.SetAnimation(layerIndex, transition, false);
			skeletonAnimation.AnimationState.AddAnimation(layerIndex, target, loop, 0f);
		}
		else
		{
			skeletonAnimation.AnimationState.SetAnimation(layerIndex, target, loop);
		}

		this.TargetAnimation = target;
	}

	private Spine.Animation GetAnimation(string shortName)
	{
		return GetAnimation(StringToHash(shortName));
	}
	private Spine.Animation GetAnimation(int shortNameHash)
	{
		var foundState = statesAndAnimations.Find(entry => StringToHash(entry.stateName) == shortNameHash);
		return (foundState == null) ? null : foundState.animation;
	}

	Spine.Animation TryGetTransition(Spine.Animation from, Spine.Animation to)
	{
		Spine.Animation foundTransition = null;
		transitionDictionary.TryGetValue(new AnimationStateData.AnimationPair(from, to), out foundTransition);
		return foundTransition;
	}

	Spine.Animation GetCurrentAnimation(int layerIndex)
	{
		var currentTrackEntry = skeletonAnimation.AnimationState.GetCurrent(layerIndex);
		return (currentTrackEntry != null) ? currentTrackEntry.Animation : null;
	}

	int StringToHash(string s)
	{
		return Animator.StringToHash(s);
	}


}