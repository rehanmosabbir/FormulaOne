using FormulaOne.Entities.Dtos.Requests;
using MediatR;

namespace FormulaOne.API.Commands
{
    public class UpdateDriverInfoRequest : IRequest<bool>
    {
        public UpdateDriverRequest DriverRequest { get; set; }
        public UpdateDriverInfoRequest(UpdateDriverRequest updateDriverRequest)
        {
            DriverRequest = updateDriverRequest;
        }
    }
}
