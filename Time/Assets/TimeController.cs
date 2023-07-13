using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    [SerializeField] float timeSpeed = 1f;

    public GameObject[] HMS;

    [SerializeField] Text TimerT;
    [SerializeField] Text[] TextEnter;

    bool customizationChack = false;

    [SerializeField] Toggle toggle;
    [SerializeField] Toggle toggleCheckDay;

    private void Start()
    {
        GetTimePK();
    }

    int h, m, s;
    int clockH, clockM;

    private void GetTimePK()
    {
        h = int.Parse( DateTime.Now.ToString("HH:mm:ss").Substring(0, 2));
        m = int.Parse(DateTime.Now.ToString("HH:mm:ss").Substring(3, 2));
        s = int.Parse(DateTime.Now.ToString("HH:mm:ss").Substring(6, 2));
        angleTime();

        StartCoroutine(Timers());
    }

    private void angleTime()
    {
        HMS[0].transform.rotation = Quaternion.Euler(0, 0, -h * 30);
        HMS[1].transform.rotation = Quaternion.Euler(0, 0, -m * 6);
        HMS[2].transform.rotation = Quaternion.Euler(0, 0, -s * 6);
    }

    private void Error()
    {
        print("Будильник выключен");
    }

    public void setTime()
    {
        if (!toggle.isOn)
        {
            clockH = int.Parse(TextEnter[0].text);
            clockM = int.Parse(TextEnter[1].text);
        }
        else
        {
            if (toggleCheckDay.isOn)
            {
                if (HMS[0].transform.rotation.eulerAngles.z > 0)
                {
                    clockH = (int)Math.Round((360 - HMS[0].transform.rotation.eulerAngles.z) / 30);
                    clockM = (int)Math.Round((360 - HMS[1].transform.rotation.eulerAngles.z) / 6);
                }
                else
                {
                    clockH = (int)Math.Round((HMS[0].transform.rotation.eulerAngles.z / 30));
                    clockM = (int)Math.Round((HMS[1].transform.rotation.eulerAngles.z / 6));
                }
            }
            else
            {
                if (HMS[0].transform.rotation.eulerAngles.z > 0)
                {
                    clockH = (int)Math.Round((360 - HMS[0].transform.rotation.eulerAngles.z) / 30) + 12;
                    clockM = (int)Math.Round((360 - HMS[1].transform.rotation.eulerAngles.z) / 6);
                }
                else
                {
                    clockH = (int)Math.Round((HMS[0].transform.rotation.eulerAngles.z / 30)) + 12;
                    clockM = (int)Math.Round((HMS[1].transform.rotation.eulerAngles.z / 6));
                }
            }

            if(clockH == 24)
            {
                clockH = 0;
            }
        }

        print(clockH +":"+ clockM);

        if (clockH > 24 || clockM > 60)
        {
            Error();
        }
    }

    IEnumerator Timers()
    {
        while(true)
        {
            if (!customizationChack)
            {
                ChecingTheTime();
                angleTime();
            }
            else
            {
                ChecingTheTime();
            }
            TimerT.text = h.ToString() + ":" + m.ToString() + ":" + s.ToString();

            if(clockH == h && clockM == m)
            {
                print("Будильник сработал");
            }

            yield return new WaitForSeconds(timeSpeed);
        }
    }

    public void customizeStart()
    {
        customizationChack = !customizationChack;
    }

    private void ChecingTheTime()
    {
        if (s < 60)
        {
            s += 1;
        }
        else
        {
            s = 0;
            m++;
            if (m >= 60)
            {
                m = 0;
                h++;
                if (h >= 24)
                {
                    h = 0;
                }
            }
        }

    }
}
