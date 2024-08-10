using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
    [SerializeField]
    public Slider BossHP;
    public FinalBossScript Finalboss;

    private float maxBH;
    private float curBH;
    // Start is called before the first frame update
    void Start()
    {
        maxBH = Finalboss.BossHp;
        curBH = Finalboss.BossHp;
        BossHP.value = (float) curBH / (float) maxBH;
    }

    // Update is called once per frame
    void Update()
    {
        curBH = Finalboss.BossHp;
        HandleHP();
    }

    private void HandleHP()
    {
        BossHP.value = (float)curBH / (float)maxBH;
    }
}
