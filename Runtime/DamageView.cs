using System;
using UnityEngine;

namespace BennyKok.DamageDisplay
{
    public class DamageView : Poolable
    {
        public TMPro.TextMeshProUGUI label;

        [System.NonSerialized]
        public Transform target;

        private DamageDisplaySystem system;

        private Color defaultColor;

        private Vector3 initPos;

        private Vector3 worldOffset;

        private Vector3 offset;

        private bool fading;

        private CanvasGroup canvasGroup;

        private Vector3 lastTransfromPosition;


        private float defaultTextSize;


        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            defaultColor = label.color;
            defaultTextSize = label.fontSize;
        }

        public void OnDisplay(Transform target, Vector3 offset, float damage)
        {
            OnDisplay(target, offset, (Math.Round(damage, 2)).ToString(), defaultColor, defaultTextSize);
        }

        public void OnDisplay(Transform target, Vector3 offset, string damage, Color color, float fontSize)
        {
            this.label.text = damage;
            this.label.color = color;
            this.target = target;
            label.fontSize = fontSize;

            worldOffset = offset;

            var worldPoint = target.transform.position;
            var screenPos = system.targetCamera.WorldToScreenPoint(worldPoint);
            initPos = screenPos;

            this.offset.x += UnityEngine.Random.Range(40, -40);

            if (target)
            {
                lastTransfromPosition = target.transform.position;
            }
        }

        private void OnEnable()
        {
            system = DamageDisplaySystem.Instance;
        }

        private void Update()
        {
            if (target && !system.positionStay)
            {
                lastTransfromPosition = target.transform.position;
            }
            else
            {
                // if (!fading && used)
                //     ResetToPoolInstant();
            }

            Vector3 screenPos;
            var worldPoint = lastTransfromPosition + worldOffset;

            // if (system.positionStay)
            // {
            //     screenPos = initPos;
            // }
            // else
            // {
            screenPos = system.targetCamera.WorldToScreenPoint(worldPoint);
            // }

            var isBehind = Vector3.Dot(system.targetCamera.transform.forward, worldPoint - system.targetCamera.transform.position) < 0;
            label.enabled = !isBehind;

            offset.y += Time.unscaledDeltaTime * system.viewUpwardSpeed;
            transform.position = screenPos + offset;

            if (fading)
            {
                canvasGroup.alpha -= Time.unscaledDeltaTime / 0.2f;

                if (canvasGroup.alpha <= 0)
                {
                    fading = false;
                    ResetInternal();
                }
            }
        }

        protected override void ResetToPoolInstant()
        {
            fading = true;
        }

        private void ResetInternal()
        {
            base.ResetToPoolInstant();
            offset = Vector3.zero;
            canvasGroup.alpha = 1;
        }
    }
}