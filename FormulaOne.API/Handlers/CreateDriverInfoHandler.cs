﻿using AutoMapper;
using FormulaOne.API.Commands;
using FormulaOne.DataService.Repositories.Interfaces;
using FormulaOne.Entities.DbSet;
using FormulaOne.Entities.Dtos.Responses;
using MediatR;

namespace FormulaOne.API.Handlers
{
    public class CreateDriverInfoHandler : IRequestHandler<CreateDriverInfoRequest, GetDriverResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;
        public CreateDriverInfoHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetDriverResponse> Handle(CreateDriverInfoRequest request, CancellationToken cancellationToken)
        {
            var driver = _mapper.Map<Driver>(request.DriverRequest);
            await _unitOfWork.Drivers.Add(driver);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<GetDriverResponse>(driver);
        }
    }
}
