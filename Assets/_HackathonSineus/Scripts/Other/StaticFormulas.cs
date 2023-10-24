namespace YagaClub
{
    public static class StaticFormulas
    {
        public static bool IsValueZero(float value)
        {
            float valueError = 0.01f;

            if (value > valueError || value < -valueError)
                return false;
            else
                return true;
        }
    }
}
