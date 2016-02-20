namespace Ssi.TrackTruck.Bussiness.Helpers
{
    public static class Util
    {
        public static long PessoToCentavos(double? pesso)
        {
            return (long) (pesso ?? 0)*100;
        }
    }
}
