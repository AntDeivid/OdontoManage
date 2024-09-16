using AutoMapper;
using OdontoManage.Application.Interfaces;
using OdontoManage.Application.Models.DTOs;
using OdontoManage.Core.Interfaces;
using OdontoManage.Core.Models;

namespace OdontoManage.Application.Services;

public class AddressService : IAddressService
{
        private readonly IAddressRespository _repository;
        private readonly IMapper _mapper;

        public AddressService(IAddressRespository repository, IMapper mapper)
        {
                _repository = repository;
                _mapper = mapper;
        }

        public AddressDto Create(AddressCreateDto address)
        {
                var exist = _repository.GetAddressByCode(address.ZipCode);

                if (exist != null)
                {
                        throw new Exception($"Address already exists");
                }
                
                var entity = _mapper.Map<Address>(address);
                var createdAddress = _repository.Save(entity);
                return _mapper.Map<AddressDto>(createdAddress);
        }

        public List<AddressDto> GetAll()
        {
                var addresses = _repository.GetAll();
                return _mapper.Map<List<AddressDto>>(addresses);
        }

        public AddressDto GetById(Guid id)
        {
                var address = _repository.GetById(id);
                if (address == null)
                {
                        throw new Exception($"Address not found");
                }
                return _mapper.Map<AddressDto>(address); 
        }

        public AddressDto GetByCode(string code)
        {
                var address = _repository.GetAddressByCode(code);
                if (address == null)
                {
                        throw new Exception($"Address not found");
                }
                return _mapper.Map<AddressDto>(address);
        }
        
        public AddressDto Update(Guid id,AddressUpdateDto address)
        {
                var exist = _repository.GetById(id);
                if (exist == null)
                {
                        throw new Exception($"Address not found");
                }
                
                exist.ZipCode = address.ZipCode;
                exist.City = address.City;
                exist.Street = address.Street;
                exist.Neighborhood = address.Neighborhood;
                exist.State = address.State;
                
                var updatedAddress = _repository.Update(exist);
                return _mapper.Map<AddressDto>(updatedAddress);
        }

        public void Delete(Guid id)
        {
                var address = _repository.GetById(id);
                if (address == null)
                {
                        throw new Exception($"Address not found");
                }
                _repository.Delete(id);
        }
}