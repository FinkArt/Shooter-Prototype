using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : WeaponBase
{
    protected override IEnumerator Reload()
    {
        var canReload = CurrentWeaponData.Bullets < CurrentWeaponData.BulletsPerClip && CurrentWeaponData.Clips > 0;
        if (canReload)
        {
            isReloading = true;
            animator.SetTrigger("ReloadIn");
            PlaySound(CurrentWeaponData.ReloadAudio);
            yield return new WaitForSeconds(animator.GetNextAnimatorClipInfo(0).Length);
            var toAdd = CurrentWeaponData.BulletsPerClip - CurrentWeaponData.Bullets;
            toAdd = Mathf.Min(toAdd, CurrentWeaponData.Clips);
            while (toAdd > 0)
            {
                animator.SetTrigger("PutBullet");
                yield return new WaitForSeconds(CurrentWeaponData.ReloadingTime);
                CurrentWeaponData.Bullets++;
                CurrentWeaponData.Clips--;
                toAdd--;
            }
            animator.SetTrigger("ReloadOut");
            yield return new WaitForSeconds(animator.GetNextAnimatorClipInfo(0).Length);
            isReloading = false;
        }
    }
}
