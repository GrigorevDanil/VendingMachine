using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using VendingMachine.Application.Abstractions;
using VendingMachine.Application.Abstractions.Messages;
using VendingMachine.Application.Abstractions.Repositories.Base;
using VendingMachine.Application.Extensions;
using VendingMachine.Domain.Shared;
using VendingMachine.Domain.ValueObjects.Ids;

namespace VendingMachine.Application.Commands.Product.UpdateProductStock;

public class UpdateProductStockHandler : ICommandHandler<Guid, UpdateProductStockCommand>
{
    private readonly IValidator<UpdateProductStockCommand> _validator; 
    
    private readonly IRepository<VendingMachine.Domain.Entities.Product,ProductId>  _productRepository;
    
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly ILogger<UpdateProductStockHandler> _logger;

    public UpdateProductStockHandler(IValidator<UpdateProductStockCommand> validator, IRepository<Domain.Entities.Product, ProductId> productRepository, IUnitOfWork unitOfWork, ILogger<UpdateProductStockHandler> logger)
    {
        _validator = validator;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(UpdateProductStockCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false) 
            return validationResult.ToErrorList();
        
        var productId = ProductId.Of(command.ProductId);
        var productResult = await _productRepository.GetByIdAsync(productId, cancellationToken);
        if (productResult.IsFailure) 
            return productResult.Error.ToErrorList();
        var product = productResult.Value;
        
        var setStockResult = product.SetStock(command.Stock);
        if (setStockResult.IsFailure) 
            return setStockResult.Error.ToErrorList();
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Stock product {productId} was be updated", command.ProductId);

        return command.ProductId;
    }
}