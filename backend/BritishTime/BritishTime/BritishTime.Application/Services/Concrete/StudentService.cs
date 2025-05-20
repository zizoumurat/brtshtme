using AutoMapper;
using BritishTime.Application.Services.Abstract;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.Contracts;
using BritishTime.Domain.Repositories.Students;
using Microsoft.AspNetCore.Identity;

namespace BritishTime.Application.Services.Concrete;

public class StudentService : IStudentService
{
    private readonly ISalesService _salesService;
    private readonly ICrmRecordActionService _crmRecordActionService;
    private readonly ICommandStudentRepository _commandStudentRepository;
    private readonly ICommandContractRepository _commandContractRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public StudentService(ISalesService salesService, ICommandStudentRepository commandStudentRepository, UserManager<AppUser> userManager, ICommandContractRepository commandContractRepository, IMapper mapper, ICrmRecordActionService crmRecordActionService)
    {
        _salesService = salesService;
        _commandStudentRepository = commandStudentRepository;
        _userManager = userManager;
        _commandContractRepository = commandContractRepository;
        _mapper = mapper;
        _crmRecordActionService = crmRecordActionService;
    }

    public async Task<StudentDto> AddAsync(CalculatePaymentDto calculatePaymentRequest, StudentCreateDto studentRequest)
    {
        try
        {
            var calculatePaymentResult = await _salesService.CalculatePayment(calculatePaymentRequest);

            if (calculatePaymentResult == null)
            {
                throw new Exception("Error calculating payment");
            }

            var user = new AppUser
            {
                Id = Guid.NewGuid(),
                FirstName = studentRequest.FirstName,
                LastName = studentRequest.LastName,
                Email = studentRequest.Email,
                UserName = studentRequest.Email,
                BranchId = studentRequest.BranchId
            };

            var firstName = studentRequest.FirstName.Trim();
            var lastName = studentRequest.LastName.Trim();
            var identityNumber = studentRequest.IdentityNumber.Trim();

            var namePart = $"BT{firstName[..1].ToLower()}";
            var surnamePart = $"{lastName[..1].ToLower()}";
            var last3Tc = identityNumber[^3..];

            var password = $"{namePart}{surnamePart}#{last3Tc}.";

            var createUserResult = await _userManager.CreateAsync(user, password);
            if (!createUserResult.Succeeded)
            {
                throw new Exception("Kullanıcı oluşturulamadı: " + string.Join(", ", createUserResult.Errors.Select(e => e.Description)));
            }

            await _userManager.AddToRoleAsync(user, "Student");

            // 1. Öğrenci oluştur
            var student = new Student
            {
                Id = Guid.NewGuid(),
                FirstName = studentRequest.FirstName,
                LastName = studentRequest.LastName,
                IdentityNumber = studentRequest.IdentityNumber,
                BirthDate = studentRequest.BirthDate,
                Phone = studentRequest.Phone,
                SecondPhone = studentRequest.SecondPhone,
                Email = studentRequest.Email,
                StudentType = studentRequest.StudentType,
                Address = studentRequest.Address,
                CityId = studentRequest.CityId,
                DistrictId = studentRequest.DistrictId,
                ParentFirstName = studentRequest.ParentFirstName,
                ParentLastName = studentRequest.ParentLastName,
                ParentPhone = studentRequest.ParentPhone,
                ParentIdentityNumber = studentRequest.ParentIdentityNumber,
                ParentBirthDate = studentRequest.ParentBirthDate,
                BranchId = studentRequest.BranchId,
                CrmRecordId = studentRequest.CrmRecordId,
                UserId = user.Id
            };

            await _commandStudentRepository.AddAsync(student);

            // 2. Sözleşme oluştur
            var contract = new Contract
            {
                Id = Guid.NewGuid(),
                StudentId = student.Id,
                StartDate = DateTime.Now,
                LevelCount = calculatePaymentRequest.LevelCount,
                TotalAmount = calculatePaymentResult.TotalAmount,
                PaymentMethod = calculatePaymentRequest.PaymentMethod,
                InstallmentCount = calculatePaymentRequest.InstallmentCount,
                Deposit = calculatePaymentRequest.Deposit,
                ContractType = calculatePaymentRequest.ContractType,
                EducationType = calculatePaymentRequest.EducationType,
                Signatory = calculatePaymentRequest.Signatory,
                LessonScheduleId = calculatePaymentRequest.LessonScheduleId,
                CampaignId = calculatePaymentRequest.CampaignId,
                DiscountId = calculatePaymentRequest.DiscountId,
                UsedLevelCount = 0,
                Installments = calculatePaymentResult.Installments.Select(x => new Installment
                {
                    DueDate = x.DueDate,
                    Amount = x.Amount
                }).ToList()
            };

            await _commandContractRepository.AddAsync(contract);

            if (studentRequest.CrmRecordId.HasValue)
                await _crmRecordActionService.AddAsync(new(studentRequest.CrmRecordId.Value, Domain.Enums.CrmActionType.Sale, null, "Satış işlemi yapıldı"));

            return _mapper.Map<StudentDto>(student);
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<PaginatedList<StudentDto>> GetAllAsync(StudentFilterDto filter, PageRequest pagination)
    {
        throw new NotImplementedException();
    }

    public Task<List<SelectListDto>> GetUserStudentListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<StudentDto> UpdateAsync(StudentDto StudentDto)
    {
        throw new NotImplementedException();
    }
}
