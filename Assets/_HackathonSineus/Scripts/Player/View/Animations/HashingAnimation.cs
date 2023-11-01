using UnityEngine;

namespace YagaClub
{
    public class HashingAnimation
    {
        public int PhoneOpen { get => Animator.StringToHash("PhoneOpen"); }
        public int OnLadder { get => Animator.StringToHash("OnLadder"); }
        public int Walk { get => Animator.StringToHash("Walk"); }
    }
}
