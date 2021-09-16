using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowpolyBt : MonoBehaviour
{
  public LowPolySideMenu ALP;
    int totalcount;
    private void Start()
    {
      totalcount =  ALP.UID.character.Count;
    }
    public void clickBt()
    {
       
            for (int x = 0; x < totalcount; x++)
            {
                ALP.UID.character[x].SetActive(false);

            }
            ALP.UID.character[int.Parse(this.gameObject.name)].SetActive(true);
            ALP.UID.setCharacter(this.gameObject.name);

    }
}
