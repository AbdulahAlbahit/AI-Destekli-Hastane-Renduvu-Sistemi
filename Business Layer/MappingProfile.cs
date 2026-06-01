using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Data_Accese_Layer.Entities;
using Business_Layer.Dto;
using Data_Accese_Layer.Dto;
namespace Business_Layer
{
    public class MappingProfile:Profile
    {

        public MappingProfile()
        {
            CreateMap<Appointment, AppointmentCreateDto>().ReverseMap();
            CreateMap<Appointment,AppointmentDetailDto>().ReverseMap();
        }
    }
}
