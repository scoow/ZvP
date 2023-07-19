using UnityEngine;

public class PlantHealth : MonoBehaviour
{
    [SerializeField] int _health = 100;
    Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Debug.Log(_health);
    }

    public void ProcessHit(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            _animator.SetTrigger("IsDiyng");
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
