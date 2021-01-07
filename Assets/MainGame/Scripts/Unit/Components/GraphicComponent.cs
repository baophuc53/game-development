using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationLayer
{
	Attack = 1,
	Default = 0
}

public class GraphicComponent : BaseComponent
{
	public SkeletonAnimation skeletonAnimation;
	public List<StateNameToAnimationReference> statesAndAnimations = new List<StateNameToAnimationReference>();
	public List<AnimationTransition> transitions = new List<AnimationTransition>(); // Alternately, an AnimationPair-Animation Dictionary (commented out) can be used for more efficient lookups.

	public Camera Camera;

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

	readonly Dictionary<Spine.AnimationStateData.AnimationPair, Spine.Animation> transitionDictionary = new Dictionary<AnimationStateData.AnimationPair, Spine.Animation>(Spine.AnimationStateData.AnimationPairComparer.Instance);

	public Spine.Animation TargetAnimation { get; private set; }

	void Awake()
	{
        // Initialize AnimationReferenceAssets
        //foreach (var entry in statesAndAnimations)
        //{
        //	entry.animation.Initialize();
        //}
        //foreach (var entry in transitions)
        //{
        //	entry.from.Initialize();
        //	entry.to.Initialize();
        //	entry.transition.Initialize();
        //}

        // Build Dictionary
        foreach (var entry in transitions)
        {
            transitionDictionary.Add(new AnimationStateData.AnimationPair(entry.from.Animation, entry.to.Animation), entry.transition.Animation);
        }
    }

	/// <summary>Sets the horizontal flip state of the skeleton based on a nonzero float. If negative, the skeleton is flipped. If positive, the skeleton is not flipped.</summary>
	public void SetFlip(float horizontal)
	{
		if (horizontal != 0)
		{
			skeletonAnimation.Skeleton.ScaleX = horizontal > 0 ? 1f : -1f;
		}
	}

	/// <summary>Plays an animation based on the state name.</summary>
	public void PlayAnimation(string stateShortName, int layerIndex, bool loop = true)
	{
		var animation = GetAnimation(stateShortName);
		if (animation == null)
        {
			return;
        }

		PlayNewAnimation(animation, layerIndex, loop);
	}

	public void PlayAnimationssss(string animationName, int layerIndex)
	{
		PlayAnimationssss(GetAnimation(animationName), layerIndex);
	}

	/// <summary>Gets a Spine Animation based on the state name.</summary>
	public Spine.Animation GetAnimation(string stateShortName)
	{
		return GetAnimation(StringToHash(stateShortName));
	}

	/// <summary>Gets a Spine Animation based on the hash of the state name.</summary>
	public Spine.Animation GetAnimation(int shortNameHash)
	{
		var foundState = statesAndAnimations.Find(entry => StringToHash(entry.stateName) == shortNameHash);
		return (foundState == null) ? null : foundState.animation;
	}

	/// <summary>Play an animation. If a transition animation is defined, the transition is played before the target animation being passed.</summary>
	public void PlayNewAnimation(Spine.Animation target, int layerIndex, bool loop)
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

	/// <summary>Play a non-looping animation once then continue playing the state animation.</summary>
	public void PlayAnimationssss(Spine.Animation oneShot, int layerIndex)
	{
		var state = skeletonAnimation.AnimationState;
		state.SetAnimation(layerIndex, oneShot, false);

		var transition = TryGetTransition(oneShot, TargetAnimation);
		if (transition != null)
			state.AddAnimation(0, transition, false, 0f);

		state.AddAnimation(0, this.TargetAnimation, true, 0f);
	}

	Spine.Animation TryGetTransition(Spine.Animation from, Spine.Animation to)
	{
        //foreach (var transition in transitions)
        //{
        //	if (transition.from.Animation == from && transition.to.Animation == to)
        //	{
        //		return transition.transition.Animation;
        //	}
        //}
        //return null;

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
