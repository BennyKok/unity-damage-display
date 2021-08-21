using System.Reflection.Emit;
using System.Collections.Generic;
using UnityEngine;

namespace BennyKok.DamageDisplay
{
    public class DamageDisplaySystem : PoolSystem<DamageDisplaySystem, DamageView>
    {
        [Header("System")]
        public Camera targetCamera;

        public float viewLifetime = 1f;
        public float viewUpwardSpeed = 10f;
        public bool positionStay;

        private bool defaultPositionStay;

        protected override void Awake()
        {
            base.Awake();
            defaultPositionStay = positionStay;

            if (!targetCamera)
                targetCamera = Camera.main;
        }

        public void DisplayDamage(Transform target, Vector3 offset, float value)
        {
            var view = GetAvailableItem();

            positionStay = defaultPositionStay;

            view.OnDisplay(target, offset, value);
            view.ResetToPool(viewLifetime);
        }

        public void DisplayDamage(Transform target, Vector3 offset, string value, Color color, bool worldPositionStay, float fontSize)
        {
            var view = GetAvailableItem();

            positionStay = worldPositionStay;

            view.OnDisplay(target, offset, value, color, fontSize);
            view.ResetToPool(viewLifetime);
        }
    }
}