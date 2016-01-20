using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Misuka.Domain.DTO;
using Misuka.Domain.Entity;
using Misuka.Domain.SearchCriteria;
using Misuka.Domain.Security;
using Misuka.Domain.Utilities;
using Misuka.Infrastructure.Data;
using Misuka.Infrastructure.EntityFramework.Repositories;
using Misuka.Services.ReportServices.Persons;
using Misuka.Services.Services;

namespace Misuka.Services.ReportServices
{
  class PersonReportService: IPersonReportService
  {
    private readonly ICommandExecutor _executor;
    private readonly IPersonService _personService;
    private readonly IRepository<Domain.Entity.User> _userRepository;
    private readonly IUserSession _userSession;

    public PersonReportService(ICommandExecutor executor, IRepository<Domain.Entity.User> userRepository, IPersonService personService)
    {
      _executor = executor;
      _personService = personService;
      _userRepository = userRepository;
      _userSession = new UserSession();
    }

  
    public PersonDTO GetById(Guid personId)
    {
      var person = _personService.Find(personId);
      return Mapper.Map<Domain.Entity.Person, PersonDTO>(person);
    }

    public List<PersonDTO> GetAll()
    {
      var persons = _personService.Queryable().ToList();
      return persons.Select(Mapper.Map<Domain.Entity.Person, PersonDTO>).ToList();
    
    }

    public SearchResult<PersonDTO> Search(PersonSearchCriteria searchCriteria, int pageSize, int pageIndex)
    {
      return _executor.Execute(new GetPersonDTOBySearchCriteriaDbCommand(searchCriteria, pageIndex, pageSize));
    }

    public PersonDTO GetUserByUserName(string userName)
    {
      var user =
       _userRepository.Query(
         u => String.Compare(u.UserName, userName, StringComparison.InvariantCultureIgnoreCase) == 0)
         .Select()
         .FirstOrDefault();
      return GetById(user.PersonId);
    }
  }
}

