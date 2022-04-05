using UnityEngine;

namespace CommandPattern
{
    public static class HopelessInfo
    {
        // counting the number of hopeless guy
        private static int countHopeless;

        public static void setCountHopeless(int val)
        {
            countHopeless = val;
        }

        public static void changeCountHopeless(int val)
        {
            countHopeless += val;
        }

        public static int getCountHopeless()
        {
            return countHopeless;
        }
    }
}