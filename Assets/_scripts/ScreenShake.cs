namespace CommandPattern
{
    public static class ScreenShake
    {
        private static TraumaInducer _traumaInducer;

        public static void Execute()
        {
            _traumaInducer = TraumaInducer.Instance;
            _traumaInducer.StartCoroutine(_traumaInducer.InduceStress());
        }
    }
}