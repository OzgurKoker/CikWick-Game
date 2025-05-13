using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{

    [SerializeField] private Transform _playerVisualTransform;

    private PlayerController _playerController;
    private Rigidbody _playerRigidbody;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _playerRigidbody = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<ICollectible>(out var collectible))
        {
            collectible.Collect();
            //In cases where the character was fast, the object could not destroy itself immediately, and the trigger was triggered more than once, causing an error.
            other.enabled = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IBoostable>(out var boostable))
        {
            boostable.Boost(_playerController);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.GiveDamage(_playerRigidbody, _playerVisualTransform);
        }



    }
}

