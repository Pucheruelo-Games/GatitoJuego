using System;
using UnityEngine;

namespace Code
{
    public class ParticleSystemPlayer : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particleSystem;

        public void Play()
        {
            particleSystem.Play();
        }
    }
}

