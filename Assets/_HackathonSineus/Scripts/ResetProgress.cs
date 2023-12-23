using UnityEngine;
using YagaClub;
using YG;
using Zenject;

public class ResetProgress : MonoBehaviour
{
    [Inject] private Saving _saving;

    public void ResetProg()
    {
        YandexGame.ResetSaveProgress();
        _saving.OnSave();
    }
}
