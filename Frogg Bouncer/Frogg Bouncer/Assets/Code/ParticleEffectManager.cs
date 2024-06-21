using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectManager : MonoBehaviour
{
    [SerializeField] ParticleSystem[] particleSystems;

    public void ActivateParticleSystem(int index)
    {
        particleSystems[index].Play();
    }
    public void DeactivateParticleSystem(int index)
    {
        particleSystems[index].Stop();
    }
}
