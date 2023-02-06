using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    #region Variables

    [Tooltip("cadence de tir")]
    [SerializeField] private float rateFire;

    [Tooltip("munition tire")]
    [SerializeField] private GameObject ammo;

    [Tooltip("objet que regarde le player")]


    private int layerMask = 1 << 6;
    private Bullets bullets;
    public Transform player;
    private Camera cam;

    #endregion

    #region Built Methods

    void Start()
    {
        cam = Camera.main;
        bullets = GetComponent<Bullets>();
        StartCoroutine(Shoot()); 
    }
    void Update()
    {
        StartCoroutine(Shoot());
        Look();
    }
    #endregion

    public IEnumerator Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AmmoInstantiate();
            yield return new WaitForSeconds(rateFire);
        }
    }

    void AmmoInstantiate()
    {
        GameObject bullet = Instantiate(ammo, gameObject.transform.position, Quaternion.identity);
        bullet.transform.eulerAngles = player.eulerAngles;
    }

    void Look()
    {
        /*var lookAtPos = Input.mousePosition;
        lookAtPos.z = cam.transform.position.y - transform.position.y;
        lookAtPos = cam.ScreenToWorldPoint(lookAtPos);
        transform.forward = lookAtPos - transform.position;*/

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 100, layerMask))
        {
            Vector3 victor = hit.point;
            //transform.position = hit.point;
            transform.LookAt(hit.point + new Vector3(0, 1.1f, 0)); // position de l'object qui represente le curseur
        }
    }
}
