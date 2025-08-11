namespace Prime.Services {
    public class PrimeServiceX {

        public bool IsPrime(int candidate) {
            if (candidate < 2) {
                return false;
            } else {
                throw new NotImplementedException("Please create a test first.");
            }
        }

        public bool IsEvent(int candidate) {
            if (candidate % 2 == 0) {
                return true;
            } else {
                return false;
            }
        }



    }
}