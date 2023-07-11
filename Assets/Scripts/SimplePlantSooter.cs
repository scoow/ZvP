using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlantSooter : MonoBehaviour
{  //  [SerializeField] private float _circleRadius;
   // [SerializeField] private float _maxVisionDistance;
   //  [SerializeField] private LayerMask _layerMask;
   //  [SerializeField] float _shootTimer = 1.3f;
    [SerializeField] Transform _weapon;
    [SerializeField] Bullet _bullet;
    // private Vector2 _originalPosition;
    //  private Vector2 _direction;
    //  bool _isShooting = false;
    //   bool _isReadyToShoot = false;
    // Start is called before the first frame update
    void Start()
    {
        //  _direction = Vector2.right;
        // _originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        //   RaycastHit2D hit = Physics2D.CircleCast(_originalPosition,_circleRadius,_direction,_maxVisionDistance,_layerMask );
        //   if(hit)
        //   {
        //    _isShooting = true;


    }
    //  private void OnDrawGizmos()
    // {
    //    Gizmos.color = Color.red;
    //   Debug.DrawLine(_originalPosition,_originalPosition + _direction *_maxVisionDistance ,Color.red);
    //   Gizmos.DrawWireSphere(_originalPosition + _direction * _maxVisionDistance, _circleRadius);
    // }
    public void Fire()
    {
        var newBullet = Instantiate(_bullet, _weapon.transform.position, transform.rotation);
    }

}
