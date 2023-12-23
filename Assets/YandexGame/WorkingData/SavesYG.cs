namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public int Money;

        public int LvlStove;
        public int LvlBoiler;
        public int LvlCoffeeMachine;

        public int LvlAerosol;
        public int LvlBroom;
        public int LvlCat;

        public int LvlFuel;

        public bool IsTrainingСompleted;
        public float Volume;
    }
}
