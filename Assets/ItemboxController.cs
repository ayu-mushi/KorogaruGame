using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemboxController : MonoBehaviour
{
    public GameObject focusPanel;
    // Start is called before the first frame update
    void Start()
    {
      focusPanel.GetComponent<Image>().color = new Color(1, 0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public GameObject focusPrefab(){
      return focusPanel.GetComponent<ItemSlotController>().prefab;
    }
    public void focus(GameObject p){
      if(focusPanel!=p){
      focusPanel.GetComponent<Image>().color = new Color(1, 1, 1);
      p.GetComponent<Image>().color = new Color(1, 0.5f,0.5f);
      focusPanel = p;
      }
    }
}
