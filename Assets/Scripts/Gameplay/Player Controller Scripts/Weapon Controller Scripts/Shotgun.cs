using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : WeaponBase
{
    protected override IEnumerator Reload()
    {
        var canReload = bullets < bulletsPerClip && clips > 0;
        if (canReload)
        {
            isReloading = true;
            animator.SetTrigger("ReloadIn");
            yield return new WaitForSeconds(animator.GetNextAnimatorClipInfo(0).Length);
            var toAdd = bulletsPerClip - bullets;
            toAdd = Mathf.Min(toAdd, clips);
            while (toAdd > 0)
            {
                animator.SetTrigger("PutBullet");
                yield return new WaitForSeconds(_reloadingTime);
                bullets++;
                clips--;
                toAdd--;
            }
            animator.SetTrigger("ReloadOut");
            yield return new WaitForSeconds(animator.GetNextAnimatorClipInfo(0).Length);
            isReloading = false;
        }
    }
}
