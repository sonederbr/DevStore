using DevStore.Core.Bus;
using System;
using System.Threading.Tasks;
using DevStore.Catalog.Domain.Events;

namespace DevStore.Catalog.Domain
{
    public class StockService : IStockService
    {
        private readonly ICourseRepository _productRepository;
        private readonly IMediatorHandler _bus;

        public StockService(ICourseRepository productRepository, 
                            IMediatorHandler bus)
        {
            _productRepository = productRepository;
            _bus = bus;
        }

        public async Task<bool> WithdrawStocks(Guid productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);

            if (product == null) return false;

            if (!product.HasStock(quantity)) return false;

            product.WithdrawStock(quantity);

            // TODO: Parametrizar a quantidade de estoque baixo
            if (product.PlacesAvailable < 10)
            {
                await _bus.PublishEvent(new LowStockCourseEvent(product.Id, product.PlacesAvailable));
            }

            _productRepository.Update(product);
            return await _productRepository.UnitOfWork.Commit();
        }

        public async Task<bool> ChargeStock(Guid productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);

            if (product == null) return false;
            product.ChargeStock(quantity);

            _productRepository.Update(product);
            return await _productRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            _productRepository.Dispose();
        }
    }
}