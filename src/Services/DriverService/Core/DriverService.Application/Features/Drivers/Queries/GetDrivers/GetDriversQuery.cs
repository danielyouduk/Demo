using DriverService.Application.Features.Drivers.Shared;
using MediatR;
using Services.Core;
using Services.Core.Models;
using Services.Core.Models.Service;

namespace DriverService.Application.Features.Drivers.Queries.GetDrivers;

public record GetDriversQuery(
    PagedRequestQuery PagedRequestQuery) : IRequest<ServiceResponseCollection<IReadOnlyCollection<DriverDto>>>;