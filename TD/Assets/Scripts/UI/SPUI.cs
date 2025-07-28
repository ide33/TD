// using UnityEngine;
// using UnityEngine.UI;

// public class SPUI : MonoBehaviour
// {
//     [SerializeField] private Slider spSlider;

//     // 追従するユニット
//     private Transform target;

//     public void Initialize(Transform followTarget, float maxSP)
//     {
//         target = followTarget;
//         spSlider.maxValue = maxSP;
//         spSlider.value = 0;
//     }

//     public void SetSP(float value)
//     {
//         spSlider.value = value;
//     }

//     private void LateUpdate()
//     {
//         // ユニットの頭上に固定
//         if (target != null)
//         {
//             // 頭上1.5に表示
//             transform.position = target.position + new Vector3(0, 1.5f, 0);
//         }
//     }
// }
