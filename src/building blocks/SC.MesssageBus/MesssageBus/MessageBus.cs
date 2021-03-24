using System;
using System.Threading.Tasks;
using EasyNetQ;
using SC.Core.Messages.Integration;
using Polly;
using RabbitMQ.Client.Exceptions;

namespace SC.MessageBus
{
    //abstrair para quando for usar outras messagens não tenha muiot impacto
    public class MessageBus : IMessageBus
    {
        private IBus _bus;
        private IAdvancedBus _advancedBus;//manipular algumas situaçoes, que o bus naão oferece

        private readonly string _connectionString;

        public MessageBus(string connectionString)
        {
            _connectionString = connectionString;
            TryConnect();
        }

        // se existe connected false
        public bool IsConnected => _bus?.IsConnected ?? false;
        public IAdvancedBus AdvancedBus => _bus?.Advanced;

        //tem que ser IntegrationEvent 
        public void Publish<T>(T message) where T : IntegrationEvent
        {
            TryConnect();
            _bus.Publish(message);
        }

        public async Task PublishAsync<T>(T message) where T : IntegrationEvent
        {
            TryConnect();
            await _bus.PublishAsync(message);
        }

        public void Subscribe<T>(string subscriptionId, Action<T> onMessage) where T : class
        {
            TryConnect();
            _bus.Subscribe(subscriptionId, onMessage);
        }

        public void SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage) where T : class
        {
            TryConnect();
            _bus.SubscribeAsync(subscriptionId, onMessage);
        }

        public TResponse Request<TRequest, TResponse>(TRequest request) where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return _bus.Request<TRequest, TResponse>(request);
        }

        public async Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent where TResponse : ResponseMessage
        {
            TryConnect();
            return await _bus.RequestAsync<TRequest, TResponse>(request);
        }

        public IDisposable Respond<TRequest, TResponse>(Func<TRequest, TResponse> responder)
            where TRequest : IntegrationEvent where TResponse : ResponseMessage
        {
            TryConnect();
            return _bus.Respond(responder);
        }

        public IDisposable RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> responder)
            where TRequest : IntegrationEvent where TResponse : ResponseMessage
        {
            TryConnect();
            return _bus.RespondAsync(responder);
        }

        //conectar no rabbitHuct
        private void TryConnect()
        {
            if(IsConnected) return;

            var policy = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()//não ta disponivel a conexão
                .WaitAndRetry(3, retryAttempt => // tentar 3 tentivasas
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            policy.Execute(() =>
            {
                _bus = RabbitHutch.CreateBus(_connectionString);
                _advancedBus = _bus.Advanced;//isso aqui vai instancia
                _advancedBus.Disconnected += OnDisconnect;//libera alguns itens, get de fila e etc
            });
        }

        private void OnDisconnect(object s, EventArgs e)
        {
            var policy = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .RetryForever();

            policy.Execute(TryConnect);
        }

        public void Dispose()
        {
            _bus.Dispose();
        }
    }
}