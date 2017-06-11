using PlayWay.Water;
using UnityEngine;
using UnityEngine.UI;

namespace PlayWay.WaterSamples
{
    public class WaterSettings : MonoBehaviour
    {
        public int quality = 4;

        public void Start()
        {
            UpdateQuality();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                UpdateQuality();
        }

        private void UpdateQuality()
        {
            Debug.Log("Water Quality Changed: " + quality);
            WaterQualitySettings.Instance.SetQualityLevel(quality);
        }
    }
}
