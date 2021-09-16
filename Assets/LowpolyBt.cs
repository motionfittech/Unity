using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowpolyBt : MonoBehaviour
{
   public AvatarLoaderProfile ALP;
    int totalcount;
    private void Start()
    {
      totalcount =  ALP.ListofModel.Count;
    }
    public void clickBt()
    {
       
            for (int x = 0; x < totalcount; x++)
            {
                ALP.ListofModel[x].SetActive(false);

            }
            ALP.ListofModel[int.Parse(this.gameObject.name)].SetActive(true);
        LocalDatabase.instance.setCharacter(this.gameObject.name);

    }
}
