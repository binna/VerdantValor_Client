using UnityEngine;

namespace Knight.Adventure
{
    public class PushPlatform : MonoBehaviour
    {
        [SerializeField] private float pushPower = 60;
        
        private Animator _animator;
        private Rigidbody2D _rigidbody;
        
        void Start()
        {
            _animator = GetComponent<Animator>();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            _rigidbody = other.GetComponent<Rigidbody2D>();
            Invoke("PushCharacter", 1f);
        }

        private void PushCharacter()
        {
            _rigidbody.AddForceY(pushPower, ForceMode2D.Impulse);
            _animator.SetTrigger(Define.AnimatorParameter.push);
        }
    }
}