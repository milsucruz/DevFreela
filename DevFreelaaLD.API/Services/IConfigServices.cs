namespace DevFreelaaLD.API.Services
{
    public interface IConfigServices
    {
        int GetValue();
    }

    public class ConfigService : IConfigServices
    {
        private int _value;

        public int GetValue()
        {
            _value++;

            return _value;
        }
    }
}