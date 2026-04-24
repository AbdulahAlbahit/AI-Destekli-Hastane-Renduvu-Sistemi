using Business_Layer.Services;

namespace Api_Layer
{
    public class AppointmentApi
    {
        private readonly IAppointmentService _service;

        public AppointmentApi(IAppointmentService service)
        {
            _service=service;
        }
    }
}
