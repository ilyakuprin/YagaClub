using System;
using YG;
using Zenject;

namespace YagaClub
{
    public class Saving : IInitializable, IDisposable
    {
        public event Action SaveDataReceived;

        public void Initialize()
        {
            YandexGame.GetDataEvent += OnDataReceived;

            if (YandexGame.SDKEnabled)
                OnDataReceived();
        }

        public void OnSave()
        {
            YandexGame.SaveProgress();
            OnDataReceived();
        }

        private void OnDataReceived()
        {
            SaveDataReceived?.Invoke();
        }

        public void Dispose()
        {
            YandexGame.GetDataEvent -= OnDataReceived;
        }
    }
}
