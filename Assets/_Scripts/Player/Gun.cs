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

    [SerializeField] private float offsetAmmo;
    [SerializeField] private Transform ammoPos;


    private int layerMask = 1 << 6;
    private Bullets bullets;
    public GameObject player;
    private Camera cam;
    private PlayerInputs _inputs;

    #endregion

    #region Built Methods

    void Start()
    {
        cam = Camera.main;
        bullets = GetComponent<Bullets>();
        _inputs = player.GetComponent<PlayerInputs>();
    }
    void Update()
    {
        StartCoroutine(Shoot());
        Look();
    }
    #endregion

    public IEnumerator Shoot()
    {
        if (_inputs.Attack)
        {
            AmmoInstantiate();
            yield return new WaitForSeconds(rateFire);
        }
    }

    void AmmoInstantiate()
    {
        //Vector3 ammoPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + offsetAmmo, gameObject.transform.position.z);
        GameObject bullet = Instantiate(ammo, ammoPos.position, Quaternion.identity);
        bullet.transform.eulerAngles = player.transform.eulerAngles;
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
