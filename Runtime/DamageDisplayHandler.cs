using UnityEngine;
using UnityEngine.Events;

namespace BennyKok.DamageDisplay
{
    public class DamageDisplayHandler : MonoBehaviour
    {
        public Vector3 offset;
        public string prefix;
        public Color textColor;
        public bool worldPositionStay;
        public float fontSize = 14;
        public UnityEvent afterDisplay;

        public void DisplayColor(int value)
        {
            DamageDisplaySystem.Instance.DisplayDamage(transform, offset, prefix + value.ToString(), textColor, worldPositionStay, fontSize);
            afterDisplay.Invoke();
        }

        public void Display(int value)
        {
            DamageDisplaySystem.Instance.DisplayDamage(transform, offset, value);
            afterDisplay.Invoke();
        }
    }
}