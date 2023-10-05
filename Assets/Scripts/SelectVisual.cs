using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectVisual : MonoBehaviour
{
    public GameObject selected;
    public GameObject target;

    public static GameObject instantiatedSelect = null;
    public static GameObject instantiatedTarget = null;
    // Start is called before the first frame update
    void Start()
    {
        instantiatedSelect = Instantiate(selected);
        instantiatedSelect.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        instantiatedTarget = Instantiate(target);
        instantiatedTarget.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //de visual is null
        if (Input.GetMouseButtonDown(0) && SelectPlanet.selectedPlanet != null && SelectPlanet.selectedPlanet != SelectPlanet.targetPlanet)
        {
            instantiatedSelect.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            instantiatedSelect.transform.position = SelectPlanet.selectedPlanet.body.transform.position;
            instantiatedSelect.transform.localScale = new Vector3(SelectPlanet.selectedPlanet.body.transform.localScale.x*2.1f, SelectPlanet.selectedPlanet.body.transform.localScale.y*2.1f);
        }

        if (Input.GetMouseButtonDown(1) && SelectPlanet.targetPlanet != null && SelectPlanet.selectedPlanet != SelectPlanet.targetPlanet)
        {
            instantiatedTarget.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            instantiatedTarget.transform.position = SelectPlanet.targetPlanet.body.transform.position;
            instantiatedTarget.transform.localScale = new Vector3(SelectPlanet.targetPlanet.body.transform.localScale.x*2.1f, SelectPlanet.targetPlanet.body.transform.localScale.y*2.1f);
        }
    }
}
