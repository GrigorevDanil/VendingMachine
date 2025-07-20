using System.Windows.Input;
using ICommand = VendingMachine.Application.Abstractions.Messages.ICommand;

namespace VendingMachine.Application.Commands.Order.ClearUnpaidOrders;

public record ClearUnpaidOrdersCommand(): ICommand;