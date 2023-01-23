using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MageAttack : Attack
{
    GameObject rotatingFireball;
    GameObject baseFireball;

    [SerializeField] private PlayerParameter playerData;

    int timeWallInitial=5;
    int timeWall=5;


    HeroController controller;
    private void Start()
    {
        controller = this.gameObject.GetComponent<HeroController>();
        playerData = controller.playerData;
        rotatingFireball = GameObject.Find("Parent_BulletTrail").gameObject;
        rotatingFireball.transform.parent = null;
        Debug.Log(rotatingFireball);
        rotatingFireball.SetActive(false);

    }

    private void Update()
    {

    }
    private GameObject LoadPrefab(string filename)
    {
        var fireball = Resources.Load(filename);
        if (fireball == null)
        {
            throw new FileNotFoundException("...no file found - please check the configuration");
        }
        return (GameObject)fireball;
    }

    public override void First()
    {
        Debug.Log("Mage First Attack");

    }

    public override void Second()
    {
        rotatingFireball.SetActive(true);
        rotatingFireball.transform.GetChild(0).gameObject.SetActive(true);

        Debug.Log("Mage Second Attack");

    }
    public override void Third()
    {
        Debug.Log("Mage Third Attack");
        RaycastHit hit;
        Debug.Log("try wall");
        if (Physics.Raycast(controller.rayMouse, out hit) && hit.transform.CompareTag("Ground") ) //(0.1<Mathf.Abs(hit.transform.position.y - this.gameObject.transform.position.y) 
        {
            Debug.Log("wall win");

            GameObject iceWall = (GameObject)Resources.Load("Ice Wall");
            iceWall = Instantiate(iceWall, hit.point, Quaternion.identity);
            iceWall.gameObject.transform.LookAt(this.transform);
            iceWall.transform.position += Vector3.up * 2;

            StartCoroutine(DestroyWall(iceWall));
        }

    }

    IEnumerator DestroyWall(GameObject iceWall)
    {
        yield return new WaitForSeconds(timeWall);

        StartCoroutine(NeedDestroy(iceWall));

    }

    IEnumerator NeedDestroy(GameObject iceWall)
    {
        Debug.Log("Before effects");

        GameObject effects = (GameObject)Resources.Load("Effects Ice Wall");
        effects = Instantiate(effects, iceWall.transform.position, iceWall.transform.rotation); ;
        Destroy(iceWall);
        yield return new WaitForSeconds(2);
        Destroy(effects);
        Debug.Log("After effects");

    }

    private void OnDestroy()
    {
        Destroy(rotatingFireball);
    }
}
