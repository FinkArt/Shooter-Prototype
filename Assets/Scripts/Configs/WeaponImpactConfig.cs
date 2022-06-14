using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponImpactConfig", menuName = "OurTools/WeaponImpactConfig", order = 2)]
public class WeaponImpactConfig : ScriptableObject
{
    [SerializeField] private ImpactData[] _impactsData;
    [SerializeField] private float _impactLifetime = 2f;

    public float ImpactLifetime => _impactLifetime;

    public ImpactData GetImpactByTag(string impactTag)
    {
        ImpactData defaultData = null;
        foreach (var impactData in _impactsData)
        {
            if (impactData.IsDefault)
                defaultData = impactData;
            if (impactData.GroundTag == impactTag)
                return impactData;
        }

        return defaultData;
    }
}

[System.Serializable]
public class ImpactData
{
    public string GroundTag;
    public ParticleSystem VFX;
    public bool IsDefault;
}