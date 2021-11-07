using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPreview : MonoBehaviour
{
    public GameObject Geo1, Geo2, Geo3;
    public List<GameObject> Currentcharacter,Secondcharacter,Thirdcharacter;
    public List<GameObject> particleSystemList = new List<GameObject>();
    public Material D_material;
    private Material CC_material;
    public float Waittime = 10;
    public int counter = 2;
    // Start is called before the first frame update
    void Start()
    {
        storingChara(0,1,2);
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Waittime <= 0)
        {
            if (counter < 101)
            {
                counter += 1;
            }
            else
            {
                counter = 2;
            }
            storingChara(counter-2,counter-1,counter);
            Waittime = 5;
        }
        else
        {
            Waittime -= 0.01f;
        }


    }


     void storingChara (int first,int second,int third)
    {
        Currentcharacter = new List<GameObject>(0);
        for(int i = 0; i< Geo1.transform.childCount; i++)
        {
            Currentcharacter.Add(Geo1.transform.GetChild(i).gameObject);
        }

        Secondcharacter = new List<GameObject>(0);
        for (int i = 0; i < Geo2.transform.childCount; i++)
        {
            Secondcharacter.Add(Geo2.transform.GetChild(i).gameObject);
        }

        Thirdcharacter = new List<GameObject>(0);
        for (int i = 0; i < Geo3.transform.childCount; i++)
        {
            Thirdcharacter.Add(Geo3.transform.GetChild(i).gameObject);
        }


        for (int i = 0; i < Currentcharacter.Count; i++)
        {
            Currentcharacter[i].SetActive(false);
            if(i == first)
            {
                particleconfig();
                Currentcharacter[i].SetActive(true);
            }
        }

        for (int i = 0; i < Secondcharacter.Count; i++)
        {
            Secondcharacter[i].SetActive(false);
            if (i == second)
            {
               
                Secondcharacter[i].SetActive(true);
                Secondcharacter[i].GetComponent<SkinnedMeshRenderer>().material = D_material;
            }
        }

        for (int i = 0; i < Thirdcharacter.Count; i++)
        {
            Thirdcharacter[i].SetActive(false);
            if (i == third)
            {
               
                Thirdcharacter[i].SetActive(true);
                Thirdcharacter[i].GetComponent<SkinnedMeshRenderer>().material = D_material;
            }
        }

    }

    void particleconfig()
    {
        foreach(GameObject temp in particleSystemList)
        {
            temp.SetActive(false);
          
        }

        particleSystemList[Random.Range(0, particleSystemList.Count)].SetActive(true);
    }
}
