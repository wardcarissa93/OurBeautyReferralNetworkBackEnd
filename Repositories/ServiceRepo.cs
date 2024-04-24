﻿using OurBeautyReferralNetwork.Data;
using OurBeautyReferralNetwork.Models;
using static System.Net.Mime.MediaTypeNames;

namespace OurBeautyReferralNetwork.Repositories
{
    public class ServiceRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly obrnDbContext _obrnContext;

        public ServiceRepo(ApplicationDbContext context, obrnDbContext obrnContext)
        {
            _context = context;
            _obrnContext = obrnContext;
        }

        public IEnumerable<Service> GetAllServices()
        {
            return _obrnContext.Services.ToList();
        }

        public Service GetServiceById(int serviceId)
        {
            var service = _obrnContext.Services.FirstOrDefault(t => t.PkServiceId == serviceId);
            if (service == null)
            {
                return null; // Return a 404 Not Found response if feeId does not exist
            }
            return service;
        }

        public List<Service> GetAllServicesOfBusiness(string businessId)
        {
            return _obrnContext.Services
                .Where(s => s.FkBusinessId == businessId)
                .ToList();
        }

        public bool CreateServiceForBusiness(Service service, string businessId)
        {
            bool isSuccess = true;
            try
            {
                _obrnContext.Services.Add(new Service
                {
                    PkServiceId = service.PkServiceId,
                    Image = service.Image,
                    FkBusinessId = businessId,
                    ServiceName = service.ServiceName,
                    Description = service.Description,
                    FkDiscountId = service.FkDiscountId,
                    BasePrice = service.BasePrice
                });
                _obrnContext.SaveChanges();
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }

            return isSuccess;
        }
    }
}
