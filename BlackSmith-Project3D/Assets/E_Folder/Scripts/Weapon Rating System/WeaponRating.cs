using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRating : MonoBehaviour
{
    [SerializeField] float DamagePerTenthOfStar = 1.2f; // Damage that weapon should've taken to lose 0.1 star
    [SerializeField] float MaxDamageForBestRating;

    private float WeaponStarRating;



    //Smithing: Minimum hold dmg = 3 (1,6s)// Max = 10 (2,6s+) (We have 4 sword smithing stages)
    //Sharpening: Minimum dmg = 1 (For every 0.7s of mistake) (We have 10 section each max for 3s, and max 2 section at a time)

    // Values for rating: MaxDamageForBestRating == 1;



    //Rates weapon on a scale from 1 (lowest) to 5 (highest) stars
    public float RateWeapon(float DamageDealedToWeapon)
    {
        if (DamageDealedToWeapon <= 0 || DamageDealedToWeapon <= MaxDamageForBestRating)
        {
            return 5;
        }

        DamageDealedToWeapon -= MaxDamageForBestRating; // We subtract maxDamageForBestRating since calculations should not include our first 17 damage dealt to weapon

        WeaponStarRating = (DamageDealedToWeapon / DamagePerTenthOfStar ) / 10; // Calculates how much rating will be taken away from max possible (5 stars)

        WeaponStarRating = 5 - WeaponStarRating; // Decreasing rating for damage dealt (taking from max 5 stars score)

        WeaponStarRating = Mathf.Floor(WeaponStarRating * 10) / 10; // We multiply our number to save tenth part of the number, after that we floor it removing everything beside whole part of the number and divide it by 10 to reverse our multiplying part and get a result of format XXX.X

        if (WeaponStarRating > 5)
        {
            return 5;
        }
        else if (WeaponStarRating <= 1)
        {
            return 1;
        }

        return WeaponStarRating;
    }
}
