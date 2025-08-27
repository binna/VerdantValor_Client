using UnityEngine;

namespace Knight.Adventure
{
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] private Define.MovingPlatformType type;
        [SerializeField] private float theta;
        [SerializeField] private float power = 0.1f;
        [SerializeField] private float speed = 1f;

        private Vector3 _initPosition;

        void Awake()
        {
            _initPosition = transform.position;
        }

        void Update()
        {
            // 매 프레임마다 경과 시간에 따라 theta 증가
            // theta = 시간 기반 각도 → sin(theta)로 진동 모션 생성
            // speed: 1초에 얼마나 빠르게 진동이 진행되는지를 조절
            theta += Time.deltaTime * speed;

            if (type == Define.MovingPlatformType.Horizontal)
            {
                transform.position = new Vector3(
                    _initPosition.x + power * Mathf.Cos(theta),
                    _initPosition.y,
                    _initPosition.z);
                return;
            }

            transform.position = new Vector3(
                _initPosition.x, 
                _initPosition.y + power * Mathf.Cos(theta),
                _initPosition.z);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(Define.Tag.PLAYER))
            {
                other.transform.SetParent(transform);
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(Define.Tag.PLAYER))
            {
                other.transform.SetParent(null);
            }
        }
    }
}