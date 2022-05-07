using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpdateTextUI : MonoBehaviour
{
    public Text bookCount;
    public Text personCount;

    public Text weaponInfo;

    public Text taskInfo;

    private bool task1 = false;

    private bool task2 = false;

    private bool task3 = false;
    // Update is called once per frame
    void Update()
    {
        bookCount.text = "";
        bookCount.text += "x " + LevelState.NotesNum;

        personCount.text = "";
        personCount.text += "x " + HopelessInfo.getCountHopeless();

        weaponInfo.text = "";
        if (WeaponSwitching.selectedWeapon == 0)
        {
            weaponInfo.text = (Flashlight.getCurrentBatteries() / Flashlight.getMaxBatteryCount()*100).ToString("F0") + "%";
        }else if (WeaponSwitching.selectedWeapon == 1)
        {
            weaponInfo.text = "x " + LevelState.decoyNum.ToString();
        }

        taskInfo.text = "";
        if (LevelState.NotesNum != HopelessInfo.getCountHopeless())
        {
            taskInfo.text += "Task : Collect " + (HopelessInfo.getCountHopeless() - LevelState.NotesNum).ToString() +
                             " Notes on the Table";
            if (task1 == false)
            {
                task1 = true;
                SoundManager.PlaySound(SoundManager.Sound.taskFinish);
            }
        }
        else if (HopelessInfo.getCountHopeless() != 0)
        {
            taskInfo.text += "Task : Save " + HopelessInfo.getCountHopeless() + " People in Forest";
            if (task2 == false)
            {
                task2 = true;
                SoundManager.PlaySound(SoundManager.Sound.taskFinish);
            }
        }
        else if (SafeZone.safe && HopelessInfo.getCountHopeless() == 0 && LevelState.NotesNum == 0)
        {
            taskInfo.text += "Congrats! You finish all the tasks!";
            StartCoroutine(changeLevel());
        }
        else if(HopelessInfo.getCountHopeless() == 0 && LevelState.NotesNum == 0)
        {
            taskInfo.text += "Task : Go Back to the Safe House";
            if (task3 == false)
            {
                task3 = true;
                SoundManager.PlaySound(SoundManager.Sound.taskFinish);
            }
        }
    }
    IEnumerator changeLevel()
    {
        yield return new WaitForSeconds(2);
        LevelState.Level+=1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Reset();
    }
    void Reset()
    {
        // reset the setting
        HopelessInfo.setCountHopeless(0);
        LevelState.NotesNum = 0;
        Flashlight.setBatteries(5);
    }
}
