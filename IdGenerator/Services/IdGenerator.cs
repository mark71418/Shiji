using IdGenerator.Models;

namespace IdGenerator.Services
{
    public interface IIdGenerator
    {
        int Generate(string clientId);
    }

    public class IdGenerator : IIdGenerator
    {
        private object _lock = new();

        private List<Client> _clientLookup = new();

        public int Generate(string clientId)
        {
            var client = _clientLookup.FirstOrDefault(x => string.Equals(x.ClientId, clientId, StringComparison.OrdinalIgnoreCase));

            lock (_lock)
            {

                if (client != null)
                {
                    client.CurrentId++;
                }
                else
                {
                    client = new Client
                    {
                        ClientId = clientId,
                        CurrentId = 1
                    };

                    _clientLookup.Add(client);
                }
            }
           
            return client.CurrentId;
        }
    }
}
