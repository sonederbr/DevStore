using Rebus.Sagas;

namespace DevStore.Sales.Application
{
    public class OrderSagaData : SagaData
    {
        public bool OrderStarted { get; set; }
        public bool OrderEnrolledAccepted { get; set; }
        public bool PaymentAccepted { get; set; }
        public bool OrderFinished { get; set; }
        public bool OrderCanceled { get; set; }

        public bool SagaIsCompleted => OrderStarted
                                 && PaymentAccepted
                                 && OrderFinished 
                                 || OrderCanceled;
    }
}