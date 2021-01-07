using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    Normal,
    Lazer,
    Ice
}

public class Bullet : MonoBehaviour
{
    #region Field Declarations

    public SkeletonAnimation skeletonAnimation;
    public BulletType bulletType = BulletType.Normal;
    public float speed = 10f;
    public Vector2 Direction { get; set; }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Direction * Time.deltaTime * speed, Space.World);
        var position = transform.position;

        if (position.x < 0 && position.x > Screen.width || position.y < 0 || position.y > Screen.height)
        {
            Destroy(gameObject);
        }
    }
}
